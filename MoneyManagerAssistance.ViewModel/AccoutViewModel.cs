using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManagerAssistance.ViewModel;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;
using SQLitePCL;

namespace MoneyManagerAssistance.ViewModels
{
    public class AccoutViewModel : TableViewModelBase<AccoutRecordModel, long>
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
        public async Task<bool> SaveAccountRecord(AccoutRecordModel accoutRecord)
        {
            //var result = await DbHelper.InsertAccount(accoutRecord);
            return true;
        }

        public async Task<bool> GetAllAccountRecords()
        {
            //var result = await DbHelper.QueryAccount();
            return true;
        }

        protected override string GetSelectAllSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            throw new NotImplementedException();
        }

        protected override AccoutRecordModel CreateItem(ISQLiteStatement statement)
        {
            throw new NotImplementedException();
        }

        protected override string GetSelectItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, long key)
        {
            throw new NotImplementedException();
        }

        protected override string GetDeleteItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, long key)
        {
            throw new NotImplementedException();
        }

        protected override string GetInsertItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillInsertStatement(ISQLiteStatement statement, AccoutRecordModel item)
        {
            throw new NotImplementedException();
        }

        protected override string GetUpdateItemSql()
        {
            throw new NotImplementedException();
        }

        protected override void FillUpdateStatement(ISQLiteStatement statement, long key, AccoutRecordModel item)
        {
            throw new NotImplementedException();
        }
    }
}
