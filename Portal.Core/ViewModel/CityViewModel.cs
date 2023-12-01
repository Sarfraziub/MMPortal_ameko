using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
 public   class CityViewModel
    {
        public long CityId { get; set; }
        public string CityName { get; set; }
        public bool CityStatus { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }
}
