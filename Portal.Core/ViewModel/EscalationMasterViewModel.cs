using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
   public class EscalationMasterViewModel : IModel
    {
        
        public int EscalationId { get; set; }
        public string ProcessType { get; set; }
        public int EscalationDays { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }
}
