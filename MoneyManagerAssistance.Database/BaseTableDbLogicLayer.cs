using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Raysoft.Database
{
    public abstract class BaseTableDbLogicLayer<TItemType, TKeyType>
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

        /// <summary>
        /// 获取更新某几个字段的sql
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUpdateItemColumnsSql(List<string> columnNameList);

        /// <summary>
        /// 填充更新某几个字段的sql所需参数。
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="key"></param>
        /// <param name="columnKeyValue"></param>
        protected abstract void FillUpdateColumnsStatement(ISQLiteStatement statement, TKeyType key,
            Dictionary<string, object> columnKeyValue);


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
        protected ISQLiteConnection sqlConnection { set; get; }
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
        public bool InsertItem(TItemType item)
        {
            string result = string.Empty;

            using (var statement = sqlConnection.Prepare(GetInsertItemSql()))
            {
                FillInsertStatement(statement, item);
                result = statement.Step().ToString();
            }
            Timestamp = DateTime.Now;

            if (result.ToLower().Equals("done"))
                return true;
            
            return false;
        }

        public async Task<bool> InsertItemAsync(TItemType item)
        {
            string result = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    using (var statement = sqlConnection.Prepare(GetInsertItemSql()))
                    {
                        FillInsertStatement(statement, item);
                        result = statement.Step().ToString();
                        if (result.ToLower().Equals("done"))
                        {
                            Timestamp = DateTime.Now;
                            return true;
                        }
                        return false;
                    }
                });
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
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
        
        #endregion
    }
}
