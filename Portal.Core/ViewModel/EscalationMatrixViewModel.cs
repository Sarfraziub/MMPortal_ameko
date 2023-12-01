using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
 public class EscalationMatrixViewModel
    {
        public List<EscalationCountViewModel> escalationCountList { get; set; }
    }
    public class EscalationCountViewModel
    {  
        public string EscalationCount_Head { get; set; }
        public string EscalationTotalCount { get; set; }

    }



}
