using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;
using SQLitePCL;

namespace MoneyManagerAssistance.ViewModel
{
    public class AccoutViewModel : BaseViewModel
    {
        private String dateformatString = "M/d/yyyy";

        public String DateFormat
        {
            get { return dateformatString; }
            set
            {
                dateformatString = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 保存一条新的账目记录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAccountRecord(Accout accoutRecord)
        {
            //var result = await DbHelper.InsertAccount(accoutRecord);
            return true;
        }

        public async Task<bool> GetAllAccountRecords()
        {
            //var result = await DbHelper.QueryAccount();
            return true;
        }
       
    }
}
