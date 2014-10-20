using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountCategoryForBinding:ModelBase
    {
        /// <summary>
        /// 账目的大分类
        /// </summary>
        public AccountCategory AccountCategory { get; set; }

        /// <summary>
        /// 大分类下分类集合
        /// </summary>
        public ObservableCollection<SubAccountCategory> SubAccountCategorys { get; set; }
    }
}
