using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class Account:ModelBase
    {
        /// <summary>
        /// 账目id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账目日期
        /// </summary>
        public DateTime AccountDate { get; set; }

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
        public int AccountType { get; set; }

        /// <summary>
        /// 账目来源（支付宝/现金/银行卡）
        /// </summary>
        public int AccountSourceId { get; set; }

        /// <summary>
        /// 账目子类别
        /// </summary>
        public int SubCategoryId { get; set; }

        /// <summary>
        /// 成员id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 账本id
        /// </summary>
        public int ABookId { get; set; }
    }
}
