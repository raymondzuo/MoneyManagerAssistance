using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountForBinding:ModelBase
    {
        /// <summary>
        /// 账目id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账目日期
        /// </summary>
        public string AccountDate { get; set; }

        /// <summary>
        /// 账目金额
        /// </summary>
        public float AccountSum { get; set; }

        /// <summary>
        /// 账目描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 账目类型（收入/支出）
        /// </summary>
        public string AccountTypeName { get; set; }

        /// <summary>
        /// 账目来源（支付宝/现金/银行卡）
        /// </summary>
        public string AccountSourceName { get; set; }

        /// <summary>
        /// 账目大类别
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 账目子类别
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// 成员
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 账本名
        /// </summary>
        public string ABookName { get; set; }
    }
}
