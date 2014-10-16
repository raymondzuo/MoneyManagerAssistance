using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountType:ModelBase
    {
        /// <summary>
        /// 账户类型id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户类型名（例如：收入，支出，借贷）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户类型描述
        /// </summary>
        public string Description { get; set; }
    }
}
