using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.ModelLib;
using SQLitePCL;

namespace Raysoft.Database
{
    public class AccountBookTableDbLogicLayer : BaseTableDbLogicLayer<AccountBook,long>
    {
        #region 单例

        private AccountBookTableDbLogicLayer()
        {
        }

        private static AccountBookTableDbLogicLayer _instance;
        public static AccountBookTableDbLogicLayer Instance
        {
            get
            {
                lock (typeof(AccountBookTableDbLogicLayer))
                {
                    if (_instance == null)
                        _instance = new AccountBookTableDbLogicLayer();
                }

                return _instance;
            }
        }

        #endregion

        #region 重写父类的方法

        protected override string GetSelectAllSql()
        {
            return @"SELECT * FROM AccountBook;";
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //nothing to do
        }

        protected override AccountBook CreateItem(ISQLiteStatement statement)
        {
            var accoutBook = new AccountBook()
            {
                Id = int.Parse(statement[0].ToString()),
                Name = statement[1].ToString(),
                Description = statement[2].ToString(),
            };

            return accoutBook;
        }

        protected override string GetSelectItemSql()
        {
            return @"SELECT * FROM AccountBook WHERE Id = ?";
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetDeleteItemSql()
        {
            return @"DELETE FROM AccountBook WHERE Id = ?";
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetInsertItemSql()
        {
            return @"INSERT INTO AccountBook  
                    (Name, Description) 
                     VALUES (@name, @description)";
        }

        protected override void FillInsertStatement(ISQLiteStatement statement, AccountBook item)
        {
            statement.Bind("@name",item.Name);
            statement.Bind("@description", item.Description);
        }

        protected override string GetUpdateItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillUpdateStatement(ISQLiteStatement statement, long key, AccountBook item)
        {
            throw new NotImplementedException();
        }

        protected override string GetUpdateItemColumnsSql(List<string> columnNameList)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE AccountBook SET ");

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
