using System.Diagnostics;

namespace Extensions
{
    public class Stoper
    {
        private Stopwatch stopwatch;

        public long GetMiliseconds
        {
            get
            {
                if (stopwatch == null) return 0;
                return stopwatch.ElapsedMilliseconds;
            }
        }

        public void Start()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

    }
}
