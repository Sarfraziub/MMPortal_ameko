using Portal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Common;
namespace Portal.Core.ViewModel
{
    public class ProductViewModel : IModel
    {
        public long Productid { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductShortDesc { get; set; }
        public string ProductFullDesc { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductMainGroupId { get; set; }
        public string ProductMainGroupName { get; set; }
        public int ProductSubGroupId { get; set; }
        public string ProductSubGroupName { get; set; }
        public string AssemblyType { get; set; }
        public int UOMId { get; set; }
        public string UOMName { get; set; }
        public int PurchaseUOMId { get; set; }
        public string PurchaseUOMName { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal LocalTaxRate { get; set; }
        public decimal CentralTaxRate { get; set; }
        public decimal OtherTaxRate { get; set; }
        public bool IsSerializedProduct { get; set; }
        public string BrandName { get; set; }
        public decimal ReOrderQty { get; set; }
        public decimal MinOrderQty { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public string ModifiedDate { get; set; }
        public bool Product_Status { get; set; }
        public int CompanyId { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string ProductPicPath { get; set; }
        public string ProductPicName { get; set; }

        public decimal CGST_Perc { get; set; }
        public decimal SGST_Perc { get; set; }
        public decimal IGST_Perc { get; set; }
        public string HSN_Code { get; set; }
        public bool GST_Exempt { get; set; }

        public int SizeId { get; set; }
        public string SizeDesc { get; set; }
        public string SizeCode { get; set; }
        public string Length { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerCode { get; set; }

        public long VendorId { get; set; }
        public int UnitId { get; set; }
        public string UnitValue { get; set; }

    }
    
}
