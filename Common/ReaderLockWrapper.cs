using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Games.NBall.Common
{
    public class ReaderLockWrapper : IDisposable
    {
        private ReaderWriterLock locker;

        public ReaderLockWrapper(ReaderWriterLock locker)
        {
            this.locker = locker;
        }

        public void Enter()
        {
            this.locker.AcquireReaderLock(0);
        }

        void IDisposable.Dispose()
        {
            this.locker.ReleaseReaderLock();
        }
    }
}
