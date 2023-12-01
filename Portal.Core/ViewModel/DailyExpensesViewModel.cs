using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
   public class DailyExpensesViewModel : IModel
    {        
        public int DailyExpensesId { get; set; }
        public string DailyExpenseCode { get; set; }
        public string DailyExpenseDate { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string SourcedFrom { get; set; }
        public long TimeTypeId { get; set; }
        public string TimeTypeName { get; set; }
        public decimal TimeTypeRate { get; set; }
        public decimal ConveyanceAmt { get; set; }
        public decimal FoodAndTeaExpenses { get; set; }
        public decimal Vages { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long CustomerBranchId { get; set; }
        public string CustomerBranchName { get; set; }
        public long CompanyBranchId { get; set; }
        public string CompanyBranchName { get; set; }
        public string DailyExpenseStatus { get; set; }
        public int CompanyId { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
