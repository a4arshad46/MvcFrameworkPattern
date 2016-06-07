using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Core
{
    public class Disposable : IDisposable
    {
        private bool bIsDisposed;
        ~Disposable()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool bDisposing)
        {
            if (!this.bIsDisposed && bDisposing)
            {
                this.vDisposeCore();
            }
            this.bIsDisposed = true;
        }
        protected virtual void vDisposeCore()
        {
        }
    }
}
