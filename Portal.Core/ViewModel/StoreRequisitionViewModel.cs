using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
    public class StoreRequisitionViewModel
    {
        public long RequisitionId { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionDate { get; set; }
        public int FinYearId { get; set; }
        public string RequisitionType { get; set; }
        public int CompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public string BranchName { get; set; }
        public int RequisitionByUserId { get; set; }
        public string FullName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int CustomerBranchId { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }

        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public string RequisitionStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public int ApprovedBy { get; set; }
        public string ApprovedByUserName { get; set; }
        public string ApprovedDate { get; set; }
        public string RejectionStatus { get; set; }
        public int RejectedBy { get; set; }
        public string RejectedByUserName { get; set; }
        public string RejectedDate { get; set; }
        public string RejectedReason { get; set; }
        public int RequisitionSequence { get; set; }
        public long WorkOrderId { get; set; }
        public string WorkOrderNo { get; set; }
        public string WorkOrderDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string CustomerBranchName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string PendingDays { get; set; }
    }
    public class StoreRequisitionProductDetailViewModel
    {
        public int SequenceNo { get; set; }
        public string ProductName { get; set; }
        public long ProductMainGroupId { get; set; }
        public string ProductMainGroupName { get; set; }
        public string ProductCode { get; set; }
        public string UOMName { get; set; }
        public long RequisitionProductDetailId { get; set; }
        public long RequisitionId { get; set; }
        public long ProductId { get; set; }
        public string ProductShortDesc { get; set; }
        public decimal Quantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public decimal PendingQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalReceivedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal AcceptQuantity { get; set; }
        public decimal RejectQuantity { get; set; }
    }

    public class StoreRequisitionEscalationViewModel
    {
        public long RequisitionId { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionDate { get; set; }
        public int FinYearId { get; set; }
        public string RequisitionStatus { get; set; }
        public string PendingDuration { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string PendingWith { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string ReportingToUserEmail { get; set; }
    }

    public class PendingStoreRequisitionViewModel
    {
        public long RequisitionId { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionDate { get; set; }
        public string RequisitionType { get; set; }
        public string RequisitionStatus { get; set; }
        public string RequisitionApprovalStatus { get; set; }
        public string RequisitionApprovalDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSiteName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyBranchName { get; set; }
        public string SINStatus { get; set; }
        public string SINDate { get; set; }
        public string IndentStatus { get; set; }
        public string IndentDate { get; set; }
        public string IndentApprovalStatus { get; set; }
        public string IndentApprovalDate { get; set; }
        public string POStatus { get; set; }
        public string PODate { get; set; }
        public decimal POAmount { get; set; }
        public string POApprovalStatus { get; set; }
        public string POApprovalDate { get; set; }
        public string PIStatus { get; set; }
        public string PIDate { get; set; }
        public string MRNStatus { get; set; }
        public string MRNDate { get; set; }
        public string MRNCreatedBy { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyDate { get; set; }
    }
}

