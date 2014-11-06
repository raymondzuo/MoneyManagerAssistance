using Windows.Storage;
using SQLite.Net.Interop;

namespace DushuBus.Database.Sqlite.Platform
{
    public class SQLitePlatformWinRT : ISQLitePlatform
    {
        public SQLitePlatformWinRT()
        {
            this.SQLiteApi = new SQLiteApiWinRT();
            this.VolatileService = new VolatileServiceWinRT();
            this.StopwatchFactory = new StopwatchFactoryWinRT();
            this.ReflectionService = new ReflectionServiceWinRT();
        }

        public string DatabaseRootDirectory
        {
            get { return ApplicationData.Current.LocalFolder.Path; }
        }

        public ISQLiteApi SQLiteApi { get; private set; }
        public IStopwatchFactory StopwatchFactory { get; private set; }
        public IReflectionService ReflectionService { get; private set; }
        public IVolatileService VolatileService { get; private set; }
    }
}