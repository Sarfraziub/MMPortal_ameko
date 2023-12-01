using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class UnitViewModel
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitShortName { get; set; }
        public int AllowDecimal { get; set; }
        public bool IsMultipleUnit { get; set; }
        public string UnitValue { get; set; }
        public Nullable<int> ParentId { get; set; }


    }



    public enum BaseUnits
    {
        Pieces = 1,
        Grams = 2,
        SERVICE,
        LITRES,
        TABLESPOON,

    }
}
