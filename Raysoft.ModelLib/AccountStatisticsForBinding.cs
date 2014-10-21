using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class AccountStatisticsForBinding:ModelBase
    {
        private string statisticItem;
        /// <summary>
        /// 统计项目
        /// </summary>
        public string StatisticItem {
            get { return this.statisticItem; }
            set { SetProperty(ref statisticItem, value); }
        }

        private float accountSum;
        /// <summary>
        /// 按某类别查询之后分组的总账目金额
        /// </summary>
        public float AccountSum
        {
            get { return this.accountSum; }
            set { SetProperty(ref accountSum, value); }
        }
    }
}
