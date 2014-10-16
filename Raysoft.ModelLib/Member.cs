using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysoft.ModelLib
{
    public class Member:ModelBase
    {
        /// <summary>
        /// 成员id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 成员称谓
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 成员描述
        /// </summary>
        public string Description { get; set; }
    }
}
