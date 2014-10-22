using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.ModelLib;
using SQLitePCL;

namespace Raysoft.Database
{
    public class AccountTableDbLogicLayer:BaseTableDbLogicLayer<Account,long>
    {
        #region 单例

        private AccountTableDbLogicLayer()
        {
        }

        private static AccountTableDbLogicLayer _instance;
        public static AccountTableDbLogicLayer Instance
        {
            get
            {
                lock (typeof(AccountTableDbLogicLayer))
                {
                    if (_instance == null)
                        _instance = new AccountTableDbLogicLayer();
                }

                return _instance;
            }
        }

        #endregion

        #region 重写父类的方法

        protected override string GetSelectAllSql()
        {
            return @"SELECT * FROM Account;";
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //nothing to do
        }

        protected override Account CreateItem(ISQLiteStatement statement)
        {
            var account = new Account()
            {
                Id = long.Parse(statement[0].ToString()),
                AccountDate = DateTime.Parse(statement[1].ToString()),
                AccountSum = float.Parse(statement[2].ToString()),
                Description = statement[3].ToString(),
                AccountType = int.Parse(statement[4].ToString()),
                SubCategoryId = int.Parse(statement[5].ToString()),
                MemberId = int.Parse(statement[6].ToString()),
                ABookId = int.Parse(statement[7].ToString()),
                AccountSourceId = int.Parse(statement[8].ToString()),
            };

            return account;
        }

        protected override string GetSelectItemSql()
        {
            return @"SELECT * FROM Account WHERE Id = ?";
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1,key);
        }

        protected override string GetDeleteItemSql()
        {
            return @"DELETE FROM Account WHERE Id = ?";
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, long key)
        {
            statement.Bind(1, key);
        }

        protected override string GetInsertItemSql()
        {
            return @"INSERT INTO Account  
                    (AccountDate, AccountSum, Description, AccountType, SubCategoryId, MemberId, ABookId, AccountSourceId) 
                     VALUES (@accountDate, @accountSum, @description, @accountType, @subCategoryId, @memberId,@aBookId, @accountSourceId)";
        }

        protected override void FillInsertStatement(ISQLiteStatement statement, Account item)
        {
            statement.Bind("@accountDate", item.AccountDate.ToString("yyyy-MM-dd HH:mm:ss"));
            statement.Bind("@accountSum", item.AccountSum);
            statement.Bind("@description", item.Description);
            statement.Bind("@accountType", item.AccountType);
            statement.Bind("@subCategoryId", item.SubCategoryId);
            statement.Bind("@memberId", item.MemberId);
            statement.Bind("@aBookId", item.ABookId);
            statement.Bind("@accountSourceId", item.AccountSourceId);
        }

        protected override string GetUpdateItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillUpdateStatement(ISQLiteStatement statement, long key, Account item)
        {
            throw new NotImplementedException();
        }

        protected override string GetUpdateItemColumnsSql(List<string> columnNameList)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE Account SET ");

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

        protected string GetSelectItemSqlByCondition(string condition)
        {
            switch (condition)
            {
                case "SubCategoryId":
                    return @"SELECT SubAccountCategory.Name, SUM(Account.AccountSum)
                             FROM Account,SubAccountCategory
                             WHERE Account.AccountType = ? AND Account.SubCategoryId = SubAccountCategory.Id
                             GROUP BY SubAccountCategory.Id";
                    break;
                case "AccountType":
                    return @"SELECT SUBSTR(Account.AccountDate,6,6), SUM(Account.AccountSum)
                             FROM Account
                             WHERE Account.AccountType = ?
                             GROUP BY SUBSTR(Account.AccountDate,6,6)";
                    break;
            }
            return @"SELECT * FROM SubAccountCategory WHERE CategoryId = ?";
        }

        protected void FillSelectItemStatementByCategoryId(ISQLiteStatement statement, int accountType)
        {
            statement.Bind(1, accountType);
        }

        public ObservableCollection<AccountStatisticsForBinding> GetAccountStatResultByCondition(string condition, int value,int accountType)
        {
            var items = new ObservableCollection<AccountStatisticsForBinding>();

            using (var statement = sqlConnection.Prepare(GetSelectItemSqlByCondition(condition)))
            {
                FillSelectItemStatementByCategoryId(statement, accountType);

                while (statement.Step() == SQLiteResult.ROW)
                {
                    var item = new AccountStatisticsForBinding()
                    {
                        StatisticItem = statement[0].ToString(),
                        AccountSum = float.Parse(statement[1].ToString()),
                    };
                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}
