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
    public class AccoutStatisticsViewModel:BaseViewModel
    {
        private ObservableCollection<AccountStatisticsForBinding> accountStatisticsForBindings;
        public ObservableCollection<AccountStatisticsForBinding> AccountStatisticsForBindings
        {
            get { return accountStatisticsForBindings; }
            set { SetValue(ref accountStatisticsForBindings, value); }
        }

        public void SetStatisticsResult(string statItem, Dictionary<string,string> conditionDictionary)
        {
            AccountStatisticsForBindings = AccountTableDbLogicLayer.Instance.GetAccountStatResultByCondition(statItem, conditionDictionary);
        }
    }
}
