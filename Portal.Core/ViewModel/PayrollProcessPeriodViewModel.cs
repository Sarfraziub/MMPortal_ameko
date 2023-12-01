using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
    public class PayrollProcessPeriodViewModel: IModel
    {
        public int PayrollProcessingPeriodId { get; set; }
        public string PayrollProcessingStartDate { get; set; }
        public string PayrollProcessingEndDate { get; set; }
        public int MonthId { get; set; }
        public string MonthShortName { get; set; }
        public int CompanyId { get; set; }
        public int FinYearId { get; set; }
        public string FinYearCode { get; set; }
        public string PayrollProcessStatus { get; set; }
        public string PayrollProcessDate { get; set; }
        public string PayrollLocked { get; set; }
        public string PayrollLockDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public string message { get; set; }
        public bool status { get; set; } 
    }
    
}
