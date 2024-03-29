﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Raysoft.ModelLib;
using Raysoft.Storage;

namespace Raysoft.Database
{
    using SQLitePCL;

    public class DbHelper
    {

        #region DDL
        /// <summary>
        /// 创建账本表的sql
        /// </summary>
        private const string sqlOfCreateAccountBookTable = @"CREATE TABLE IF NOT EXISTS
                                                             AccountBook (Id       INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                          Name          VARCHAR( 140 ),
                                                                          CreateTime    DATETIME,
                                                                          Description   VARCHAR( 200 ) );";

        /// <summary>
        /// 创建成员表的sql
        /// </summary>
        private const string sqlOfCreateMemberTable = @"CREATE TABLE IF NOT EXISTS
                                                        Member(    Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                   Name    VARCHAR( 140 ),
                                                                   Description VARCHAR( 200 ) );";

        /// <summary>
        /// 创建财务相关来源（比如支付宝/现金）的sql
        /// </summary>
        private const string sqlOfCreateAccoutSourceTable = @"CREATE TABLE IF NOT EXISTS
                                                              AccountSource(    Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                               Name    VARCHAR( 140 ),
                                                                               Description VARCHAR( 200 ) );";
        /// <summary>
        /// 创建账户类型表的sql，比如（收入，支出，借贷等）
        /// </summary>
        private const string sqlOfCreateAccoutTypeTable = @"CREATE TABLE IF NOT EXISTS
                                                            AccountType(Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                       Name    VARCHAR( 140 ),
                                                                       Description VARCHAR( 200 ) );";

        /// <summary>
        /// 创建财务分类的sql
        /// </summary>
        private const string sqlOfCreateAccountCategoryTable = @"CREATE TABLE IF NOT EXISTS
                                                                AccountCategory( Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                                 Name    VARCHAR( 140 ),
                                                                                 CategoryType INTEGER NOT NULL);";

        /// <summary>
        /// 创建财务子分类的sql
        /// </summary>
        private const string sqlOfCreateSubAccountCategoryTable = @"CREATE TABLE IF NOT EXISTS
                                                                    SubAccountCategory( Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                                        Name    VARCHAR( 140 ),
                                                                                        CategoryId    INTEGER NOT NULL,
                                                                                        FOREIGN KEY(CategoryId) REFERENCES AccountCategory(Id) ON DELETE CASCADE );";

        /// <summary>
        /// 创建账目表的sql
        /// </summary>
        private const string sqlOfCreateAccountTable = @"CREATE TABLE IF NOT EXISTS
                                                         Account( Id    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                                  AccountDate  DATETIME,
                                                                  AccountSum   FLOAT,
                                                                  Description  VARCHAR( 200 ),
                                                                  AccountType  INTEGER NOT NULL,
                                                                  SubCategoryId INTEGER NOT NULL,
                                                                  MemberId   INTEGER NOT NULL,
                                                                  ABookId    INTEGER NOT NULL,
                                                                  AccountSourceId    INTEGER NOT NULL,
                                                                  FOREIGN KEY(AccountType) REFERENCES AccountType(Id) ON DELETE CASCADE,
                                                                  FOREIGN KEY(MemberId) REFERENCES Member(Id) ON DELETE CASCADE,
                                                                  FOREIGN KEY(SubCategoryId) REFERENCES SubAccountCategory(Id) ON DELETE CASCADE,
                                                                  FOREIGN KEY(AccountSourceId) REFERENCES AccountSource(Id) ON DELETE CASCADE,
                                                                  FOREIGN KEY(ABookId) REFERENCES AccountBook(Id) ON DELETE CASCADE );";

        #endregion

        private static SQLiteConnection conn;

        #region 创建数据表
        public static async Task<ISQLiteConnection> InitOrOpenDatabase()
        {
            var dbFolder = await StorageHelper.CreateAndGetTheFolder("DataFolder");
            conn = new SQLiteConnection("DataFolder/MyAccount.db");

            return conn;
        }


