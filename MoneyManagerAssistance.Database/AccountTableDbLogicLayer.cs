﻿using System;
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

        private string PrepareWhereStatement(bool isNeedWhereWord, Dictionary<string, string> conditionDictionary)
        {
            var sb = new StringBuilder();

            foreach (var condition in conditionDictionary)
            {
                if (isNeedWhereWord)
                {
                    sb.Append(string.Format(" WHERE {0} = ? ",condition.Key));
                    isNeedWhereWord = false;
                    continue;
                }

                sb.Append(string.Format(" AND {0} = ? ",condition.Key));
            }

            sb.Append(" ");
            return sb.ToString();
        }

        protected string GetSelectItemSqlByCondition(string condition, Dictionary<string, string> conditionDictionary)
        {
            switch (condition)
            {
                case "SubCategoryId":
                    return @"SELECT SubAccountCategory.Name, SUM(Account.AccountSum)
                             FROM Account,SubAccountCategory
                             WHERE Account.SubCategoryId = SubAccountCategory.Id" + PrepareWhereStatement(false, conditionDictionary) + 
                           @"GROUP BY SubAccountCategory.Id";
                    break;
                case "AccountType":
                    return @"SELECT SUBSTR(Account.AccountDate,6,6), SUM(Account.AccountSum)
                             FROM Account" + 
                             PrepareWhereStatement(true, conditionDictionary) 
                         + @"GROUP BY SUBSTR(Account.AccountDate,6,6)";
                    break;
                case "MemberId":
                    return @"SELECT Member.Name, SUM(Account.AccountSum)
                             FROM Account,Member
                             WHERE Account.MemberId = Member.Id" + PrepareWhereStatement(false, conditionDictionary)
                         + @"GROUP BY Account.MemberId";
                    break;
                case "DetailAll":
                    return @"SELECT Account.AccountDate,Member.Name,Account.Description,Account.AccountSum,AccountType.Name,AccountSource.Name,AccountBook.Name,SubAccountCategory.Name,AccountCategory.Name
                             FROM Account,AccountBook,SubAccountCategory,AccountSource,AccountType,AccountCategory,Member
                             WHERE Account.AccountType = AccountType.Id AND Account.SubCategoryId = SubAccountCategory.Id AND Account.ABookId = AccountBook.Id AND Account.AccountSourceId = AccountSource.Id AND AccountCategory.Id = SubAccountCategory.CategoryId AND Member.Id = Account.MemberId"
                           + PrepareWhereStatement(false, conditionDictionary);
                    break;
            }
            return @"SELECT * FROM SubAccountCategory WHERE CategoryId = ?";
        }

        protected void FillSelectItemStatementByCategoryId(ISQLiteStatement statement, Dictionary<string, string> conditionDictionary)
        {
            int i = 1;

            foreach (var condition in conditionDictionary)
            {
                statement.Bind(i,condition.Value);
                i++;
            }
        }

        public ObservableCollection<AccountStatisticsForBinding> GetAccountStatResultByCondition(string condition, Dictionary<string, string> conditionDictionary)
        {
            var items = new ObservableCollection<AccountStatisticsForBinding>();

            using (var statement = sqlConnection.Prepare(GetSelectItemSqlByCondition(condition,conditionDictionary)))
            {
                if (conditionDictionary.Count > 0)
                    FillSelectItemStatementByCategoryId(statement, conditionDictionary);

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

        public ObservableCollection<AccountForBinding> GetAccountDetailByCondition(string condition, Dictionary<string, string> conditionDictionary)
        {
            var items = new ObservableCollection<AccountForBinding>();

            using (var statement = sqlConnection.Prepare(GetSelectItemSqlByCondition(condition, conditionDictionary)))
            {
                if (conditionDictionary.Count > 0)
                    FillSelectItemStatementByCategoryId(statement, conditionDictionary);

                while (statement.Step() == SQLiteResult.ROW)
                {
                    var item = new AccountForBinding()
                    {
                        AccountDate = statement[0].ToString(),
                        MemberName = statement[1].ToString(),
                        Description = statement[2].ToString(),
                        AccountSum = float.Parse(statement[3].ToString()),
                        AccountTypeName = statement[4].ToString(),
                        AccountSourceName = statement[5].ToString(),
                        ABookName = statement[6].ToString(),
                        SubCategoryName = statement[7].ToString(),
                        CategoryName = statement[8].ToString(),
                    };
                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
