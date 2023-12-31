﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
    public class BankBookPrintBL
    {
        
        public BankBookPrintBL()
        {
        
        }
        public ResponseOut GenerateBankBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                responseOut = sqlDbInterface.GenerateBankBook(bookId, companyId, finYearId, fromDate,  toDate, reportUserId, sessionId);
            }

            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return responseOut;
        }

        public DataTable GetBankBookDetailDataTable(int reportUserId, string sessionId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtBankBook = new DataTable();
            try
            {
                dtBankBook = sqlDbInterface.GetBankBookDetail(reportUserId,sessionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtBankBook;
        }



    }
}








