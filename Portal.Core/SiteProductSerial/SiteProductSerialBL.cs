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
    public class SiteProductSerialBL
    {
        DBInterface dbInterface;
        public SiteProductSerialBL()
        {
            dbInterface = new DBInterface();
        }

        public ResponseOut ImportSiteProductSerial(SiteProductSerialViewModel siteProductSerialViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SiteProductSerialDetail siteProductSerialDetail = new SiteProductSerialDetail
                {
                    SiteProductSerialId = siteProductSerialViewModel.SiteProductSerialId,
                    ProductId = siteProductSerialViewModel.ProductId,
                    ProductSerialNo = siteProductSerialViewModel.ProductSerialNo,
                    Serial1 = siteProductSerialViewModel.Serial1,
                    Serial2 = siteProductSerialViewModel.Serial2,
                    Serial3 = siteProductSerialViewModel.Serial3,
                    ProductSerialStatus = siteProductSerialViewModel.ProductSerialStatus,
                };
                responseOut = dbInterface.AddEditSiteProductSerial(siteProductSerialDetail);
            }
            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;
        }
    }
}
