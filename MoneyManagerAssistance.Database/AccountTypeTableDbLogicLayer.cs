using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.ModelLib;
using SQLitePCL;

namespace Raysoft.Database
{
    public class AccountTypeTableDbLogicLayer:BaseTableDbLogicLayer<AccountType,long>
    {
         #region 单例

        private AccountTypeTableDbLogicLayer()
        {
        }

        private static AccountTypeTableDbLogicLayer _instance;
        public static AccountTypeTableDbLogicLayer Instance
        {
            get
            {
                lock (typeof(AccountTypeTableDbLogicLayer))
                {
                    if (_instance == null)
                        _instance = new AccountTypeTableDbLogicLayer();
                }

                return _instance;
            }
        }

        #endregion

        #region 重写父类的方法

        protected override string GetSelectAllSql()
        {
            return @"SELECT * FROM AccountType;";
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //nothing to do
        }

        protected override AccountType CreateItem(ISQLiteStatement statement)
        {
            var accoutBook = new AccountType()
            {
                Id = int.Parse(statement[0].ToString()),
                Name = statement[1].ToString(),
                Description = statement[2].ToString(),
            };

            return accoutBook;
        }

        protected override string GetSelectItemSql()
        {
            return @"SELECT * FROM AccountType WHERE Id = ?";
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetDeleteItemSql()
        {
            return @"DELETE FROM AccountType WHERE Id = ?";
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetInsertItemSql()
        {
            return @"INSERT INTO AccountType  
                    (Name, Description) 
                     VALUES (@name, @description)";
        }

        protected override void FillInsertStatement(ISQLiteStatement statement, AccountType item)
        {
            statement.Bind("@name",item.Name);
            statement.Bind("@description", item.Description);
        }

        protected override string GetUpdateItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillUpdateStatement(ISQLiteStatement statement, long key, AccountType item)
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
