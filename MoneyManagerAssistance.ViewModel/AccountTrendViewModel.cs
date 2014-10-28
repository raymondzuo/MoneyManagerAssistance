using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.Database;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.ViewModel
{
    public class AccountTrendViewModel:BaseViewModel
    {
        private ObservableCollection<AccountStatisticsForBinding> accountOutTrendForBindings;
        public ObservableCollection<AccountStatisticsForBinding> AccountOutTrendForBindings
        {
            get { return accountOutTrendForBindings; }
            set { SetValue(ref accountOutTrendForBindings, value); }
        }

        private ObservableCollection<AccountStatisticsForBinding> accountInTrendForBindings;
        public ObservableCollection<AccountStatisticsForBinding> AccountInTrendForBindings
        {
            get { return accountInTrendForBindings; }
            set { SetValue(ref accountInTrendForBindings, value); }
        }

        public void SetTrendResult(string statItem, Dictionary<string,string> conditionDictionary)
        {
            if (conditionDictionary.ContainsKey("Account.AccountType"))
            {
                if (conditionDictionary["Account.AccountType"] == "1")
                    AccountOutTrendForBindings = AccountTableDbLogicLayer.Instance.GetAccountStatResultByCondition(statItem, conditionDictionary);
                else if (conditionDictionary["Account.AccountType"] == "2")
                    AccountInTrendForBindings = AccountTableDbLogicLayer.Instance.GetAccountStatResultByCondition(statItem, conditionDictionary);
            }
            
        }
    }
}
