using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using log4net;
using System.Threading.Tasks;

namespace Games.NBall.ServiceEngine
{
    public abstract class BatchExecutorNew<TKey, TValue>
    {
        #region properties
        ConcurrentDictionary<TKey, TValue> buffer;
        int batchSize = 100;
        static ILog logger = LogManager.GetLogger(typeof(BatchExecutorNew<TKey, TValue>));
        long executeStatus;
        long bufferStatus;
        const long statusWaiting = 0;
        const long statusEexcuting = 1;
        static object syncLock = new object();

        public BatchExecutorNew()
        {
            buffer = new ConcurrentDictionary<TKey, TValue>();
            CreateTimer();
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
        }

        #endregion

        #region public methods

        public void Append(TKey key, TValue item)
        {
            long s = Interlocked.Read(ref bufferStatus);
            if (s == statusWaiting)
            {
                buffer.AddOrUpdate(key, item, (K, V) => { return item; });
            }
            else
            {
                Monitor.Enter(buffer);
                try
                {
                    buffer.AddOrUpdate(key, item, (K, V) => { return item; });
                }
                finally
                {
                    Monitor.Exit(buffer);
                }
            }
        }


        public void Flush()
        {
            FlushBuffer(buffer.Count);
        }

        #endregion

        #region protected methods

        protected void ProcessExitHandler(object sender, EventArgs e)
        {
            FlushBuffer(buffer.Count);
        }

        protected void ReadyToFlush()
        {
            long s = Interlocked.Read(ref executeStatus);
            if (s == statusWaiting)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(FlushBuffer), null);
            }
        }

        protected void FlushBuffer(object state)
        {
            lock (syncLock)
            {
                try
                {
                    Thread.VolatileWrite(ref executeStatus, statusEexcuting);
                    Thread.VolatileWrite(ref bufferStatus, statusEexcuting);

                    int length = 0;
                    TValue[] values = null;
                    Monitor.Enter(buffer);
                    try
                    {
                        length = buffer.Count;
                        if (length <= 0)
                        {
                            return;
                        }

                        values = new TValue[length];
                        buffer.Values.CopyTo(values, 0);
                        buffer.Clear();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                    }
                    finally
                    {
                        Monitor.Exit(buffer);
                        Thread.VolatileWrite(ref bufferStatus, statusWaiting);
                    }

                    int taskLength = Convert.ToInt32(Math.Ceiling(values.Length * 1.0 / batchSize));
                    Task[] tasks = new Task[taskLength];
                    for (int i = 0; i < taskLength; i++)
                    {
                        ArraySegment<TValue> segment = new ArraySegment<TValue>(values, i * batchSize, Math.Min(batchSize, length - i * batchSize));
                        tasks[i] = Task.Factory.StartNew(ExecuteTask, segment);
                    }
                    Task.WaitAll(tasks);

                }
                catch (AggregateException exceptions)
                {
                    exceptions.Flatten();
                    foreach (var ex in exceptions.InnerExceptions)
                    {
                        logger.Error(ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
                finally
                {
                    Thread.VolatileWrite(ref executeStatus, statusWaiting);
                }
            }
        }

        protected void ExecuteTask(object state)
        {
            try
            {
                ArraySegment<TValue> values = (ArraySegment<TValue>)state;
                if (values == null || values.Count == 0)
                {
                    return;
                }
                ExecutData(values);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        protected abstract void ExecutData(ArraySegment<TValue> data);

        #endregion

        #region Timer handle
        int timerInterval = 60 * 1;
        Timer timer = null;

        public virtual int TimerTickInterval
        {
            get { return timerInterval; }
        }

        private void CreateTimer()
        {
            TimerCallback callback = new TimerCallback(TimerTick);
            int interval = TimerTickInterval * 1000;
            timer = new Timer(callback, null, interval, interval);
        }

        protected void DestroyTimer()
        {
            if (timer == null)
            {
                return;
            }

            timer.Dispose();
            timer = null;
        }

        private void TimerTick(Object state)
        {
            Flush();
        }

        #endregion

        #region IDisposable
        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                DestroyTimer();
                Flush();
                AppDomain.CurrentDomain.ProcessExit -= ProcessExitHandler;

                disposed = true;
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~BatchExecutorNew()
        {
            Dispose(false);
        }
        #endregion
    }

}
