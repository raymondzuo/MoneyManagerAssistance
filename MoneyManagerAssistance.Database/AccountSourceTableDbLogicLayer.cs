using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.ModelLib;
using SQLitePCL;

namespace Raysoft.Database
{
    public class AccountSourceTableDbLogicLayer : BaseTableDbLogicLayer<AccountSource, long>
    {
        #region 单例

        private AccountSourceTableDbLogicLayer()
        {
        }

        private static AccountSourceTableDbLogicLayer _instance;
        public static AccountSourceTableDbLogicLayer Instance
        {
            get
            {
                lock (typeof(AccountSourceTableDbLogicLayer))
                {
                    if (_instance == null)
                        _instance = new AccountSourceTableDbLogicLayer();
                }

                return _instance;
            }
        }

        #endregion

        #region 重写父类的方法

        protected override string GetSelectAllSql()
        {
            return @"SELECT * FROM AccountSource;";
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //nothing to do
        }

        protected override AccountSource CreateItem(ISQLiteStatement statement)
        {
            var accoutBook = new AccountSource()
            {
                Id = int.Parse(statement[0].ToString()),
                Name = statement[1].ToString(),
                Description = statement[2].ToString(),
            };

            return accoutBook;
        }

        protected override string GetSelectItemSql()
        {
            return @"SELECT * FROM AccountSource WHERE Id = ?";
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetDeleteItemSql()
        {
            return @"DELETE FROM AccountSource WHERE Id = ?";
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetInsertItemSql()
        {
            return @"INSERT INTO AccountSource  
                    (Name, Description) 
                     VALUES (@name, @description)";
        }

        protected override void FillInsertStatement(ISQLiteStatement statement, AccountSource item)
        {
            statement.Bind("@name",item.Name);
            statement.Bind("@description", item.Description);
        }

        protected override string GetUpdateItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillUpdateStatement(ISQLiteStatement statement, long key, AccountSource item)
        {
            throw new NotImplementedException();
        }

        protected override string GetUpdateItemColumnsSql(List<string> columnNameList)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE AccountSource SET ");

            foreach (var columnName in columnNameList)
            {
                stringBuilder.Append(columnName + " = ?,");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(" WHERE Id = ?");

            return stringBuilder.ToString();
        }

        protected override void FillUpdateColumnsStatement(ISQLiteStatement statement, long key, Dictionary<string, object> columnKeyValue)
        {
            int i = 1;

            foreach (var kv in columnKeyValue)
            {
                statement.Bind(i, kv.Value);
                i++;
            }

            statement.Bind(i, key);
        }

        #endregion
    }
}