        public static bool CreateAccountBookTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateAccountBookTable))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreateAccountSourceTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateAccoutSourceTable))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreateAccountTypeTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateAccoutTypeTable))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreateMemberTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateMemberTable))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreateAccountCategoryTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateAccountCategoryTable))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool  CreateSubAccountCategoryTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateSubAccountCategoryTable))
            {
                statement.Step();
            }

            var sql = @"PRAGMA foreign_keys = ON";
            using (var statement = conn.Prepare(sql))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CreateAccountTable()
        {
            using (var statement = conn.Prepare(sqlOfCreateAccountTable))
            {
                statement.Step();
            }

            var sql = @"PRAGMA foreign_keys = ON";
            using (var statement = conn.Prepare(sql))
            {
                var result = statement.Step();
                if (result.ToString().ToLower().Equals("done"))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region DML

        public static async Task<bool> InitMemberData()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var member = conn.Prepare("INSERT INTO Member (Name, Description) VALUES (?, ?)"))
                    {
                        member.Bind(1, "自己");
                        member.Bind(2, "我自己");


                        var result = member.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

            return false;
        }

        public static async Task<bool> InitAccountBook()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var abook = conn.Prepare("INSERT INTO AccountBook (Name, CreateTime, Description) VALUES (?, ?, ?)"))
                    {
                        abook.Bind(1, "账本1");
                        abook.Bind(2, "2014-05-01");
                        abook.Bind(3, "我自己用");


                        var result = abook.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

            return false;
        }

        public static async Task<bool> IniAccoutSourceData()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var accoutSource = conn.Prepare("INSERT INTO AccountSource (Name, Description) VALUES (?, ?)"))
                    {
                        accoutSource.Bind(1, "现金");
                        accoutSource.Bind(2, "已有现金");


                        var result = accoutSource.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

            return false;
        }

        public static async Task<bool> InitAccountCategoryData()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var member = conn.Prepare("INSERT INTO AccountCategory (Name, CategoryType) VALUES (?, ?)"))
                    {
                        member.Bind(1, "吃饭消费");
                        member.Bind(2, "1");


                        var result = member.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

            return false;
        }

        public static async Task<bool> InitSubAccountCategoryData()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var member = conn.Prepare("INSERT INTO SubAccountCategory (Name, CategoryId) VALUES (?, ?)"))
                    {
                        member.Bind(1, "餐馆");
                        member.Bind(2, "1");


                        var result = member.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

            return false;
        }
        /// <summary>
        /// 插入一条新账目
        /// </summary>
        /// <param name="accoutRecord"></param>
        /// <returns></returns>
        public static async Task<bool> InsertAccount(Account accoutRecord)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var account = conn.Prepare("INSERT INTO Account (AccountDate, AccountSum, Description, SubCategoryId, MemberId, ABookId, AccountSourceId, AccountType) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"))
                    {
                        account.ClearBindings();
                        account.Reset();
                        
                        account.Bind(1, accoutRecord.AccountDate);
                        account.Bind(2, accoutRecord.AccountSum);
                        account.Bind(3, accoutRecord.Description);
                        account.Bind(4, accoutRecord.SubCategoryId);
                        account.Bind(5, accoutRecord.MemberId);
                        account.Bind(6, accoutRecord.ABookId);
                        account.Bind(7, accoutRecord.AccountSourceId);
                        account.Bind(8, accoutRecord.AccountType);
                        
                        var result = account.Step();
                        var rs = result.ToString();
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> QueryAccount()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var account = conn.Prepare("Select * From Account"))
                    {
                        var result = account.Step();
                        var rs = result.ToString();
                        var count = account.DataCount;
                        for (int i = 0; i < account.DataCount; i++)
                        {
                            var a = account[1];
                        }

                        
                    }
                }
                );
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion






        public static void LoadDatabase(SQLiteConnection db)
        {
            string sql = @"CREATE TABLE IF NOT EXISTS
                               AccountBook (AccountId    INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Name    VARCHAR( 140 ),
                                            CreateTime    VARCHAR( 140 ),
                                            Description VARCHAR( 140 ) 
                            );";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            sql = @"CREATE TABLE IF NOT EXISTS
                                Project (Id          INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                         CustomerId  INTEGER,
                                         Name        VARCHAR( 140 ),
                                         Description VARCHAR( 140 ),
                                         DueDate     DATETIME,
                                         FOREIGN KEY(CustomerId) REFERENCES Customer(Id) ON DELETE CASCADE 
                            )";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            // Turn on Foreign Key constraints
            sql = @"PRAGMA foreign_keys = ON";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }
        }

        public async static Task ResetDataAsync(SQLiteConnection db)
        {
            // Empty the Customer and Project tables 
            string sql = @"DELETE FROM Project";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            sql = @"DELETE FROM Customer";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            List<Task> tasks = new List<Task>();

            // Add seed customers and projects
            var cust1Task = InsertCustomer(db, "Adventure Works", "Bellevue", "Mu Han");
            tasks.Add(cust1Task.ContinueWith((id) => InsertProject(db, id.Result, "Expense Reports", "Windows Store app", DateTime.Today.AddDays(4))));
            tasks.Add(cust1Task.ContinueWith((id) => InsertProject(db, id.Result, "Time Reporting", "Windows Store app", DateTime.Today.AddDays(14))));
            tasks.Add(cust1Task.ContinueWith((id) => InsertProject(db, id.Result, "Project Management", "Windows Store app", DateTime.Today.AddDays(24))));
            await Task.WhenAll(tasks.ToArray());

            tasks = new List<Task>();
            var cust2Task = InsertCustomer(db, "Contoso", "Seattle", "David Hamilton");
            tasks.Add(cust2Task.ContinueWith((id) => InsertProject(db, id.Result, "Soccer Scheduling", "Windows Phone app", DateTime.Today.AddDays(6))));
            await Task.WhenAll(tasks.ToArray());

            tasks = new List<Task>();
            var cust3Task = InsertCustomer(db, "Fabrikam", "Redmond", "Guido Pica");
            tasks.Add(cust3Task.ContinueWith((id) => InsertProject(db, id.Result, "Product Catalog", "MVC4 app", DateTime.Today.AddDays(4))));
            tasks.Add(cust3Task.ContinueWith((id) => InsertProject(db, id.Result, "Expense Reports", "Windows Store app", DateTime.Today.AddDays(-3))));
            tasks.Add(cust3Task.ContinueWith((id) => InsertProject(db, id.Result, "Expense Reports", "Windows Phone app", DateTime.Today.AddDays(45))));
            await Task.WhenAll(tasks.ToArray());

            tasks = new List<Task>();
            var cust4Task = InsertCustomer(db, "Tailspin Toys", "Kent", "Michelle Alexander");
            tasks.Add(cust4Task.ContinueWith((id) => InsertProject(db, id.Result, "Kids Game", "Windows Store app", DateTime.Today.AddDays(60))));
            await Task.WhenAll(tasks.ToArray());
        }

        private async static Task<long> InsertCustomer(ISQLiteConnection db, string customerName, string customerCity, string customerContact)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var custstmt = db.Prepare("INSERT INTO Customer (Name, City, Contact) VALUES (?, ?, ?)"))
                    {
                        custstmt.Bind(1, customerName);
                        custstmt.Bind(2, customerCity);
                        custstmt.Bind(3, customerContact);
                        custstmt.Step();

                    }
                }
                );
            }
            catch (Exception ex)
            {
                return 0;
            }

            using (var idstmt = db.Prepare("SELECT last_insert_rowid()"))
            {
                idstmt.Step();
                {
                    return (long)idstmt[0];
                }
                throw new Exception("Couldn't get inserted ID");
            };
        }

        private static Task InsertProject(ISQLiteConnection db, long customerID, string name, string description, DateTime duedate)
        {
            return Task.Run(() =>
            {

                using (var projstmt = db.Prepare("INSERT INTO Project (CustomerId, Name, Description, DueDate) VALUES (?, ?, ?, ?)"))
                {

                    // Reset the prepared statement so we can reuse it.
                    projstmt.ClearBindings();
                    projstmt.Reset();

                    projstmt.Bind(1, customerID);
                    projstmt.Bind(2, name);
                    projstmt.Bind(3, description);
                    projstmt.Bind(4, duedate.ToString("yyyy-MM-dd HH:mm:ss"));

                    projstmt.Step();
                }
            }
            );
        }

    }
}
