using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common
{
    public enum Roles { SuperAdmin=1,Admin=2,Other=3}
    public enum StatusMessage { Success, Fail };
    public enum FolderCreationMode { Year, Month, Day, Hour, Individual }
    public enum ProviderType { AT }
    public enum ErrorMode { Critical, Normal }
    public enum ErrorType { Error, Exception }
    public enum ProductMode { Common, Portal }
    public enum AccessMode { AddAccess=1, EditAccess=2,ViewAccess=3 }
    public enum RequestMode { GetPost=1,Ajax=2}

    public enum MailSendingMode { SaleInvoice=1, SO=2, Quotation=3, Challan=4, PO=5, BirthDay=6, PurchaseInvoice=7 }
}
