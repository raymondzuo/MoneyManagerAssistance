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
    public class AccountDetailViewModel:BaseViewModel
    {
        private ObservableCollection<AccountForBinding> accountForBindings;
        public ObservableCollection<AccountForBinding> AccountForBindings
        {
            get { return accountForBindings; }
            set { SetValue(ref accountForBindings, value); }
        }

        public void SetStatisticsResult(string statItem, int value, int accountType)
        {
            AccountForBindings = AccountTableDbLogicLayer.Instance.GetAccountDetailByCondition(statItem, value, accountType);
        }
    }
}
