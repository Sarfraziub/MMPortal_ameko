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
    public class GLTrialBalancePrintBL
    {
        DBInterface dbInterface;
        public GLTrialBalancePrintBL()
        {
            dbInterface = new DBInterface();
        }
        #region Trial Balance 2 Column
        public ResponseOut GenerateGLTrialBalance2Column(int companyId, int finYearId, DateTime asOnDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                responseOut = sqlDbInterface.GenerateGLTrial2Column(companyId,finYearId, asOnDate, reportUserId, sessionId);
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
        public DataTable GetGLTrialBalance2ColumnDataTable(int reportUserId, string sessionId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTrialBalance = new DataTable();
            try
            {
                dtTrialBalance = sqlDbInterface.GetGLTrial2ColumnDetail(reportUserId,sessionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTrialBalance;
        }
        #endregion
    }
}








