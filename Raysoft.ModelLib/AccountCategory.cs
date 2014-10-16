using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountCategory:ModelBase
    {
        /// <summary>
        /// 账户大分类id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户大分类名（例如：餐饮，交通）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户大分类类别（收入类别，支出类别）
        /// </summary>
        public int CategoryType { get; set; }
    }
}
