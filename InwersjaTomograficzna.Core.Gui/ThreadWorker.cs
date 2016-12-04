using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.Gui
{
    public class ThreadWorker
    {
        public event EventHandler ThreadDone;

        public delegate void ComputeMethod();
        ComputeMethod run;
        public ThreadWorker(ComputeMethod method)
        {
            run = method;
        }

        public void Run()
        {
            run();
            ThreadDone?.Invoke(this, EventArgs.Empty);
        }

    }
}
