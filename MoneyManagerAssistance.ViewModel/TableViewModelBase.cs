using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.Phone.Common;
using SQLitePCL;

namespace MoneyManagerAssistance.ViewModel
{
    public abstract class TableViewModelBase<TItemType, TKeyType>:BaseViewModel
    {
        #region 抽象类定义
        /// <summary>
        /// 获取查询sql语句
        /// </summary>
        /// <returns></returns>
        protected abstract string GetSelectAllSql();
        /// <summary>
        /// 填充查询语句需要的参数
        /// </summary>
        /// <param name="statement"></param>
        protected abstract void FillSelectAllStatement(ISQLiteStatement statement);

        /// <summary>
        /// 生成查询对象
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        protected abstract TItemType CreateItem(ISQLiteStatement statement);

        /// <summary>
        /// 获取单条记录的sql
        /// </summary>
        /// <returns></returns>
        protected abstract string GetSelectItemSql();
        /// <summary>
        /// 填充查询单条记录的参数。
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="key"></param>
        protected abstract void FillSelectItemStatement(ISQLiteStatement statement, TKeyType key);

        /// <summary>
        /// 获取删除记录的sql
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDeleteItemSql();
        /// <summary>
        /// 填充删除记录所需的参数
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="key"></param>
        protected abstract void FillDeleteItemStatement(ISQLiteStatement statement, TKeyType key);

        /// <summary>
        /// 获取插入一条记录的参数
        /// </summary>
        /// <returns></returns>
        protected abstract string GetInsertItemSql();
        /// <summary>
        /// 填充插入一条记录所需参数
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="item"></param>
        protected abstract void FillInsertStatement(ISQLiteStatement statement, TItemType item);

        /// <summary>
        /// 获取更新一条记录的sql
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUpdateItemSql();
        /// <summary>
        /// 填充更新记录所需参数
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="key"></param>
        /// <param name="item"></param>
        protected abstract void FillUpdateStatement(ISQLiteStatement statement, TKeyType key, TItemType item);

        #endregion

        #region 字段
        protected DateTime lastModifiedTime = DateTime.MaxValue;
        public virtual DateTime Timestamp
        {
            get { return lastModifiedTime; }
            protected set { lastModifiedTime = value; }
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        private ISQLiteConnection sqlConnection { set; get; }
        public void SetSqlConnection(ISQLiteConnection conn)
        {
            this.sqlConnection = conn;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取所有的条目
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TItemType> GetAllItems()
        {
            var items = new ObservableCollection<TItemType>();
            using (var statement = sqlConnection.Prepare(GetSelectAllSql()))
            {
                FillSelectAllStatement(statement);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    var item = CreateItem(statement);
                    items.Add(item);
                }
            }
            Timestamp = DateTime.Now;

            return items;
        }

        /// <summary>
        /// 根据key获取某一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TItemType GetItem(TKeyType key)
        {
            using (var statement = sqlConnection.Prepare(GetSelectItemSql()))
            {
                FillSelectItemStatement(statement, key);

                if (statement.Step() == SQLiteResult.ROW)
                {
                    var item = CreateItem(statement);
                    Timestamp = DateTime.Now;
                    return item;
                }
            }

            throw new ArgumentOutOfRangeException("key not found");
        }

        /// <summary>
        /// 插入一条新条目
        /// </summary>
        /// <param name="item"></param>
        public void InsertItem(TItemType item)
        {
            using (var statement = sqlConnection.Prepare(GetInsertItemSql()))
            {
                FillInsertStatement(statement, item);
                statement.Step();
            }
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 根据key更新某条记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public void UpdateItem(TKeyType key, TItemType item)
        {
            using (var statement = sqlConnection.Prepare(GetUpdateItemSql()))
            {
                FillUpdateStatement(statement, key, item);
                statement.Step();
            }
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 根据key删除某条记录
        /// </summary>
        /// <param name="key"></param>
        public void DeleteItem(TKeyType key)
        {
            using (var statement = sqlConnection.Prepare(GetDeleteItemSql()))
            {
                FillDeleteItemStatement(statement, key);
                statement.Step();
            }
            Timestamp = DateTime.Now;
        }

        protected override string GetUpdateItemColumnsSql(List<string> columnNameList)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE Books SET ");

            foreach (var columnName in columnNameList)
            {
                stringBuilder.Append(columnName + " = ?,");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(" WHERE BookId = ? AND SiteId = ?");

            return stringBuilder.ToString();
        }

        protected override void FillUpdateColumnsStatement(ISQLiteStatement statement, Tuple<long, long> key, Dictionary<string, object> columnKeyValue)
        {
            int i = 1;

            foreach (var kv in columnKeyValue)
            {
                statement.Bind(i, kv.Value);
                i++;
            }

            statement.Bind(i, key.Item1);
            statement.Bind(i + 1, key.Item2);
        }
        #endregion
    }
}
