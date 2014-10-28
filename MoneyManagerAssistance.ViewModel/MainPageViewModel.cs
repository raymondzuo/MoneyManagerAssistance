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
    public class MainPageViewModel:BaseViewModel
    {
        private ObservableCollection<AccountForBinding> accountForBindings;
        public ObservableCollection<AccountForBinding> AccountForBindings
        {
            get { return accountForBindings; }
            set { SetValue(ref accountForBindings, value); }
        }

        public void SetStatisticsResult(string statItem, Dictionary<string, string> conditionDictionary)
        {
            AccountForBindings = AccountTableDbLogicLayer.Instance.GetAccountDetailByCondition(statItem, conditionDictionary);
        }
    }
}
