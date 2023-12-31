﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ERPEntities : DbContext
    {
        public ERPEntities()
            : base("name=ERPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RoleUIActionMapping> RoleUIActionMappings { get; set; }
        public virtual DbSet<UserInterface> UserInterfaces { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<LeadSource> LeadSources { get; set; }
        public virtual DbSet<LeadStatu> LeadStatus { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductMainGroup> ProductMainGroups { get; set; }
        public virtual DbSet<ProductSubGroup> ProductSubGroups { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<UOM> UOMs { get; set; }
        public virtual DbSet<ProductOpeningStock> ProductOpeningStocks { get; set; }
        public virtual DbSet<ProductBOM> ProductBOMs { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<SLType> SLTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeFamilyInfo> EmployeeFamilyInfoes { get; set; }
        public virtual DbSet<EmployeePayInfo> EmployeePayInfoes { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<EmployeeReportingInfo> EmployeeReportingInfoes { get; set; }
        public virtual DbSet<EmployeeStateMapping> EmployeeStateMappings { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<LeadFollowUp> LeadFollowUps { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<FollowUpActivityType> FollowUpActivityTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBranch> CustomerBranches { get; set; }
        public virtual DbSet<CustomerProductMapping> CustomerProductMappings { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<GLMainGroup> GLMainGroups { get; set; }
        public virtual DbSet<GLSubGroup> GLSubGroups { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<QuotationProductDetail> QuotationProductDetails { get; set; }
        public virtual DbSet<QuotationSetting> QuotationSettings { get; set; }
        public virtual DbSet<QuotationSupportingDocument> QuotationSupportingDocuments { get; set; }
        public virtual DbSet<QuotationTaxDetail> QuotationTaxDetails { get; set; }
        public virtual DbSet<QuotationTermsDetail> QuotationTermsDetails { get; set; }
        public virtual DbSet<TermsTemplate> TermsTemplates { get; set; }
        public virtual DbSet<TermTemplateDetail> TermTemplateDetails { get; set; }
        public virtual DbSet<POProductDetail> POProductDetails { get; set; }
        public virtual DbSet<POSetting> POSettings { get; set; }
        public virtual DbSet<POSupportingDocument> POSupportingDocuments { get; set; }
        public virtual DbSet<POTermsDetail> POTermsDetails { get; set; }
        public virtual DbSet<PO> POes { get; set; }
        public virtual DbSet<POTaxDetail> POTaxDetails { get; set; }
        public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual DbSet<PurchaseInvoiceProductDetail> PurchaseInvoiceProductDetails { get; set; }
        public virtual DbSet<PurchaseInvoiceTaxDetail> PurchaseInvoiceTaxDetails { get; set; }
        public virtual DbSet<PurchaseInvoiceTermsDetail> PurchaseInvoiceTermsDetails { get; set; }
        public virtual DbSet<SaleInvoice> SaleInvoices { get; set; }
        public virtual DbSet<SaleInvoiceProductDetail> SaleInvoiceProductDetails { get; set; }
        public virtual DbSet<SaleInvoiceTaxDetail> SaleInvoiceTaxDetails { get; set; }
        public virtual DbSet<SaleInvoiceTermsDetail> SaleInvoiceTermsDetails { get; set; }
        public virtual DbSet<SO> SOes { get; set; }
        public virtual DbSet<SOProductDetail> SOProductDetails { get; set; }
        public virtual DbSet<SOTaxDetail> SOTaxDetails { get; set; }
        public virtual DbSet<SOTermsDetail> SOTermsDetails { get; set; }
        public virtual DbSet<ProductSubCategoryStateTaxMapping> ProductSubCategoryStateTaxMappings { get; set; }
        public virtual DbSet<CustomerPayment> CustomerPayments { get; set; }
        public virtual DbSet<SOSetting> SOSettings { get; set; }
        public virtual DbSet<VendorPayment> VendorPayments { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ComapnyBranch> ComapnyBranches { get; set; }
        public virtual DbSet<FormType> FormTypes { get; set; }
        public virtual DbSet<ChallanTaxDetail> ChallanTaxDetails { get; set; }
        public virtual DbSet<ChallanTermsDetail> ChallanTermsDetails { get; set; }
        public virtual DbSet<DeliveryChallan> DeliveryChallans { get; set; }
        public virtual DbSet<MRN> MRNs { get; set; }
        public virtual DbSet<MRNProductDetail> MRNProductDetails { get; set; }
        public virtual DbSet<ChallanProductDetail> ChallanProductDetails { get; set; }
        public virtual DbSet<ProductTrnStock> ProductTrnStocks { get; set; }
        public virtual DbSet<CustomerForm> CustomerForms { get; set; }
        public virtual DbSet<STN> STNs { get; set; }
        public virtual DbSet<STNProductDetail> STNProductDetails { get; set; }
        public virtual DbSet<STR> STRs { get; set; }
        public virtual DbSet<STRProductDetail> STRProductDetails { get; set; }
        public virtual DbSet<VendorProductMapping> VendorProductMappings { get; set; }
        public virtual DbSet<UserEmailSetting> UserEmailSettings { get; set; }
        public virtual DbSet<GL> GLs { get; set; }
        public virtual DbSet<GLDetail> GLDetails { get; set; }
        public virtual DbSet<VendorForm> VendorForms { get; set; }
        public virtual DbSet<SL> SLs { get; set; }
        public virtual DbSet<SLDetail> SLDetails { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<SubCostCenter> SubCostCenters { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; }
        public virtual DbSet<ProductGLMapping> ProductGLMappings { get; set; }
        public virtual DbSet<AdditionalTax> AdditionalTaxes { get; set; }
        public virtual DbSet<CustomerFollowUp> CustomerFollowUps { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<PISupportingDocument> PISupportingDocuments { get; set; }
        public virtual DbSet<MRNSupportingDocument> MRNSupportingDocuments { get; set; }
        public virtual DbSet<SOSupportingDocument> SOSupportingDocuments { get; set; }
        public virtual DbSet<DeliveryChallanSupportingDocument> DeliveryChallanSupportingDocuments { get; set; }
        public virtual DbSet<SaleInvoiceSupportingDocument> SaleInvoiceSupportingDocuments { get; set; }
        public virtual DbSet<STNSupportingDocument> STNSupportingDocuments { get; set; }
        public virtual DbSet<STRSupportingDocument> STRSupportingDocuments { get; set; }
        public virtual DbSet<VoucherSupportingDocument> VoucherSupportingDocuments { get; set; }
        public virtual DbSet<ChasisSerialMapping> ChasisSerialMappings { get; set; }
        public virtual DbSet<SaleInvoiceProductSerialDetail> SaleInvoiceProductSerialDetails { get; set; }
        public virtual DbSet<Thought> Thoughts { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<StockIssueNote> StockIssueNotes { get; set; }
        public virtual DbSet<StockIssueNoteProductDetail> StockIssueNoteProductDetails { get; set; }
        public virtual DbSet<StockIssueNoteSupportingDocument> StockIssueNoteSupportingDocuments { get; set; }
        public virtual DbSet<StickyNote> StickyNotes { get; set; }
        public virtual DbSet<ImageGallery> ImageGalleries { get; set; }
        public virtual DbSet<ImageGalleryDetail> ImageGalleryDetails { get; set; }
        public virtual DbSet<MailLog> MailLogs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<SaleReturn> SaleReturns { get; set; }
        public virtual DbSet<SaleReturnProductDetail> SaleReturnProductDetails { get; set; }
        public virtual DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public virtual DbSet<PurchaseReturnProductDetail> PurchaseReturnProductDetails { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<PurchaseQuotation> PurchaseQuotations { get; set; }
        public virtual DbSet<PurchaseQuotationProductDetail> PurchaseQuotationProductDetails { get; set; }
        public virtual DbSet<StoreRequisition> StoreRequisitions { get; set; }
        public virtual DbSet<StoreRequisitionProductDetail> StoreRequisitionProductDetails { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<ProductSerialDetail> ProductSerialDetails { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderProductDetail> WorkOrderProductDetails { get; set; }
        public virtual DbSet<WorkOrderProductSerialDetail> WorkOrderProductSerialDetails { get; set; }
        public virtual DbSet<Fabrication> Fabrications { get; set; }
        public virtual DbSet<FabricationBOMDetail> FabricationBOMDetails { get; set; }
        public virtual DbSet<FabricationProductDetail> FabricationProductDetails { get; set; }
        public virtual DbSet<WorkOrderBOMDetail> WorkOrderBOMDetails { get; set; }
        public virtual DbSet<PurchaseIndent> PurchaseIndents { get; set; }
        public virtual DbSet<PurchaseIndentProductDetail> PurchaseIndentProductDetails { get; set; }
        public virtual DbSet<SiteConsumptionDetail> SiteConsumptionDetails { get; set; }
        public virtual DbSet<SiteProductTrnStock> SiteProductTrnStocks { get; set; }
        public virtual DbSet<SiteConsumption> SiteConsumptions { get; set; }
        public virtual DbSet<CustomerSiteMRN> CustomerSiteMRNs { get; set; }
        public virtual DbSet<CustomerSiteMRNProductDetail> CustomerSiteMRNProductDetails { get; set; }
        public virtual DbSet<CustomerSiteMRNSupportingDocument> CustomerSiteMRNSupportingDocuments { get; set; }
        public virtual DbSet<SiteProductOpeningStock> SiteProductOpeningStocks { get; set; }
        public virtual DbSet<SiteProductSerialDetail> SiteProductSerialDetails { get; set; }
        public virtual DbSet<SiteChallanProductDetail> SiteChallanProductDetails { get; set; }
        public virtual DbSet<SiteChallanTermsDetail> SiteChallanTermsDetails { get; set; }
        public virtual DbSet<SiteDeliveryChallan> SiteDeliveryChallans { get; set; }
        public virtual DbSet<SiteDeliveryChallanSupportingDocument> SiteDeliveryChallanSupportingDocuments { get; set; }
        public virtual DbSet<EscalationMaster> EscalationMasters { get; set; }
        public virtual DbSet<DailyExpense> DailyExpenses { get; set; }
        public virtual DbSet<TimeType> TimeTypes { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
    
        public virtual ObjectResult<proc_GetRoleWiseChildUI_Result> proc_GetRoleWiseChildUI(Nullable<int> parentUIId, Nullable<int> roleId)
        {
            var parentUIIdParameter = parentUIId.HasValue ?
                new ObjectParameter("ParentUIId", parentUIId) :
                new ObjectParameter("ParentUIId", typeof(int));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_GetRoleWiseChildUI_Result>("proc_GetRoleWiseChildUI", parentUIIdParameter, roleIdParameter);
        }
    
        public virtual ObjectResult<proc_GetRoleWiseParentUI_Result> proc_GetRoleWiseParentUI(Nullable<int> roleId)
        {
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_GetRoleWiseParentUI_Result>("proc_GetRoleWiseParentUI", roleIdParameter);
        }
    
        public virtual int proc_GetCompanies(string companyName, string city, string panNo, string phoneNo, string tinNo)
        {
            var companyNameParameter = companyName != null ?
                new ObjectParameter("CompanyName", companyName) :
                new ObjectParameter("CompanyName", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var panNoParameter = panNo != null ?
                new ObjectParameter("PanNo", panNo) :
                new ObjectParameter("PanNo", typeof(string));
    
            var phoneNoParameter = phoneNo != null ?
                new ObjectParameter("PhoneNo", phoneNo) :
                new ObjectParameter("PhoneNo", typeof(string));
    
            var tinNoParameter = tinNo != null ?
                new ObjectParameter("TinNo", tinNo) :
                new ObjectParameter("TinNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_GetCompanies", companyNameParameter, cityParameter, panNoParameter, phoneNoParameter, tinNoParameter);
        }
    
        public virtual int proc_CopyAssemblyBOM(Nullable<long> copyFromAssemblyId, Nullable<long> copyToAssemblyId, Nullable<int> createdBy)
        {
            var copyFromAssemblyIdParameter = copyFromAssemblyId.HasValue ?
                new ObjectParameter("CopyFromAssemblyId", copyFromAssemblyId) :
                new ObjectParameter("CopyFromAssemblyId", typeof(long));
    
            var copyToAssemblyIdParameter = copyToAssemblyId.HasValue ?
                new ObjectParameter("CopyToAssemblyId", copyToAssemblyId) :
                new ObjectParameter("CopyToAssemblyId", typeof(long));
    
            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_CopyAssemblyBOM", copyFromAssemblyIdParameter, copyToAssemblyIdParameter, createdByParameter);
        }
    
        public virtual int proc_AddEditCustomerSiteMRN(Nullable<long> customerSiteMRNId, Nullable<System.DateTime> customerSiteMRNDate, string gRNo, Nullable<System.DateTime> gRDate, Nullable<int> requisitionId, string deliveryChallanNo, string pONo, Nullable<long> pOId, Nullable<int> vendorId, string vendorName, string contactPerson, string shippingContactPerson, string shippingBillingAddress, string shippingCity, Nullable<int> shippingStateId, Nullable<int> shippingCountryId, string shippingPinCode, string shippingTINNo, string shippingEmail, string shippingMobileNo, string shippingContactNo, string shippingFax, Nullable<int> companyBranchId, string dispatchRefNo, Nullable<System.DateTime> dispatchRefDate, string lRNO, Nullable<System.DateTime> lRDate, string approvalStatus, string transportVia, Nullable<int> noOfPackets, string remarks1, string remarks2, Nullable<int> finYearId, Nullable<int> companyId, Nullable<int> createdBy, Nullable<int> customerId, Nullable<int> customerBranchId, Nullable<bool> isLocal, ObjectParameter status, ObjectParameter message, ObjectParameter retCustomerSiteMRNId)
        {
            var customerSiteMRNIdParameter = customerSiteMRNId.HasValue ?
                new ObjectParameter("CustomerSiteMRNId", customerSiteMRNId) :
                new ObjectParameter("CustomerSiteMRNId", typeof(long));
    
            var customerSiteMRNDateParameter = customerSiteMRNDate.HasValue ?
                new ObjectParameter("CustomerSiteMRNDate", customerSiteMRNDate) :
                new ObjectParameter("CustomerSiteMRNDate", typeof(System.DateTime));
    
            var gRNoParameter = gRNo != null ?
                new ObjectParameter("GRNo", gRNo) :
                new ObjectParameter("GRNo", typeof(string));
    
            var gRDateParameter = gRDate.HasValue ?
                new ObjectParameter("GRDate", gRDate) :
                new ObjectParameter("GRDate", typeof(System.DateTime));
    
            var requisitionIdParameter = requisitionId.HasValue ?
                new ObjectParameter("RequisitionId", requisitionId) :
                new ObjectParameter("RequisitionId", typeof(int));
    
            var deliveryChallanNoParameter = deliveryChallanNo != null ?
                new ObjectParameter("DeliveryChallanNo", deliveryChallanNo) :
                new ObjectParameter("DeliveryChallanNo", typeof(string));
    
            var pONoParameter = pONo != null ?
                new ObjectParameter("PONo", pONo) :
                new ObjectParameter("PONo", typeof(string));
    
            var pOIdParameter = pOId.HasValue ?
                new ObjectParameter("POId", pOId) :
                new ObjectParameter("POId", typeof(long));
    
            var vendorIdParameter = vendorId.HasValue ?
                new ObjectParameter("VendorId", vendorId) :
                new ObjectParameter("VendorId", typeof(int));
    
            var vendorNameParameter = vendorName != null ?
                new ObjectParameter("VendorName", vendorName) :
                new ObjectParameter("VendorName", typeof(string));
    
            var contactPersonParameter = contactPerson != null ?
                new ObjectParameter("ContactPerson", contactPerson) :
                new ObjectParameter("ContactPerson", typeof(string));
    
            var shippingContactPersonParameter = shippingContactPerson != null ?
                new ObjectParameter("ShippingContactPerson", shippingContactPerson) :
                new ObjectParameter("ShippingContactPerson", typeof(string));
    
            var shippingBillingAddressParameter = shippingBillingAddress != null ?
                new ObjectParameter("ShippingBillingAddress", shippingBillingAddress) :
                new ObjectParameter("ShippingBillingAddress", typeof(string));
    
            var shippingCityParameter = shippingCity != null ?
                new ObjectParameter("ShippingCity", shippingCity) :
                new ObjectParameter("ShippingCity", typeof(string));
    
            var shippingStateIdParameter = shippingStateId.HasValue ?
                new ObjectParameter("ShippingStateId", shippingStateId) :
                new ObjectParameter("ShippingStateId", typeof(int));
    
            var shippingCountryIdParameter = shippingCountryId.HasValue ?
                new ObjectParameter("ShippingCountryId", shippingCountryId) :
                new ObjectParameter("ShippingCountryId", typeof(int));
    
            var shippingPinCodeParameter = shippingPinCode != null ?
                new ObjectParameter("ShippingPinCode", shippingPinCode) :
                new ObjectParameter("ShippingPinCode", typeof(string));
    
            var shippingTINNoParameter = shippingTINNo != null ?
                new ObjectParameter("ShippingTINNo", shippingTINNo) :
                new ObjectParameter("ShippingTINNo", typeof(string));
    
            var shippingEmailParameter = shippingEmail != null ?
                new ObjectParameter("ShippingEmail", shippingEmail) :
                new ObjectParameter("ShippingEmail", typeof(string));
    
            var shippingMobileNoParameter = shippingMobileNo != null ?
                new ObjectParameter("ShippingMobileNo", shippingMobileNo) :
                new ObjectParameter("ShippingMobileNo", typeof(string));
    
            var shippingContactNoParameter = shippingContactNo != null ?
                new ObjectParameter("ShippingContactNo", shippingContactNo) :
                new ObjectParameter("ShippingContactNo", typeof(string));
    
            var shippingFaxParameter = shippingFax != null ?
                new ObjectParameter("ShippingFax", shippingFax) :
                new ObjectParameter("ShippingFax", typeof(string));
    
            var companyBranchIdParameter = companyBranchId.HasValue ?
                new ObjectParameter("CompanyBranchId", companyBranchId) :
                new ObjectParameter("CompanyBranchId", typeof(int));
    
            var dispatchRefNoParameter = dispatchRefNo != null ?
                new ObjectParameter("DispatchRefNo", dispatchRefNo) :
                new ObjectParameter("DispatchRefNo", typeof(string));
    
            var dispatchRefDateParameter = dispatchRefDate.HasValue ?
                new ObjectParameter("DispatchRefDate", dispatchRefDate) :
                new ObjectParameter("DispatchRefDate", typeof(System.DateTime));
    
            var lRNOParameter = lRNO != null ?
                new ObjectParameter("LRNO", lRNO) :
                new ObjectParameter("LRNO", typeof(string));
    
            var lRDateParameter = lRDate.HasValue ?
                new ObjectParameter("LRDate", lRDate) :
                new ObjectParameter("LRDate", typeof(System.DateTime));
    
            var approvalStatusParameter = approvalStatus != null ?
                new ObjectParameter("ApprovalStatus", approvalStatus) :
                new ObjectParameter("ApprovalStatus", typeof(string));
    
            var transportViaParameter = transportVia != null ?
                new ObjectParameter("TransportVia", transportVia) :
                new ObjectParameter("TransportVia", typeof(string));
    
            var noOfPacketsParameter = noOfPackets.HasValue ?
                new ObjectParameter("NoOfPackets", noOfPackets) :
                new ObjectParameter("NoOfPackets", typeof(int));
    
            var remarks1Parameter = remarks1 != null ?
                new ObjectParameter("Remarks1", remarks1) :
                new ObjectParameter("Remarks1", typeof(string));
    
            var remarks2Parameter = remarks2 != null ?
                new ObjectParameter("Remarks2", remarks2) :
                new ObjectParameter("Remarks2", typeof(string));
    
            var finYearIdParameter = finYearId.HasValue ?
                new ObjectParameter("FinYearId", finYearId) :
                new ObjectParameter("FinYearId", typeof(int));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(int));
    
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            var customerBranchIdParameter = customerBranchId.HasValue ?
                new ObjectParameter("CustomerBranchId", customerBranchId) :
                new ObjectParameter("CustomerBranchId", typeof(int));
    
            var isLocalParameter = isLocal.HasValue ?
                new ObjectParameter("IsLocal", isLocal) :
                new ObjectParameter("IsLocal", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_AddEditCustomerSiteMRN", customerSiteMRNIdParameter, customerSiteMRNDateParameter, gRNoParameter, gRDateParameter, requisitionIdParameter, deliveryChallanNoParameter, pONoParameter, pOIdParameter, vendorIdParameter, vendorNameParameter, contactPersonParameter, shippingContactPersonParameter, shippingBillingAddressParameter, shippingCityParameter, shippingStateIdParameter, shippingCountryIdParameter, shippingPinCodeParameter, shippingTINNoParameter, shippingEmailParameter, shippingMobileNoParameter, shippingContactNoParameter, shippingFaxParameter, companyBranchIdParameter, dispatchRefNoParameter, dispatchRefDateParameter, lRNOParameter, lRDateParameter, approvalStatusParameter, transportViaParameter, noOfPacketsParameter, remarks1Parameter, remarks2Parameter, finYearIdParameter, companyIdParameter, createdByParameter, customerIdParameter, customerBranchIdParameter, isLocalParameter, status, message, retCustomerSiteMRNId);
        }
    
        public virtual int proc_CustomerSiteGetMRNs(string customerSiteMRNNo, string vendorName, string dispatchRefNo, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> companyId, string approvalStatus)
        {
            var customerSiteMRNNoParameter = customerSiteMRNNo != null ?
                new ObjectParameter("CustomerSiteMRNNo", customerSiteMRNNo) :
                new ObjectParameter("CustomerSiteMRNNo", typeof(string));
    
            var vendorNameParameter = vendorName != null ?
                new ObjectParameter("VendorName", vendorName) :
                new ObjectParameter("VendorName", typeof(string));
    
            var dispatchRefNoParameter = dispatchRefNo != null ?
                new ObjectParameter("DispatchRefNo", dispatchRefNo) :
                new ObjectParameter("DispatchRefNo", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("FromDate", fromDate) :
                new ObjectParameter("FromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("ToDate", toDate) :
                new ObjectParameter("ToDate", typeof(System.DateTime));
    
            var companyIdParameter = companyId.HasValue ?
                new ObjectParameter("CompanyId", companyId) :
                new ObjectParameter("CompanyId", typeof(int));
    
            var approvalStatusParameter = approvalStatus != null ?
                new ObjectParameter("ApprovalStatus", approvalStatus) :
                new ObjectParameter("ApprovalStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_CustomerSiteGetMRNs", customerSiteMRNNoParameter, vendorNameParameter, dispatchRefNoParameter, fromDateParameter, toDateParameter, companyIdParameter, approvalStatusParameter);
        }
    
        public virtual ObjectResult<proc_GetCustomerSiteMRNDetail_Result> proc_GetCustomerSiteMRNDetail(Nullable<int> customerSiteMRNId)
        {
            var customerSiteMRNIdParameter = customerSiteMRNId.HasValue ?
                new ObjectParameter("CustomerSiteMRNId", customerSiteMRNId) :
                new ObjectParameter("CustomerSiteMRNId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_GetCustomerSiteMRNDetail_Result>("proc_GetCustomerSiteMRNDetail", customerSiteMRNIdParameter);
        }
    
        public virtual ObjectResult<proc_GetCustomerSiteMRNProducts_Result> proc_GetCustomerSiteMRNProducts(Nullable<long> customerSiteMRNId)
        {
            var customerSiteMRNIdParameter = customerSiteMRNId.HasValue ?
                new ObjectParameter("CustomerSiteMRNId", customerSiteMRNId) :
                new ObjectParameter("CustomerSiteMRNId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_GetCustomerSiteMRNProducts_Result>("proc_GetCustomerSiteMRNProducts", customerSiteMRNIdParameter);
        }
    
        public virtual ObjectResult<proc_GetCustomerSiteMRNSupportingDocument_Result> proc_GetCustomerSiteMRNSupportingDocument(Nullable<long> customerSiteMrnId)
        {
            var customerSiteMrnIdParameter = customerSiteMrnId.HasValue ?
                new ObjectParameter("customerSiteMrnId", customerSiteMrnId) :
                new ObjectParameter("customerSiteMrnId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_GetCustomerSiteMRNSupportingDocument_Result>("proc_GetCustomerSiteMRNSupportingDocument", customerSiteMrnIdParameter);
        }
    
        public virtual int proc_UpdateIndentQtyOnCancelPO(Nullable<long> pOId)
        {
            var pOIdParameter = pOId.HasValue ?
                new ObjectParameter("POId", pOId) :
                new ObjectParameter("POId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_UpdateIndentQtyOnCancelPO", pOIdParameter);
        }
    
        public virtual int proc_UpdatePurchasePriceAndVendorId(Nullable<long> pOId)
        {
            var pOIdParameter = pOId.HasValue ?
                new ObjectParameter("POId", pOId) :
                new ObjectParameter("POId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_UpdatePurchasePriceAndVendorId", pOIdParameter);
        }
    }
}
