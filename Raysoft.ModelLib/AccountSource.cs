using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountSource : ModelBase
    {
        /// <summary>
        /// 账户来源
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户来源名（例如：支付宝，现金，银行卡）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户来源描述
        /// </summary>
        public string Description { get; set; }
    }
}
