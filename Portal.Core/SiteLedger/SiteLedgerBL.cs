using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using System.Transactions;
namespace Portal.Core
{
    public class SiteLedgerBL
    {
        
        public SiteLedgerBL()
        {
        
        }   
        public DataTable GetSiteLedgerDataTable(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, DateTime fromDate, DateTime toDate, int companyId, int customerId, int customerBranchId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtSiteLedger = new DataTable();
            try
            {
                dtSiteLedger = sqlDbInterface.GetSiteLedgerDetail(productTypeId,  assemblyType,  productMainGroupId, productSubGroupId,  productId, fromDate, toDate,companyId,customerId,customerBranchId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSiteLedger;
        }
       
    }
}
