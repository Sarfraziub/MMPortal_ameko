using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
   public class TimeTypeViewModel : IModel
    {
        public long TimeTypeId { get; set;}
        public string TimeTypeName { get; set;}
        public string message { get; set; }
        public string status { get; set; }
    }
}
