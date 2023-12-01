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
    public class CreditNoteBookPrintBL
    {
        
        public CreditNoteBookPrintBL()
        {
        
        }
        public ResponseOut GenerateCreditNoteBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                responseOut = sqlDbInterface.GenerateCreditNoteBook(bookId, companyId, finYearId, fromDate,  toDate, reportUserId, sessionId);
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

        public DataTable GetCreditNoteBookDetailDataTable(int reportUserId, string sessionId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtSaleBook = new DataTable();
            try
            {
                dtSaleBook = sqlDbInterface.GetCreditNoteBookDetail(reportUserId,sessionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSaleBook;
        }



    }
}








