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
    public class AccountCateSelectViewModel:BaseViewModel
    {
        private ObservableCollection<AccountCategory> accountCategorys;
        public ObservableCollection<AccountCategory> AccountCategorys
        {
            get { return accountCategorys; }
            set { SetValue(ref accountCategorys, value); }
        }


        private ObservableCollection<SubAccountCategory> subAccountCategory;
        public ObservableCollection<SubAccountCategory> SubAccountCategory
        {
            get { return subAccountCategory; }
            set { SetValue(ref subAccountCategory, value); }
        }

        private ObservableCollection<AccountCategoryForBinding> accountCategoryForBindings;
        public ObservableCollection<AccountCategoryForBinding> AccountCategoryForBindings
        {
            get { return accountCategoryForBindings; }
            set { SetValue(ref accountCategoryForBindings, value); }
        }

        public void SetAccountCategorys(int categoryType)
        {
            var categoryForBindings = new ObservableCollection<AccountCategoryForBinding>();
            AccountCategorys = AccountCategoryTableDbLogicLayer.Instance.GetAccountTypesByCategoryType(categoryType);
            
            foreach (var accountCategory in AccountCategorys)
            {
                var categoryForBinding = new AccountCategoryForBinding();
                categoryForBinding.AccountCategory = accountCategory;
                categoryForBinding.SubAccountCategorys = SubAccountCategoryTableDbLogicLayer.Instance.GetSubAccountTypesByCategoryType(accountCategory.Id);
                categoryForBindings.Add(categoryForBinding);
            }

            AccountCategoryForBindings = categoryForBindings;
        }
    }
}
