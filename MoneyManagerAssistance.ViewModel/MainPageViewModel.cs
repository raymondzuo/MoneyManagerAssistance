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
        private ObservableCollection<AccountForBinding> inAccountForBindings;
        public ObservableCollection<AccountForBinding> InAccountForBindings
        {
            get { return inAccountForBindings; }
            set { SetValue(ref inAccountForBindings, value); }
        }

        private ObservableCollection<AccountForBinding> outAccountForBindings;
        public ObservableCollection<AccountForBinding> OutAccountForBindings
        {
            get { return outAccountForBindings; }
            set { SetValue(ref outAccountForBindings, value); }
        }

        private float inAccountSum;

        public float InAccountSum
        {
            get { return this.inAccountSum; }
            set { this.SetValue(ref inAccountSum, value); }
        }

        private float outAccountSum;

        public float OutAccountSum
        {
            get { return this.outAccountSum; }
            set { this.SetValue(ref outAccountSum, value); }
        }

        private MainPageViewModel()
        {
        }

        private static MainPageViewModel instance;

        public static MainPageViewModel Instance
        {
            get
            {
                lock (typeof(MainPageViewModel))
                {
                    if (instance == null)
                    {
                        instance = new MainPageViewModel();
                    }
                }
                return instance;
            }
        }

        public void SetInAccountResult(string statItem, Dictionary<string, string> conditionDictionary)
        {
            InAccountForBindings = AccountTableDbLogicLayer.Instance.GetAccountDetailByCondition(statItem, conditionDictionary);
            float inAcntSum = 0;

            foreach (var inAccountForBinding in InAccountForBindings)
            {
                inAcntSum += inAccountForBinding.AccountSum;
            }

            InAccountSum = inAcntSum;
        }

        public void SetOutAccountResult(string statItem, Dictionary<string, string> conditionDictionary)
        {
            OutAccountForBindings = AccountTableDbLogicLayer.Instance.GetAccountDetailByCondition(statItem, conditionDictionary);

            float outAcntSum = 0;

            foreach (var outAccountForBinding in OutAccountForBindings)
            {
                outAcntSum += outAccountForBinding.AccountSum;
            }

            OutAccountSum = outAcntSum;
        }
    }
}
