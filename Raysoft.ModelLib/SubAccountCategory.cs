using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class SubAccountCategory : ModelBase
    {
        /// <summary>
        /// 账户小分类id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户小分类名（例如：汽车加油，酒水饮料）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户大分类类别id（例如：餐饮，交通）
        /// </summary>
        public int CategoryId { get; set; }
    }
}
