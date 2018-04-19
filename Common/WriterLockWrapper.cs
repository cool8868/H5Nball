using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Games.NBall.Common
{
    public class WriterLockWrapper : IDisposable
    {
        private ReaderWriterLock locker;

        public WriterLockWrapper(ReaderWriterLock locker)
        {
            this.locker = locker;
        }

        public void Enter()
        {
            this.locker.AcquireWriterLock(0);
        }

        void IDisposable.Dispose()
        {
            this.locker.ReleaseWriterLock();
        }
    }
}
