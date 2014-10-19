using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.Database;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;
using Raysoft.Storage;
using SQLitePCL;

namespace MoneyManagerAssistance.ViewModel
{
    public class AppViewModel:BaseViewModel
    {
        #region 单例
        private AppViewModel()
        {

        }

        private static AppViewModel _instance;

        public static AppViewModel Instance
        {
            get
            {
                lock (typeof(AppViewModel))
                {
                    if (_instance == null)
                    {
                        _instance = new AppViewModel();
                    }
                }

                return _instance;
            }
        }

        private ISQLiteConnection conn;
        public void SetSqlConnection(ISQLiteConnection conn)
        {
            this.conn = conn;
        }

        #endregion

        #region 创建数据库以及数据表

        public async void CreateDatabaseTable()
        {
            if (StorageHelper.GetIsolatedValue("HasCreateAccountTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateAccountTable"))
            {
                if (DbHelper.CreateAccountTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateAccountTable",true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasCreateAccountBookTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateAccountBookTable"))
            {
                if (DbHelper.CreateAccountBookTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateAccountBookTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasCreateAccountCategoryTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateAccountCategoryTable"))
            {
                if (DbHelper.CreateAccountCategoryTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateAccountCategoryTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasCreateAccountSourceTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateAccountSourceTable"))
            {
                if (DbHelper.CreateAccountSourceTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateAccountSourceTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasCreateAccountTypeTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateAccountTypeTable"))
            {
                if (DbHelper.CreateAccountTypeTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateAccountTypeTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasCreateMemberTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasCreateMemberTable"))
            {
                if (DbHelper.CreateMemberTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasCreateMemberTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasSubAccountCategoryTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasSubAccountCategoryTable"))
            {
                if (DbHelper.CreateSubAccountCategoryTable())
                {
                    StorageHelper.SetIsolatedKeyValue("HasSubAccountCategoryTable", true);
                }
            }
        }

        #endregion

        #region 初始化各数据表的基础数据

        public void InitDbData(ISQLiteConnection conn)
        {
            if (StorageHelper.GetIsolatedValue("HasInitAccountTypeTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitAccountTypeTable"))
            {
                if (InitAccountType())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitAccountTypeTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasInitAccountBookTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitAccountBookTable"))
            {
                if (InitAccountBook())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitAccountBookTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasInitAccountCategoryTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitAccountCategoryTable"))
            {
                if (InitAccountCategoryData())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitAccountCategoryTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasInitAccountSourceTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitAccountSourceTable"))
            {
                if (InitAccoutSourceData())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitAccountSourceTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasInitMemberTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitMemberTable"))
            {
                if (InitMemberData())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitMemberTable", true);
                }
            }

            if (StorageHelper.GetIsolatedValue("HasInitSubAccountCategoryTable") == null ||
                !(bool)StorageHelper.GetIsolatedValue("HasInitSubAccountCategoryTable"))
            {
                if (InitSubAccountCategoryData())
                {
                    StorageHelper.SetIsolatedKeyValue("HasInitSubAccountCategoryTable", true);
                }
            }
        }

        public bool InitAccountType()
        {
            bool isAllSuccess = true;

            var dataList = new List<AccountType>()
            {
                new AccountType(){Name = "支出",Description = ""},
                new AccountType(){Name = "收入",Description = ""},
            };

            AccountTypeTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var accountType in dataList)
            {
                isAllSuccess = isAllSuccess && AccountTypeTableDbLogicLayer.Instance.InsertItem(accountType);
            }

            return isAllSuccess;
        }

        public bool InitMemberData()
        {
            bool isAllSuccess = true;

            var dataList = new List<Member>()
            {
                new Member(){Name = "本人",Description = ""},
                new Member(){Name = "爸爸",Description = ""},
                new Member(){Name = "妈妈",Description = ""},
                new Member(){Name = "爱人",Description = ""},
                new Member(){Name = "子女",Description = ""},
            };

            MemberTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var member in dataList)
            {
                isAllSuccess = isAllSuccess && MemberTableDbLogicLayer.Instance.InsertItem(member);
            }

            return isAllSuccess;
        }

        public bool InitAccountBook()
        {
            bool isAllSuccess = true;

            var dataList = new List<AccountBook>()
            {
                new AccountBook(){Name = "默认账本",Description = ""},
            };

            AccountBookTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var accountBook in dataList)
            {
                isAllSuccess = isAllSuccess && AccountBookTableDbLogicLayer.Instance.InsertItem(accountBook);
            }

            return isAllSuccess;
        }

        public bool InitAccoutSourceData()
        {
            bool isAllSuccess = true;

            var dataList = new List<AccountSource>()
            {
                new AccountSource(){Name = "现金",Description = ""},
                new AccountSource(){Name = "信用卡",Description = ""},
                new AccountSource(){Name = "银行卡（借记卡）",Description = ""},
                new AccountSource(){Name = "虚拟账户（支付宝，金融，证券等）",Description = ""},
            };

            AccountSourceTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var accountSource in dataList)
            {
                isAllSuccess = isAllSuccess && AccountSourceTableDbLogicLayer.Instance.InsertItem(accountSource);
            }

            return isAllSuccess;
        }

        public bool InitAccountCategoryData()
        {
            bool isAllSuccess = true;

            var dataList = new List<AccountCategory>()
            {
                new AccountCategory(){Name = "食品酒水类",CategoryType = 0},
                new AccountCategory(){Name = "服饰类",CategoryType = 0},
                new AccountCategory(){Name = "交通类",CategoryType = 0},
                new AccountCategory(){Name = "通信类",CategoryType = 0},
                new AccountCategory(){Name = "居家物业类",CategoryType = 0},
                new AccountCategory(){Name = "医疗保健类",CategoryType = 0},
                new AccountCategory(){Name = "娱乐休闲类",CategoryType = 0},
                new AccountCategory(){Name = "学习培训类",CategoryType = 0},
                new AccountCategory(){Name = "人情往来类",CategoryType = 0},
                new AccountCategory(){Name = "杂项",CategoryType = 0},

                new AccountCategory(){Name = "工薪收入类",CategoryType = 1},
                new AccountCategory(){Name = "其他收入",CategoryType = 1},
            };

            AccountCategoryTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var accountSource in dataList)
            {
                isAllSuccess = isAllSuccess && AccountCategoryTableDbLogicLayer.Instance.InsertItem(accountSource);
            }

            return isAllSuccess;
        }

        public bool InitSubAccountCategoryData()
        {
            bool isAllSuccess = true;

            var dataList = new List<SubAccountCategory>()
            {
                new SubAccountCategory(){Name = "日常三餐",CategoryId = 1},
                new SubAccountCategory(){Name = "水果零食",CategoryId = 1},
                new SubAccountCategory(){Name = "烟酒茶",CategoryId = 1},
                new SubAccountCategory(){Name = "其他",CategoryId = 1},

                new SubAccountCategory(){Name = "衣服裤子",CategoryId = 2},
                new SubAccountCategory(){Name = "鞋帽包包",CategoryId = 2},
                new SubAccountCategory(){Name = "化妆饰品",CategoryId = 2},

                new SubAccountCategory(){Name = "公共交通",CategoryId = 3},
                new SubAccountCategory(){Name = "打车租车",CategoryId = 3},
                new SubAccountCategory(){Name = "私家车支出",CategoryId = 3},

                new SubAccountCategory(){Name = "上网费用",CategoryId = 4},
                new SubAccountCategory(){Name = "手机话费",CategoryId = 4},
                new SubAccountCategory(){Name = "座机费",CategoryId = 4},

                new SubAccountCategory(){Name = "日常用品",CategoryId = 5},
                new SubAccountCategory(){Name = "水电煤气",CategoryId = 5},
                new SubAccountCategory(){Name = "房租",CategoryId = 5},
                new SubAccountCategory(){Name = "物业管理",CategoryId = 5},
                new SubAccountCategory(){Name = "维修保养",CategoryId = 5},

                new SubAccountCategory(){Name = "治疗费",CategoryId = 6},
                new SubAccountCategory(){Name = "医药费",CategoryId = 6},
                new SubAccountCategory(){Name = "保健费",CategoryId = 6},
                new SubAccountCategory(){Name = "美容费",CategoryId = 6},

                new SubAccountCategory(){Name = "运动健身",CategoryId = 7},
                new SubAccountCategory(){Name = "休闲玩乐",CategoryId = 7},
                new SubAccountCategory(){Name = "腐败聚会",CategoryId = 7},
                new SubAccountCategory(){Name = "旅游度假",CategoryId = 7},
                new SubAccountCategory(){Name = "宠物宝贝",CategoryId = 7},

                new SubAccountCategory(){Name = "培训进修",CategoryId = 8},
                new SubAccountCategory(){Name = "书刊杂志",CategoryId = 8},
                new SubAccountCategory(){Name = "数码装备",CategoryId = 8},

                new SubAccountCategory(){Name = "送礼请客",CategoryId = 9},
                new SubAccountCategory(){Name = "孝敬父母",CategoryId = 9},
                new SubAccountCategory(){Name = "慈善捐助",CategoryId = 9},
                new SubAccountCategory(){Name = "还人钱物",CategoryId = 9},

                new SubAccountCategory(){Name = "意外丢失",CategoryId = 10},
                new SubAccountCategory(){Name = "烂账损失",CategoryId = 10},

                new SubAccountCategory(){Name = "工资收入",CategoryId = 11},
                new SubAccountCategory(){Name = "利息收入",CategoryId = 11},
                new SubAccountCategory(){Name = "加班收入",CategoryId = 11},
                new SubAccountCategory(){Name = "奖金收入",CategoryId = 11},
                new SubAccountCategory(){Name = "投资收入",CategoryId = 11},
                new SubAccountCategory(){Name = "兼职收入",CategoryId = 11},

                new SubAccountCategory(){Name = "礼金收入",CategoryId = 12},
                new SubAccountCategory(){Name = "经营收入",CategoryId = 12},
                new SubAccountCategory(){Name = "中奖收入",CategoryId = 12},
                new SubAccountCategory(){Name = "意外收入",CategoryId = 12},
            };

            SubAccountCategoryTableDbLogicLayer.Instance.SetSqlConnection(conn);

            foreach (var subAccountCategory in dataList)
            {
                isAllSuccess = isAllSuccess && SubAccountCategoryTableDbLogicLayer.Instance.InsertItem(subAccountCategory);
            }

            return isAllSuccess;
        }


        #endregion
    }
}
