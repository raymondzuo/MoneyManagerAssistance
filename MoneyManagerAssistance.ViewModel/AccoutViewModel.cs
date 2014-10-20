using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.Database;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;
using SQLitePCL;
using Raysoft.ModelLib;

namespace MoneyManagerAssistance.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        private String dateformatString = "M/d/yyyy";

        public String DateFormat
        {
            get { return dateformatString; }
            set
            {
                dateformatString = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Member> members;

        public ObservableCollection<Member> Members
        {
            get { return members; }
            set { SetValue(ref members, value); }
        }

        private ObservableCollection<AccountType> accountTypes;

        public ObservableCollection<AccountType> AccountTypes
        {
            get { return accountTypes; }
            set { SetValue(ref accountTypes, value); }
        }

        private ObservableCollection<AccountSource> accoutSources;

        public ObservableCollection<AccountSource> AccoutSources
        {
            get { return accoutSources; }
            set { SetValue(ref accoutSources, value); }
        }

        public AccountViewModel()
        {
            Members = GetMembersFromDb();
            AccountTypes = GetAccountTypeFromDb();
            AccoutSources = GetAccountSourceFromDb();
        }

        private ObservableCollection<Member> GetMembersFromDb()
        {
            return MemberTableDbLogicLayer.Instance.GetAllItems();
        }

        private ObservableCollection<AccountType> GetAccountTypeFromDb()
        {
            return AccountTypeTableDbLogicLayer.Instance.GetAllItems();
        }

        private ObservableCollection<AccountSource> GetAccountSourceFromDb()
        {
            return AccountSourceTableDbLogicLayer.Instance.GetAllItems();
        } 

        /// <summary>
        /// 保存一条新的账目记录
        /// </summary>
        /// <returns></returns>
        public bool SaveAccountRecord(Account accoutRecord)
        {
            return AccountTableDbLogicLayer.Instance.InsertItem(accoutRecord);
        }

        public async Task<bool> GetAllAccountRecords()
        {
            //var result = await DbHelper.QueryAccount();
            return true;
        }
       
    }
}
