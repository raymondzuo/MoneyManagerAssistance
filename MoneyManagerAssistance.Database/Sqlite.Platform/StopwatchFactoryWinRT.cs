using System.Diagnostics;
using SQLite.Net.Interop;

namespace DushuBus.Database.Sqlite.Platform
{
    public class StopwatchFactoryWinRT : IStopwatchFactory
    {
        public IStopwatch Create()
        {
            return new StopwatchWinRT();
        }

        private class StopwatchWinRT : IStopwatch
        {
            private readonly Stopwatch _stopWatch;

            public StopwatchWinRT()
            {
                this._stopWatch = new Stopwatch();
            }

            public void Stop()
            {
                this._stopWatch.Stop();
            }

            public void Reset()
            {
                this._stopWatch.Reset();
            }

            public void Start()
            {
                this._stopWatch.Start();
            }

            public long ElapsedMilliseconds
            {
                get { return this._stopWatch.ElapsedMilliseconds; }
            }
        }
    }
}