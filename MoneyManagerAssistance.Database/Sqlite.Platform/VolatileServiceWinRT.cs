using System.Threading;
using SQLite.Net.Interop;

namespace DushuBus.Database.Sqlite.Platform
{
    public class VolatileServiceWinRT : IVolatileService
    {
        public void Write(ref int transactionDepth, int depth)
        {
            Volatile.Write(ref transactionDepth, depth);
        }
    }
}