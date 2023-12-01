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

namespace Portal.Core
{
    public class EscalationMatrixBL
    {
        
        public EscalationMatrixBL()
        {
            
        }
        public List<EscalationCountViewModel> GetEscalationMatrixCountList(int companyId, int finYearId, out string escalationCountList,out string escalationTotalCountList)
        {            
            List<EscalationCountViewModel> escalationList = new List<EscalationCountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPOCount = sqlDbInterface.GetDashboard_EscalationMatrixCount(companyId, finYearId);
                if (dtPOCount != null && dtPOCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPOCount.Rows)
                    {
                        escalationList.Add(new EscalationCountViewModel
                        {                         
                            EscalationCount_Head = Convert.ToString(dr["Count_Head"]),                          
                            EscalationTotalCount = Convert.ToString(dr["TotalCount"])
                        });
                    }
                }
                var EscalcountHead = (from temp in escalationList
                                   select temp.EscalationCount_Head).ToList();

                var Escaltotal_Count = (from temp in escalationList
                                     select temp.EscalationTotalCount).ToList();

                escalationCountList = "'"+ string.Join("','", EscalcountHead) + "'";


                escalationTotalCountList = string.Join(",", Escaltotal_Count);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return escalationList;
        }

    }
}








