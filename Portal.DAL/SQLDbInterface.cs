using Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;
using Portal.DAL.Infrastructure;
namespace Portal.DAL
{
    public class SQLDbInterface : IDisposable
    {
        private readonly string connectionString = "";
        public SQLDbInterface()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SQLConnStr"].ConnectionString;
        }
        #region Dispose Methods
        public void Dispose()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
        }

        #endregion
        #region Company
        public DataTable GetCompanyList(string companyName, string cityName, string panNo, string phoneNo, string tinNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanies", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    da.SelectCommand.Parameters.AddWithValue("@City", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@PanNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@TinNo", tinNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCompanyDetail(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanyDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCountryList(string countryName, string countryCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCountries", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CountryName", countryName);
                    da.SelectCommand.Parameters.AddWithValue("@CountryCode", countryCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCountryDetail(int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCountryDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region User
        public DataTable GetUserList(string userName, int companyid, int roleId, string fullName, string phoneNo, string email, int userRole)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (userRole == (int)Roles.SuperAdmin)
                    {
                        da = new SqlDataAdapter("proc_GetPrimaryUsers", con);
                    }
                    else
                    {
                        da = new SqlDataAdapter("proc_GetUsers", con);
                    }
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserName", userName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.SelectCommand.Parameters.AddWithValue("@FullName", fullName);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@Email", email);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserDetail(int userId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GetDashboardUser()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDashboardUsers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboardUsersDetails(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetUserDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public int GetTodayUsersCount(int companyId)
        {
            int count = 0;
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayUsersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        count = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return count;
        }

        public int GetTotalUsersCount(int companyId)
        {
            int count = 0;
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTotalUsersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        count = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return count;
        }

        #endregion
        #region Fin Year
        public DataTable GetFinYearList()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFinYear", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region CompanyBranch
        public DataTable GetCompanyBranchList(string BranchName, string cityName, string panNo, string phoneNo, string tinNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompaniesBranch", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchName", BranchName);
                    da.SelectCommand.Parameters.AddWithValue("@City", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@PanNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneNo);
                    da.SelectCommand.Parameters.AddWithValue("@TinNo", tinNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCompanyBranchDetail(int companyBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCompanyBranchDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region State 
        public DataTable GetStateList(string stateName, string stateCode, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@StateName", stateName);
                    da.SelectCommand.Parameters.AddWithValue("@StateCode", stateCode);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetStateDetail(int stateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStateDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region City 
        public DataTable GetCityList(string cityName, int stateId, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCities", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CityName", cityName);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCityDetail(int CityId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCityDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CityId", CityId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 

        #region Role 
        public DataTable GetRoleList(string roleName, string roleDesc, string isAdmin, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoles", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoleName", roleName);
                    da.SelectCommand.Parameters.AddWithValue("@RoleDesc", roleDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@IsAdmin", isAdmin);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetRoleDetail(int roleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoleDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetDashboardRolesDetails(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetRoleDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public int GetTotalRolesCount(int companyId)
        {
            int count = 0;
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTotalRolesCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        count = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return count;
        }
        #endregion

        #region RoleUIMapping
        public DataTable GetRoleUIActionMappingDetail(string interfaceType, string interfaceSubType, int roleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoleUIActionMappingDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InterfaceType", interfaceType);
                    da.SelectCommand.Parameters.AddWithValue("@InterfaceSubType", interfaceSubType);
                    da.SelectCommand.Parameters.AddWithValue("@RoleId", roleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion


        #region LeadStatus
        public DataTable GetLeadStatusList(string leadstatusName, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadStatuses", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusName", leadstatusName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadStatusDetail(int leadstatusId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadStatusDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusId", leadstatusId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region LeadSource
        public DataTable GetLeadSourceList(string leadsourceName, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadSources", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadsourceName", leadsourceName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadSourceDetail(int leadsourceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadSourceDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadSourceId", leadsourceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region ProductType
        public DataTable GetProductTypeList(string producttypeName, string producttypeCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeName", producttypeName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeCode", producttypeCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetProductTypeDetail(int producttypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", producttypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region ClaimType
        public DataTable GetClaimTypeList(string claimtypeName, string claimNature, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetClaimTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ClaimTypeName", claimtypeName);
                    da.SelectCommand.Parameters.AddWithValue("@ClaimNature", claimNature);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetClaimTypeDetail(int claimtypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetClaimTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ClaimTypeId", claimtypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeClaimApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeClaimApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region LeaveType
        public DataTable GetLeaveTypeList(string leaveTypeName, string leaveTypeCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeaveTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeaveTypeName", leaveTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@LeaveTypeCode", leaveTypeCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeaveTypeDetail(int leaveTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeaveTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeLeaveApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeLeaveApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region Education
        public DataTable GetEducationList(string educationName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEducation", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EducationName", educationName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEducationDetail(int educationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEducationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EducationId", educationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region ShiftType
        public DataTable GetShiftTypeList(string shiftTypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetShiftType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ShiftTypeName", shiftTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetShiftTypeDetail(int shiftTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetShiftTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ShiftTypeId", shiftTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Language
        public DataTable GetLanguageList(string languageName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLanguage", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LanguageName", languageName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLanguageDetail(int languageId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLanguageDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LanguageId", languageId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Religion
        public DataTable GetReligionList(string religionName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetReligion", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReligionName", religionName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetReligionDetail(int religionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetReligionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReligionId", religionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Product
        public DataTable GetProductList(string productName, int companyid, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, int productSubGroupId, string assemblyType, string brandName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@ProductCode", productCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductShortDesc", productShortDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductFullDesc", productFullDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@BrandName", brandName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductDetail(long productId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetReorderPointProductCountList(string productName, string productCode, string productShortDesc, string productFullDesc, int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetReorderPointProductCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductCode", productCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductShortDesc", productShortDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductFullDesc", productFullDesc);
                    //da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    //da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    //da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    //da.SelectCommand.Parameters.AddWithValue("@BrandName", brandName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductMainGroupNameByProductID(long productId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductMainGroupNameByProductID", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region PaymentTerm  
        public DataTable GetPaymentTermList(string paymenttermDesc, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTermDesc", paymenttermDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPaymentTermDetail(int paymenttermId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentTermDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTermId", paymenttermId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 


        #region Department 
        public DataTable GetDepartmentList(string departmentName, string departmentCode, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDepartments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentName", departmentName);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetDepartmentDetail(int departmentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDepartmentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@departmentId", departmentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion




        #region Product Opening Stock
        public DataTable GetProductOpeningList(string productName, int companyid, int finYearId, int companyBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductOpenings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@companyBranchId", companyBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductOpeningDetail(long openingTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductOpeningDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@OpeningTrnId", openingTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Product BOM
        public DataTable GetAssemblyList(string assemblyName, string assemblyType, int companyid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssemblyList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyName", assemblyName);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetAssemblyBOMList(long assemblyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssemblyBOMList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyId", assemblyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductBOMDetail(long bomId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductBOMDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BOMId", bomId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetBOMListReport(long assemblyId, string assemblyType, int companyid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBOMListReport", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyId", assemblyId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Product Main Group 
        public DataTable GetProductMainGroupList(string productmaingroupName, string productmaingroupCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductMainGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupName", productmaingroupName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupCode", productmaingroupCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductMainGroupDetail(int productmaingroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductMainGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productmaingroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Product Sub Group 
        public DataTable GetProductSubGroupList(string productsubgroupName, string productsubgroupCode, int productmaingroupId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupName", productsubgroupName);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupCode", productsubgroupCode);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productmaingroupId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductSubGroupDetail(int productsubgroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductSubGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productsubgroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 

        #region UOM 
        public DataTable GetUOMList(string uomName, string uomDesc, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUOMs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UOMName", uomName);
                    da.SelectCommand.Parameters.AddWithValue("@UOMDesc", uomDesc);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetUOMDetail(int uomId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUOMDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UOMId", uomId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Designation 
        public DataTable GetDesignationList(string designationName, string designationCode, int departmentId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDesignations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DesignationName", designationName);
                    da.SelectCommand.Parameters.AddWithValue("@DesignationCode", designationCode);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetDesignationDetail(int designationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDesignationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@designationId", designationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion 

        #region PaymentMode 
        public DataTable GetPaymentModeList(string paymentModeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentMode", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeName", paymentModeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetPaymentModeDetail(int paymentModeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPaymentModeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region SLType 
        public DataTable GetSLTypeList(string sLTypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeName", sLTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSLTypeDetail(int sLTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", sLTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Services 
        public DataTable GetServicesList(string servicesName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetServices", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ServicesName", servicesName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetServicesDetail(int servicesId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetServicesDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ServicesId", servicesId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region EmployeeStateMapping
        public DataTable GetStateMap(int employeeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStates_EmpStateMap", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Lead 
        public DataTable GetEmployeeId(string userEmail)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployeeIdByEmail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserEmail", userEmail);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadList(string leadCode, string companyName, string contactPersonName, string Email, string contactNo, string companyAddress, string companyCity, int companyStateId, int leadStatusId, int leadSourceId, int userId, string status, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeads", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadCode", leadCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    da.SelectCommand.Parameters.AddWithValue("@ContactPersonName", contactPersonName);
                    da.SelectCommand.Parameters.AddWithValue("@Email", Email);
                    da.SelectCommand.Parameters.AddWithValue("@ContactNo", contactNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyAddress", companyAddress);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyCity", companyCity);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyStateId", companyStateId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadSourceId", leadSourceId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusId", leadStatusId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLeadDetail(int leadId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadId", leadId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetLeadStateMap(string EmployeeUserName, int countryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStateLead", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserName", EmployeeUserName);
                    da.SelectCommand.Parameters.AddWithValue("@CountryId", countryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetTeamDetailList(int reportingUserId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTeamDetailList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GenerateLeadReport(string leadCode, string companyName, string contactPersonName, string Email, string contactNo, string companyAddress, string companyCity, int companyStateId, int leadStatusId, int leadSourceId, int userId, string status, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeads", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LeadCode", leadCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    da.SelectCommand.Parameters.AddWithValue("@ContactPersonName", contactPersonName);
                    da.SelectCommand.Parameters.AddWithValue("@Email", Email);
                    da.SelectCommand.Parameters.AddWithValue("@ContactNo", contactNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyAddress", companyAddress);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyCity", companyCity);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyStateId", companyStateId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadSourceId", leadSourceId);
                    da.SelectCommand.Parameters.AddWithValue("@LeadStatusId", leadStatusId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion


        #region Tax 
        public DataTable GetTaxList(string taxName = "", string taxType = "0", string taxSubType = "0", int companyId = 0, string status = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TaxName", taxName);
                    da.SelectCommand.Parameters.AddWithValue("@TaxType", taxType);
                    da.SelectCommand.Parameters.AddWithValue("@TaxSubType", taxSubType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTaxDetail(int taxId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTaxDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@taxId", taxId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Additional Tax 
        public DataTable GetAdditionalTaxList(string taxName = "", int companyId = 0, string status = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAdditionalTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AddTaxName", taxName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetAdditionalTaxDetail(int taxId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAdditionalTaxDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AddtaxId", taxId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion 

        #region Employee
        public DataTable GetEmployeeList(string employeeName, string employeeCode, string mobileNo, string email, string panNo, int departmentId, string employeeType, string currentStatus, int companyId, string employeeStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployees", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeName", employeeName);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@Email", email);
                    da.SelectCommand.Parameters.AddWithValue("@PANNo", panNo);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeType", employeeType);
                    da.SelectCommand.Parameters.AddWithValue("@CurrentStatus", currentStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeStatus", employeeStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeDetail(int employeeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmployeeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetEssEmployeeDetail(int userId, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_ESS_GetEmployeeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Book
        public DataTable GetBookList(string bookName = "", string bookType = "0", string bookCode = "", int companyId = 0, string status = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBooks", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookName", bookName);
                    da.SelectCommand.Parameters.AddWithValue("@BookType", bookType);
                    da.SelectCommand.Parameters.AddWithValue("@BookCode", bookCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetBookDetail(int bookId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBookDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@bookId", bookId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public decimal GetBookBalance(int companyId, int finYearId, long voucherId, int bookId)
        {
            decimal bookBalance = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_GetBankCashBookBalance", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CompanyId", companyId);
                        cmd.Parameters.AddWithValue("@FinYearId", finYearId);
                        cmd.Parameters.AddWithValue("@VoucherId", voucherId);
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            bookBalance = Convert.ToDecimal(result);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookBalance;
        }
        #endregion 


        #region CustomerType
        public DataTable GetCustomerTypeList(string customertypeDesc, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeDesc", customertypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerTypeDetail(int customertypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeId", customertypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion






        #region Customer
        public DataTable GetCustomerList(string customerName, string customerCode, string mobileNo, int customerTypeid, int companyId, string customerStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerCode", customerCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerTypeId", customerTypeid);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerStatus", customerStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerDetail(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetNewCustomerCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetNewCustomersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetTotalCustomerCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTotalCustomersCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetTodayNewCustomer(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetNewCustomersToday", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion


        #region Vendor
        public DataTable GetVendorList(string vendorName, string vendorCode, string mobileNo, int companyId, string vendorStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendors", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@VendorCode", vendorCode);
                    da.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@VendorStatus", vendorStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorDetail(int vendorId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion




        #region Customer Branch
        public DataTable GetCustomerBranchList(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerBranchs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region GL Main Group 
        public DataTable GetGLMainGroupList(string glmaingroupName, string glType, int sequenceNo, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLMainGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupName", glmaingroupName);
                    da.SelectCommand.Parameters.AddWithValue("@GLType", glType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SequenceNo", sequenceNo);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetGLMainGroupDetail(int glmaingroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLMainGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glmaingroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region Schedule 
        public DataTable GetScheduleList(string scheduleName, int scheduleNo, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSchedule", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ScheduleName", scheduleName);
                    da.SelectCommand.Parameters.AddWithValue("@ScheduleNo", scheduleNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetScheduleDetail(int scheduleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetScheduleDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@scheduleId", scheduleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region FormType 
        public DataTable GetFormTypeList(string formTypeDesc, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_FormType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormTypeDesc", formTypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetFormTypeDetail(int formTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFormTypeDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormTypeId", formTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }




        #endregion

        #region Customer Product
        public DataTable GetCustomerProductList(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCustomerSiteWiseReport(int customerId = 0, int customerBranchId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteWiseExpense", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Vendor Product
        public DataTable GetVendorProductList(int vendorId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region GL Sub Group 
        public DataTable GetGLSubGroupList(string glsubgroupName, int scheduleId, string glmaingroupId, int sequenceNo, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLSubGroups", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupName", glsubgroupName);
                    da.SelectCommand.Parameters.AddWithValue("@glmaingroupId", glmaingroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ScheduleId", scheduleId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SequenceNo", sequenceNo);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetGLSubGroupDetail(int glsubgroupId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLSubGroupDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glsubgroupId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region QuotationSetting
        public DataTable GetQuotationSettingList(int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationSettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetQuotationSettingDetail(int quotationsettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationSettingDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationSettingId", quotationsettingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Term Template Details 

        public DataTable GetTermTemplateList(string termtemplateName, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermTemplates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTempalteName", termtemplateName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTermTemplateDetail(int termtemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermTemplateDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTemplateId", termtemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }



        public DataTable GetTermTemplateDetailList(int termtemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermProductDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTemplateId", termtemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetTermTemplateDetailListForPO(int termtemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTermProductDetailsForPO", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TermTemplateId", termtemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion




        #region Quotation


        public ResponseOut AddEditQuotation(Quotation quotation, List<QuotationProductDetail> quotationProductList, List<QuotationTaxDetail> quotationTaxList, List<QuotationTermsDetail> quotationTermList, List<QuotationSupportingDocument> quotationDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtQuotationProduct = new DataTable();
                dtQuotationProduct.Columns.Add("QuotationProductDetailId", typeof(Int64));
                dtQuotationProduct.Columns.Add("QuotationId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtQuotationProduct.Columns.Add("Price", typeof(decimal));
                dtQuotationProduct.Columns.Add("Quantity", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxId", typeof(Int64));
                dtQuotationProduct.Columns.Add("TaxName", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtQuotationProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtQuotationProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("HSN_Code", typeof(string));

                if (quotationProductList != null && quotationProductList.Count > 0)
                {
                    foreach (QuotationProductDetail item in quotationProductList)
                    {
                        DataRow dtrow = dtQuotationProduct.NewRow();
                        dtrow["QuotationProductDetailId"] = 0;
                        dtrow["QuotationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtQuotationProduct.Rows.Add(dtrow);
                    }
                    dtQuotationProduct.AcceptChanges();

                }

                DataTable dtQuotationTax = new DataTable();
                dtQuotationTax.Columns.Add("TaxId", typeof(Int64));
                dtQuotationTax.Columns.Add("TaxName", typeof(string));
                dtQuotationTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationTax.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_1", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_2", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_3", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_3", typeof(decimal));


                if (quotationTaxList != null && quotationTaxList.Count > 0)
                {
                    foreach (QuotationTaxDetail item in quotationTaxList)
                    {
                        DataRow dtrow = dtQuotationTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtQuotationTax.Rows.Add(dtrow);
                    }
                    dtQuotationTax.AcceptChanges();
                }

                DataTable dtQuotationTerm = new DataTable();
                dtQuotationTerm.Columns.Add("TermDesc", typeof(string));
                dtQuotationTerm.Columns.Add("TermSequence", typeof(int));

                if (quotationTermList != null && quotationTermList.Count > 0)
                {
                    foreach (QuotationTermsDetail item in quotationTermList)
                    {
                        DataRow dtrow = dtQuotationTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtQuotationTerm.Rows.Add(dtrow);
                    }
                    dtQuotationTerm.AcceptChanges();
                }


                DataTable dtQUOTATIONDocument = new DataTable();
                dtQUOTATIONDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtQUOTATIONDocument.Columns.Add("DocumentName", typeof(string));
                dtQUOTATIONDocument.Columns.Add("DocumentPath", typeof(string));

                if (quotationDocumentList != null && quotationDocumentList.Count > 0)
                {
                    foreach (QuotationSupportingDocument item in quotationDocumentList)
                    {
                        DataRow dtrow = dtQUOTATIONDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtQUOTATIONDocument.Rows.Add(dtrow);
                    }
                    dtQUOTATIONDocument.AcceptChanges();
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditQuotation", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuotationId", quotation.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationDate", quotation.QuotationDate);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", quotation.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", quotation.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", quotation.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", quotation.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", quotation.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", quotation.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", quotation.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", quotation.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", quotation.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", quotation.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", quotation.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", quotation.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", quotation.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", quotation.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@RefNo", quotation.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", quotation.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", quotation.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", quotation.TotalValue);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", quotation.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", quotation.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", quotation.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", quotation.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", quotation.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", quotation.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", quotation.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", quotation.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", quotation.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", quotation.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", quotation.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", quotation.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", quotation.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", quotation.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", quotation.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", quotation.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", quotation.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", quotation.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", quotation.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", quotation.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", quotation.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", quotation.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", quotation.ReverseChargeAmount);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", quotation.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", quotation.Remarks2);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", quotation.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", quotation.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", quotation.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", quotation.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@QuotationProductDetail", dtQuotationProduct);
                        sqlCommand.Parameters.AddWithValue("@QuotationTaxDetail", dtQuotationTax);
                        sqlCommand.Parameters.AddWithValue("@QuotationTermDetail", dtQuotationTerm);

                        sqlCommand.Parameters.AddWithValue("@QuotationSupportingDocument", dtQUOTATIONDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetQuotationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (quotation.QuotationId == 0)
                            {
                                responseOut.message = ActionMessage.QuotationCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.QuotationUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetQuotationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetQuotationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetQuotationProductList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationTaxList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationTermList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetQuotationList(string quotationNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationNo", quotationNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetQuotationDetail(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetQupotationSupportingDocumentList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PO
        public ResponseOut AddEditPO(PO po, List<POProductDetail> poProductList, List<POTaxDetail> poTaxList, List<POTermsDetail> poTermList, List<POSupportingDocument> poDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPOProduct = new DataTable();
                dtPOProduct.Columns.Add("POProductDetailId", typeof(Int64));
                dtPOProduct.Columns.Add("POId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPOProduct.Columns.Add("Price", typeof(decimal));
                dtPOProduct.Columns.Add("Quantity", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TaxId", typeof(Int64));
                dtPOProduct.Columns.Add("TaxName", typeof(string));
                dtPOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("TaxAmount", typeof(decimal));

                dtPOProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPOProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPOProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));

                dtPOProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("HSN_Code", typeof(string));
                dtPOProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (poProductList != null && poProductList.Count > 0)
                {
                    foreach (POProductDetail item in poProductList)
                    {
                        DataRow dtrow = dtPOProduct.NewRow();
                        dtrow["POProductDetailId"] = 0;
                        dtrow["POId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;


                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtPOProduct.Rows.Add(dtrow);
                    }
                    dtPOProduct.AcceptChanges();

                }
                DataTable dtPOTax = new DataTable();
                dtPOTax.Columns.Add("TaxId", typeof(Int64));
                dtPOTax.Columns.Add("TaxName", typeof(string));
                dtPOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOTax.Columns.Add("TaxAmount", typeof(decimal));

                dtPOTax.Columns.Add("SurchargeName_1", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPOTax.Columns.Add("SurchargeName_2", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPOTax.Columns.Add("SurchargeName_3", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_3", typeof(decimal));

                if (poTaxList != null && poTaxList.Count > 0)
                {
                    foreach (POTaxDetail item in poTaxList)
                    {
                        DataRow dtrow = dtPOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtPOTax.Rows.Add(dtrow);
                    }
                    dtPOTax.AcceptChanges();
                }

                DataTable dtPODocument = new DataTable();
                dtPODocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtPODocument.Columns.Add("DocumentName", typeof(string));
                dtPODocument.Columns.Add("DocumentPath", typeof(string));

                if (poDocumentList != null && poDocumentList.Count > 0)
                {
                    foreach (POSupportingDocument item in poDocumentList)
                    {
                        DataRow dtrow = dtPODocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtPODocument.Rows.Add(dtrow);
                    }
                    dtPODocument.AcceptChanges();
                }

                DataTable dtPOTerm = new DataTable();
                dtPOTerm.Columns.Add("TermDesc", typeof(string));
                dtPOTerm.Columns.Add("TermSequence", typeof(int));

                if (poTermList != null && poTermList.Count > 0)
                {
                    foreach (POTermsDetail item in poTermList)
                    {
                        DataRow dtrow = dtPOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPOTerm.Rows.Add(dtrow);
                    }
                    dtPOTerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@POId", po.POId);
                        sqlCommand.Parameters.AddWithValue("@PODate", po.PODate);
                        sqlCommand.Parameters.AddWithValue("@IndentId", po.IndentId);
                        sqlCommand.Parameters.AddWithValue("@IndentNo", po.IndentNo);
                        sqlCommand.Parameters.AddWithValue("@QuotationId", po.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationNo", po.QuotationNo);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", po.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@VendorId", po.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", po.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", po.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", po.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", po.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@City", po.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", po.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", po.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", po.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", po.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", po.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", po.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", po.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", po.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", po.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@RefNo", po.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", po.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", po.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", po.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", po.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", po.ConsigneeName);

                        sqlCommand.Parameters.AddWithValue("@ShippingCity", po.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", po.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", po.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", po.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", po.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", po.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", po.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", po.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", po.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", po.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", po.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", po.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", po.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", po.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", po.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", po.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", po.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", po.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", po.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", po.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", po.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", po.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", po.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", po.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", po.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", po.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", po.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", po.ReverseChargeAmount);


                        sqlCommand.Parameters.AddWithValue("@Remarks1", po.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", po.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@ExpectedDeliveryDate", po.ExpectedDeliveryDate);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", po.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", po.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", po.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", po.CreatedBy);


                        sqlCommand.Parameters.AddWithValue("@POProductDetail", dtPOProduct);
                        sqlCommand.Parameters.AddWithValue("@POTaxDetail", dtPOTax);
                        sqlCommand.Parameters.AddWithValue("@POTermDetail", dtPOTerm);
                        sqlCommand.Parameters.AddWithValue("@POSupportingDocument", dtPODocument);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (po.POId == 0)
                            {
                                responseOut.message = ActionMessage.POCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.POUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPOProductList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOProductListForMRN(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOProductsForMRN", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOTaxList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOSupportingDocumentList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOTermList(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOList(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOListForMRN(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOsForMRN", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOLessThan25KList(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOLessThan25K", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPODetail(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPOPurchaseQuotationList(string quotationNo, string vendorName, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOPurchaseQuotations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationNo", quotationNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPOPurchaseQuotationProductList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOPurchaseQuotationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOEscalationList(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetListPendingPOEscalationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPOLessThan25KEscalationList(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetListPendingPOLessThen25KEscalationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region PurchaseInvoice
        public ResponseOut AddEditPI(PurchaseInvoice pi, List<PurchaseInvoiceProductDetail> piProductList, List<PurchaseInvoiceTaxDetail> piTaxList, List<PurchaseInvoiceTermsDetail> piTermList, List<PISupportingDocument> piDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPIProduct = new DataTable();
                dtPIProduct.Columns.Add("InvoiceProductDetailId", typeof(Int64));
                dtPIProduct.Columns.Add("InvoiceId", typeof(Int64));
                dtPIProduct.Columns.Add("ProductId", typeof(Int64));
                dtPIProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPIProduct.Columns.Add("Price", typeof(decimal));
                dtPIProduct.Columns.Add("Quantity", typeof(decimal));
                dtPIProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPIProduct.Columns.Add("DiscountAmount", typeof(decimal));

                dtPIProduct.Columns.Add("TaxId", typeof(Int64));
                dtPIProduct.Columns.Add("TaxName", typeof(string));

                dtPIProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPIProduct.Columns.Add("TaxAmount", typeof(decimal));

                dtPIProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtPIProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPIProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPIProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtPIProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPIProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPIProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtPIProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPIProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));

                dtPIProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPIProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPIProduct.Columns.Add("HSN_Code", typeof(string));
                dtPIProduct.Columns.Add("TotalPrice", typeof(decimal));


                if (piProductList != null && piProductList.Count > 0)
                {
                    foreach (PurchaseInvoiceProductDetail item in piProductList)
                    {
                        DataRow dtrow = dtPIProduct.NewRow();
                        dtrow["InvoiceProductDetailId"] = 0;
                        dtrow["InvoiceId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;


                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;

                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;


                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;


                        dtPIProduct.Rows.Add(dtrow);
                    }
                    dtPIProduct.AcceptChanges();

                }
                DataTable dtPITax = new DataTable();
                dtPITax.Columns.Add("TaxId", typeof(Int64));
                dtPITax.Columns.Add("TaxName", typeof(string));
                dtPITax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPITax.Columns.Add("TaxAmount", typeof(decimal));

                dtPITax.Columns.Add("SurchargeName_1", typeof(string));
                dtPITax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPITax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPITax.Columns.Add("SurchargeName_2", typeof(string));
                dtPITax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPITax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPITax.Columns.Add("SurchargeName_3", typeof(string));
                dtPITax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPITax.Columns.Add("SurchargeAmount_3", typeof(decimal));
                if (piTaxList != null && piTaxList.Count > 0)
                {
                    foreach (PurchaseInvoiceTaxDetail item in piTaxList)
                    {
                        DataRow dtrow = dtPITax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtPITax.Rows.Add(dtrow);
                    }
                    dtPITax.AcceptChanges();
                }

                DataTable dtPODocument = new DataTable();
                dtPODocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtPODocument.Columns.Add("DocumentName", typeof(string));
                dtPODocument.Columns.Add("DocumentPath", typeof(string));

                if (piDocumentList != null && piDocumentList.Count > 0)
                {
                    foreach (PISupportingDocument item in piDocumentList)
                    {
                        DataRow dtrow = dtPODocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtPODocument.Rows.Add(dtrow);
                    }
                    dtPODocument.AcceptChanges();
                }


                DataTable dtPITerm = new DataTable();
                dtPITerm.Columns.Add("TermDesc", typeof(string));
                dtPITerm.Columns.Add("TermSequence", typeof(int));

                if (piTermList != null && piTermList.Count > 0)
                {
                    foreach (PurchaseInvoiceTermsDetail item in piTermList)
                    {
                        DataRow dtrow = dtPITerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPITerm.Rows.Add(dtrow);
                    }
                    dtPITerm.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPurchaseInvoice", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", pi.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceDate", pi.InvoiceDate);
                        sqlCommand.Parameters.AddWithValue("@POId", pi.POId);
                        sqlCommand.Parameters.AddWithValue("@PONo", pi.PONo);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", pi.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@VendorId", pi.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", pi.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", pi.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", pi.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", pi.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@City", pi.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", pi.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", pi.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", pi.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", pi.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", pi.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", pi.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", pi.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", pi.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", pi.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@RefNo", pi.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", pi.RefDate);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", pi.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", pi.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", pi.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", pi.ConsigneeName);

                        sqlCommand.Parameters.AddWithValue("@ShippingCity", pi.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", pi.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", pi.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", pi.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", pi.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", pi.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", pi.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", pi.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", pi.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", pi.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", pi.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", pi.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", pi.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", pi.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", pi.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", pi.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", pi.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", pi.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", pi.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", pi.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", pi.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", pi.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", pi.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", pi.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", pi.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", pi.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", pi.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", pi.ReverseChargeAmount);

                        sqlCommand.Parameters.AddWithValue("@Remarks", pi.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", pi.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", pi.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", pi.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", pi.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceProductDetail", dtPIProduct);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceTaxDetail", dtPITax);
                        sqlCommand.Parameters.AddWithValue("@PurchaseInvoiceTermDetail", dtPITerm);
                        sqlCommand.Parameters.AddWithValue("@PISupportingDocument", dtPODocument);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetInvoiceId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (pi.InvoiceId == 0)
                            {
                                responseOut.message = ActionMessage.PICreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.PIUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetInvoiceId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetInvoiceId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPIProductList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPIMRNProductList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPITaxList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceTaxDetailTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPITermList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceTermsDetailTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPIList(string invoiceNo, string vendorName, string refNo, string fromDate, string toDate, int companyId, string approvalStatus = "", string displayType = "", string vendorCode = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    da = new SqlDataAdapter("proc_GetPIs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@VendorCode", vendorCode);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPISupportingDocumentList(long piId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPISupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", piId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }



        public DataTable GetPIDetail(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPIDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPOListForPI(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPOForPI", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Lead FollowUp
        public DataTable GetLeadFollowUpList(int leadId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLeadFollowUp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@leadid", leadId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region DocumentType
        public DataTable GetDocumentTypeList(string documenttypeDesc, int companyId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDocumentTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DocumentTypeDesc", documenttypeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetDocumentTypeDetail(int documenttypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDocumentTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DocumentTypeId", documenttypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region  SO

        public ResponseOut AddEditSO(SO so, List<SOProductDetail> soProductList, List<SOTaxDetail> soTaxList, List<SOTermsDetail> soTermList, List<SOSupportingDocument> soDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSOProduct = new DataTable();
                dtSOProduct.Columns.Add("SOProductDetailId", typeof(Int64));
                dtSOProduct.Columns.Add("SOId", typeof(Int64));
                dtSOProduct.Columns.Add("ProductId", typeof(Int64));
                dtSOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtSOProduct.Columns.Add("Price", typeof(decimal));
                dtSOProduct.Columns.Add("Quantity", typeof(decimal));
                dtSOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtSOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtSOProduct.Columns.Add("TaxId", typeof(Int64));
                dtSOProduct.Columns.Add("TaxName", typeof(string));
                dtSOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtSOProduct.Columns.Add("TaxAmount", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSOProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtSOProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSOProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtSOProduct.Columns.Add("CGST_Perc", typeof(string));
                dtSOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtSOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtSOProduct.Columns.Add("SGST_Amount", typeof(string));
                dtSOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtSOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtSOProduct.Columns.Add("HSN_Code", typeof(string));
                dtSOProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (soProductList != null && soProductList.Count > 0)
                {
                    foreach (SOProductDetail item in soProductList)
                    {
                        DataRow dtrow = dtSOProduct.NewRow();
                        dtrow["SOProductDetailId"] = 0;
                        dtrow["SOId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;
                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;
                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Amount;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtSOProduct.Rows.Add(dtrow);
                    }
                    dtSOProduct.AcceptChanges();

                }

                DataTable dtSOTax = new DataTable();
                dtSOTax.Columns.Add("TaxId", typeof(Int64));
                dtSOTax.Columns.Add("TaxName", typeof(string));
                dtSOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtSOTax.Columns.Add("TaxAmount", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_1", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_2", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSOTax.Columns.Add("SurchargeName_3", typeof(string));
                dtSOTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSOTax.Columns.Add("SurchargeAmount_3", typeof(decimal));

                if (soTaxList != null && soTaxList.Count > 0)
                {
                    foreach (SOTaxDetail item in soTaxList)
                    {
                        DataRow dtrow = dtSOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtSOTax.Rows.Add(dtrow);
                    }
                    dtSOTax.AcceptChanges();
                }

                DataTable dtSOTerm = new DataTable();
                dtSOTerm.Columns.Add("TermDesc", typeof(string));
                dtSOTerm.Columns.Add("TermSequence", typeof(int));

                if (soTermList != null && soTermList.Count > 0)
                {
                    foreach (SOTermsDetail item in soTermList)
                    {
                        DataRow dtrow = dtSOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtSOTerm.Rows.Add(dtrow);
                    }
                    dtSOTerm.AcceptChanges();
                }



                DataTable dtSODocument = new DataTable();
                dtSODocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtSODocument.Columns.Add("DocumentName", typeof(string));
                dtSODocument.Columns.Add("DocumentPath", typeof(string));

                if (soDocumentList != null && soDocumentList.Count > 0)
                {
                    foreach (SOSupportingDocument item in soDocumentList)
                    {
                        DataRow dtrow = dtSODocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtSODocument.Rows.Add(dtrow);
                    }
                    dtSODocument.AcceptChanges();
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@SOId", so.SOId);
                        sqlCommand.Parameters.AddWithValue("@SODate", so.SODate);

                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", so.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", so.CurrencyCode);

                        sqlCommand.Parameters.AddWithValue("@QuotationId", so.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationNo", so.QuotationNo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", so.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", so.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", so.ContactPerson);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", so.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", so.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", so.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", so.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", so.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", so.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", so.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", so.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", so.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", so.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@Email", so.Email);
                        sqlCommand.Parameters.AddWithValue("@MobileNo", so.MobileNo);
                        sqlCommand.Parameters.AddWithValue("@ContactNo", so.ContactNo);
                        sqlCommand.Parameters.AddWithValue("@Fax", so.Fax);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", so.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", so.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", so.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", so.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", so.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", so.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", so.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", so.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", so.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", so.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", so.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", so.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", so.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", so.ShippingFax);

                        sqlCommand.Parameters.AddWithValue("@RefNo", so.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", so.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", so.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", so.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", so.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", so.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@PayToBookId", so.PayToBookId);


                        sqlCommand.Parameters.AddWithValue("@Remarks1", so.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", so.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", so.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", so.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", so.CreatedBy);


                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", so.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", so.ReverseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", so.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", so.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", so.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", so.FreightIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", so.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", so.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", so.LoadingIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", so.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", so.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", so.LoadingIGST_Amt);

                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", so.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", so.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", so.FreightIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", so.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", so.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", so.LoadingIGST_Perc);

                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", so.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", so.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", so.InsuranceIGST_Perc);


                        sqlCommand.Parameters.AddWithValue("@SOProductDetail", dtSOProduct);
                        sqlCommand.Parameters.AddWithValue("@SOTaxDetail", dtSOTax);
                        sqlCommand.Parameters.AddWithValue("@SOTermDetail", dtSOTerm);

                        sqlCommand.Parameters.AddWithValue("@SOSupportingDocument", dtSODocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (so.SOId == 0)
                            {
                                responseOut.message = ActionMessage.SOCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SOUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSOProductList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSOTaxList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSOTermList(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSOList(string soNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus = "", string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SONo", soNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSODetail(long soId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", soId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetSOSupportingDocumentList(long sOId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOId", sOId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region  SaleInvoice

        public ResponseOut AddEditSaleInvoice(SaleInvoice saleinvoice, List<SaleInvoiceProductDetail> saleinvoiceProductList, List<SaleInvoiceTaxDetail> saleinvoiceTaxList, List<SaleInvoiceTermsDetail> saleinvoiceTermList, List<SaleInvoiceProductSerialDetail> saleInvoiceProductSerialDetail, List<SaleInvoiceSupportingDocument> siDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSaleInvoiceProduct = new DataTable();
                dtSaleInvoiceProduct.Columns.Add("InvoiceProductDetailId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("InvoiceId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("ProductId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("Price", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("Quantity", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("TaxId", typeof(Int64));
                dtSaleInvoiceProduct.Columns.Add("TaxName", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSaleInvoiceProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSaleInvoiceProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtSaleInvoiceProduct.Columns.Add("HSN_Code", typeof(string));
                dtSaleInvoiceProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (saleinvoiceProductList != null && saleinvoiceProductList.Count > 0)
                {
                    foreach (SaleInvoiceProductDetail item in saleinvoiceProductList)
                    {
                        DataRow dtrow = dtSaleInvoiceProduct.NewRow();
                        dtrow["InvoiceProductDetailId"] = 0;
                        dtrow["InvoiceId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtSaleInvoiceProduct.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceProduct.AcceptChanges();

                }

                DataTable dtSaleInvoiceTax = new DataTable();
                dtSaleInvoiceTax.Columns.Add("TaxId", typeof(Int64));
                dtSaleInvoiceTax.Columns.Add("TaxName", typeof(string));
                dtSaleInvoiceTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("TaxAmount", typeof(decimal));

                dtSaleInvoiceTax.Columns.Add("SurchargeName_1", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtSaleInvoiceTax.Columns.Add("SurchargeName_2", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtSaleInvoiceTax.Columns.Add("SurchargeName_3", typeof(string));
                dtSaleInvoiceTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtSaleInvoiceTax.Columns.Add("SurchargeAmount_3", typeof(decimal));

                if (saleinvoiceTaxList != null && saleinvoiceTaxList.Count > 0)
                {
                    foreach (SaleInvoiceTaxDetail item in saleinvoiceTaxList)
                    {
                        DataRow dtrow = dtSaleInvoiceTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtSaleInvoiceTax.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceTax.AcceptChanges();
                }

                DataTable dtSaleInvoiceTerm = new DataTable();
                dtSaleInvoiceTerm.Columns.Add("TermDesc", typeof(string));
                dtSaleInvoiceTerm.Columns.Add("TermSequence", typeof(int));

                if (saleinvoiceTermList != null && saleinvoiceTermList.Count > 0)
                {
                    foreach (SaleInvoiceTermsDetail item in saleinvoiceTermList)
                    {
                        DataRow dtrow = dtSaleInvoiceTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtSaleInvoiceTerm.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceTerm.AcceptChanges();
                }

                DataTable dtSaleInvoiceProductSerial = new DataTable();
                dtSaleInvoiceProductSerial.Columns.Add("ProductId", typeof(int));
                dtSaleInvoiceProductSerial.Columns.Add("RefSerial1", typeof(string));

                dtSaleInvoiceProductSerial.Columns.Add("RefSerial2", typeof(string));
                dtSaleInvoiceProductSerial.Columns.Add("RefSerial3", typeof(string));
                dtSaleInvoiceProductSerial.Columns.Add("RefSerial4", typeof(string));

                if (saleInvoiceProductSerialDetail != null && saleInvoiceProductSerialDetail.Count > 0)
                {
                    foreach (SaleInvoiceProductSerialDetail item in saleInvoiceProductSerialDetail)
                    {
                        DataRow dtrow = dtSaleInvoiceProductSerial.NewRow();

                        dtrow["ProductId"] = item.ProductId;
                        dtrow["RefSerial1"] = item.RefSerial1;
                        dtrow["RefSerial2"] = item.RefSerial2;
                        dtrow["RefSerial3"] = item.RefSerial3;
                        dtrow["RefSerial4"] = item.RefSerial4;
                        dtSaleInvoiceProductSerial.Rows.Add(dtrow);
                    }
                    dtSaleInvoiceProductSerial.AcceptChanges();
                }

                DataTable dtSIDocument = new DataTable();
                dtSIDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtSIDocument.Columns.Add("DocumentName", typeof(string));
                dtSIDocument.Columns.Add("DocumentPath", typeof(string));

                if (siDocumentList != null && siDocumentList.Count > 0)
                {
                    foreach (SaleInvoiceSupportingDocument item in siDocumentList)
                    {
                        DataRow dtrow = dtSIDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtSIDocument.Rows.Add(dtrow);
                    }
                    dtSIDocument.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSaleInvoice", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", saleinvoice.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceDate", saleinvoice.InvoiceDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceType", saleinvoice.InvoiceType);
                        sqlCommand.Parameters.AddWithValue("@SOId", saleinvoice.SOId);
                        sqlCommand.Parameters.AddWithValue("@SONo", saleinvoice.SONo);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", saleinvoice.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", saleinvoice.CurrencyCode);

                        sqlCommand.Parameters.AddWithValue("@CustomerId", saleinvoice.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", saleinvoice.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", saleinvoice.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", saleinvoice.ConsigneeName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", saleinvoice.ContactPerson);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", saleinvoice.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", saleinvoice.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", saleinvoice.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", saleinvoice.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", saleinvoice.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", saleinvoice.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", saleinvoice.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", saleinvoice.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", saleinvoice.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", saleinvoice.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@Email", saleinvoice.Email);
                        sqlCommand.Parameters.AddWithValue("@MobileNo", saleinvoice.MobileNo);
                        sqlCommand.Parameters.AddWithValue("@ContactNo", saleinvoice.ContactNo);
                        sqlCommand.Parameters.AddWithValue("@Fax", saleinvoice.Fax);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", saleinvoice.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", saleinvoice.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", saleinvoice.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", saleinvoice.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", saleinvoice.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", saleinvoice.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", saleinvoice.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", saleinvoice.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", saleinvoice.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", saleinvoice.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", saleinvoice.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", saleinvoice.ShippingFax);

                        sqlCommand.Parameters.AddWithValue("@RefNo", saleinvoice.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", saleinvoice.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", saleinvoice.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", saleinvoice.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", saleinvoice.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", saleinvoice.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", saleinvoice.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", saleinvoice.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", saleinvoice.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", saleinvoice.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", saleinvoice.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", saleinvoice.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", saleinvoice.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", saleinvoice.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", saleinvoice.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", saleinvoice.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", saleinvoice.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", saleinvoice.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", saleinvoice.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", saleinvoice.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", saleinvoice.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", saleinvoice.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", saleinvoice.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", saleinvoice.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", saleinvoice.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@PayToBookId", saleinvoice.PayToBookId);


                        sqlCommand.Parameters.AddWithValue("@Remarks", saleinvoice.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", saleinvoice.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", saleinvoice.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", saleinvoice.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", saleinvoice.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", saleinvoice.ReverseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceProductDetail", dtSaleInvoiceProduct);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceTaxDetail", dtSaleInvoiceTax);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceTermDetail", dtSaleInvoiceTerm);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceProductSerialDetail", dtSaleInvoiceProductSerial);
                        sqlCommand.Parameters.AddWithValue("@SaleInvoiceSupportingDocument", dtSIDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetInvoiceId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (saleinvoice.InvoiceId == 0)
                            {
                                responseOut.message = ActionMessage.SaleInvoiceCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SaleInvoiceUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetInvoiceId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetInvoiceId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSaleInvoiceProductList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleInvoiceTaxList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleInvoiceTermList(long saleinvoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", saleinvoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSaleInvoiceList(string invoiceNo, string customerName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string invoiceType = "", string displayType = "", string approvalStatus = "", string customerCode = "", int companyBranchId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoices", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@invoiceType", invoiceType);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerCode", customerCode);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetJVSaleInvoiceList(string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId, string invoiceType = "", string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJVSaleInvoices", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@invoiceType", invoiceType);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSaleInvoiceDetail(long saleinvoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", saleinvoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GetSaleInvoiceProductSerialDetailList(long invoiceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceProductSerialList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSISupportingDocumentList(long siId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSISupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", siId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSITotalAmountSumByUser(int companyId, int FinYearId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetUserIDBySITotalAmountSum", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", FinYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }



        #endregion

        #region Product State Tax Mapping 
        public DataTable GetProductTaxMappingList(int productSubGroupId, int stateId, int taxId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductStateTaxMapping", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Productsubgroupid", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@TaxId", taxId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductStateTaxDetail(int MappingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductStateTaxDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MappingId", MappingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion
        #region SOSetting
        public DataTable GetSOSettingList(int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOSettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSOSettingDetail(int sosettingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSOSettingDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SOSettingId", sosettingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Customer Payment

        public ResponseOut AddEditCustomerPayment(CustomerPayment customerpayment)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditCustomerPayment", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PaymentTrnId", customerpayment.PaymentTrnId);
                        sqlCommand.Parameters.AddWithValue("@PaymentDate", customerpayment.PaymentDate);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", customerpayment.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", customerpayment.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@BookId", customerpayment.BookId);
                        sqlCommand.Parameters.AddWithValue("@PaymentModeId", customerpayment.PaymentModeId);
                        sqlCommand.Parameters.AddWithValue("@RefNo", customerpayment.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", customerpayment.RefDate);
                        sqlCommand.Parameters.AddWithValue("@Remarks", customerpayment.Remarks);
                        sqlCommand.Parameters.AddWithValue("@ValueDate", customerpayment.ValueDate);
                        sqlCommand.Parameters.AddWithValue("@Amount", customerpayment.Amount);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", customerpayment.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", customerpayment.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CustomerPaymentStatus", customerpayment.Status);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", customerpayment.CreatedBy);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPaymentTrnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (customerpayment.PaymentTrnId == 0)
                            {
                                responseOut.message = ActionMessage.CustomerPaymentCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.CustomerPaymentUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPaymentTrnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPaymentTrnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public DataTable GetCustomerPaymentList(string paymentNo, string customerName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPayments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentNo", paymentNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerPaymentDetail(long paymenttrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPaymentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTrnId", paymenttrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }





        #endregion


        #region  Customer Form     
        public DataTable GetCustomerFormList(string formStatus, string customerName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerForm", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", formStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCustomerFormDetail(long customerFormTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFormDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerFormTrnId", customerFormTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        #endregion

        #region  Vendor Payment

        public ResponseOut AddEditVendorPayment(VendorPayment vendorpayment)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditVendorPayment", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PaymentTrnId", vendorpayment.PaymentTrnId);
                        sqlCommand.Parameters.AddWithValue("@PaymentDate", vendorpayment.PaymentDate);
                        sqlCommand.Parameters.AddWithValue("@VendorId", vendorpayment.VendorId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", vendorpayment.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@BookId", vendorpayment.BookId);
                        sqlCommand.Parameters.AddWithValue("@PaymentModeId", vendorpayment.PaymentModeId);
                        sqlCommand.Parameters.AddWithValue("@RefNo", vendorpayment.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", vendorpayment.RefDate);
                        sqlCommand.Parameters.AddWithValue("@Remarks", vendorpayment.Remarks);
                        sqlCommand.Parameters.AddWithValue("@ValueDate", vendorpayment.ValueDate);
                        sqlCommand.Parameters.AddWithValue("@Amount", vendorpayment.Amount);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", vendorpayment.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", vendorpayment.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VendorPaymentStatus", vendorpayment.Status);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", vendorpayment.CreatedBy);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPaymentTrnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (vendorpayment.PaymentTrnId == 0)
                            {
                                responseOut.message = ActionMessage.VendorPaymentCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.VendorPaymentUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPaymentTrnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPaymentTrnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public DataTable GetVendorPaymentList(string paymentNo, string vendorName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPayments", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentNo", paymentNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorPaymentDetail(long paymenttrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPaymentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PaymentTrnId", paymenttrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region  Delivery Challan

        public ResponseOut AddEditDeliveryChallan(DeliveryChallan deliverychallan, List<ChallanProductDetail> challanProductList, List<ChallanTaxDetail> challanTaxList, List<ChallanTermsDetail> challanTermList, List<DeliveryChallanSupportingDocument> deliveryChallanDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtChallanProduct = new DataTable();
                dtChallanProduct.Columns.Add("ChallanProductDetailId", typeof(Int64));
                dtChallanProduct.Columns.Add("ChallanId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtChallanProduct.Columns.Add("Price", typeof(decimal));
                dtChallanProduct.Columns.Add("Quantity", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxId", typeof(Int64));
                dtChallanProduct.Columns.Add("TaxName", typeof(string));
                dtChallanProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("HSN_Code", typeof(string));
                dtChallanProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (challanProductList != null && challanProductList.Count > 0)
                {
                    foreach (ChallanProductDetail item in challanProductList)
                    {
                        DataRow dtrow = dtChallanProduct.NewRow();
                        dtrow["ChallanProductDetailId"] = 0;
                        dtrow["ChallanId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtChallanProduct.Rows.Add(dtrow);
                    }
                    dtChallanProduct.AcceptChanges();

                }

                DataTable dtChallanTax = new DataTable();
                dtChallanTax.Columns.Add("TaxId", typeof(Int64));
                dtChallanTax.Columns.Add("TaxName", typeof(string));
                dtChallanTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtChallanTax.Columns.Add("TaxAmount", typeof(decimal));

                if (challanTaxList != null && challanTaxList.Count > 0)
                {
                    foreach (ChallanTaxDetail item in challanTaxList)
                    {
                        DataRow dtrow = dtChallanTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtChallanTax.Rows.Add(dtrow);
                    }
                    dtChallanTax.AcceptChanges();
                }

                DataTable dtChallanTerm = new DataTable();
                dtChallanTerm.Columns.Add("TermDesc", typeof(string));
                dtChallanTerm.Columns.Add("TermSequence", typeof(int));

                if (challanTermList != null && challanTermList.Count > 0)
                {
                    foreach (ChallanTermsDetail item in challanTermList)
                    {
                        DataRow dtrow = dtChallanTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtChallanTerm.Rows.Add(dtrow);
                    }
                    dtChallanTerm.AcceptChanges();
                }


                DataTable dtDeliveryChallanDocument = new DataTable();
                dtDeliveryChallanDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtDeliveryChallanDocument.Columns.Add("DocumentName", typeof(string));
                dtDeliveryChallanDocument.Columns.Add("DocumentPath", typeof(string));

                if (deliveryChallanDocumentList != null && deliveryChallanDocumentList.Count > 0)
                {
                    foreach (DeliveryChallanSupportingDocument item in deliveryChallanDocumentList)
                    {
                        DataRow dtrow = dtDeliveryChallanDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtDeliveryChallanDocument.Rows.Add(dtrow);
                    }
                    dtDeliveryChallanDocument.AcceptChanges();
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditDeliveryChallan", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ChallanId", deliverychallan.ChallanId);
                        sqlCommand.Parameters.AddWithValue("@ChallanDate", deliverychallan.ChallanDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", deliverychallan.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", deliverychallan.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", deliverychallan.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", deliverychallan.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", deliverychallan.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", deliverychallan.ConsigeeName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", deliverychallan.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", deliverychallan.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", deliverychallan.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", deliverychallan.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", deliverychallan.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", deliverychallan.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", deliverychallan.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", deliverychallan.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", deliverychallan.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", deliverychallan.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", deliverychallan.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", deliverychallan.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", deliverychallan.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", deliverychallan.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", deliverychallan.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", deliverychallan.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", deliverychallan.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", deliverychallan.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", deliverychallan.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", deliverychallan.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", deliverychallan.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", deliverychallan.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", deliverychallan.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", deliverychallan.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", deliverychallan.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", deliverychallan.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", deliverychallan.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", deliverychallan.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", deliverychallan.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", deliverychallan.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", deliverychallan.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", deliverychallan.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", deliverychallan.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", deliverychallan.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", deliverychallan.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", deliverychallan.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", deliverychallan.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", deliverychallan.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", deliverychallan.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", deliverychallan.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", deliverychallan.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", deliverychallan.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", deliverychallan.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", deliverychallan.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", deliverychallan.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", deliverychallan.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", deliverychallan.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", deliverychallan.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", deliverychallan.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", deliverychallan.ReverseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@ChallanProductDetail", dtChallanProduct);
                        sqlCommand.Parameters.AddWithValue("@ChallanTaxDetail", dtChallanTax);
                        sqlCommand.Parameters.AddWithValue("@ChallanTermDetail", dtChallanTerm);

                        sqlCommand.Parameters.AddWithValue("@DeliveryChallanSupportingDocument", dtDeliveryChallanDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetChallanId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (deliverychallan.ChallanId == 0)
                            {
                                responseOut.message = ActionMessage.ChallanCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.ChallanUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetChallanId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetChallanId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }



        public DataTable GetChallanProductList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanTaxList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanTaxes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanTermList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanList(string challanNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallans", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanNo", challanNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetChallanDetail(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChallanDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDeliveryChallanSupportingDocumentList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDeliveryChallanSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DeliveryChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region FollowUpActivityType 
        public DataTable GetFollowUpActivityTypeList(string followUpActivityTypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFollowUpActivityType", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FollowUpActivityTypeName", followUpActivityTypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetFollowUpActivityTypeDetail(int followUpActivityTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFollowUpActivityTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FollowUpActivityTypeId", followUpActivityTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Revised Quotation


        public ResponseOut AddRevisedQuotation(Quotation quotation, List<QuotationProductDetail> quotationProductList, List<QuotationTaxDetail> quotationTaxList, List<QuotationTermsDetail> quotationTermList, List<QuotationSupportingDocument> RevisedQuotationDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtQuotationProduct = new DataTable();
                dtQuotationProduct.Columns.Add("QuotationProductDetailId", typeof(Int64));
                dtQuotationProduct.Columns.Add("QuotationId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtQuotationProduct.Columns.Add("Price", typeof(decimal));
                dtQuotationProduct.Columns.Add("Quantity", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TaxId", typeof(Int64));
                dtQuotationProduct.Columns.Add("TaxName", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtQuotationProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtQuotationProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtQuotationProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtQuotationProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtQuotationProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtQuotationProduct.Columns.Add("HSN_Code", typeof(string));

                if (quotationProductList != null && quotationProductList.Count > 0)
                {
                    foreach (QuotationProductDetail item in quotationProductList)
                    {
                        DataRow dtrow = dtQuotationProduct.NewRow();
                        dtrow["QuotationProductDetailId"] = 0;
                        dtrow["QuotationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;

                        dtQuotationProduct.Rows.Add(dtrow);
                    }
                    dtQuotationProduct.AcceptChanges();

                }

                DataTable dtQuotationTax = new DataTable();
                dtQuotationTax.Columns.Add("TaxId", typeof(Int64));
                dtQuotationTax.Columns.Add("TaxName", typeof(string));
                dtQuotationTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtQuotationTax.Columns.Add("TaxAmount", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_1", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_1", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_2", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_2", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeName_3", typeof(string));
                dtQuotationTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtQuotationTax.Columns.Add("SurchargeAmount_3", typeof(decimal));

                if (quotationTaxList != null && quotationTaxList.Count > 0)
                {
                    foreach (QuotationTaxDetail item in quotationTaxList)
                    {
                        DataRow dtrow = dtQuotationTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtQuotationTax.Rows.Add(dtrow);
                    }
                    dtQuotationTax.AcceptChanges();
                }

                DataTable dtQuotationTerm = new DataTable();
                dtQuotationTerm.Columns.Add("TermDesc", typeof(string));
                dtQuotationTerm.Columns.Add("TermSequence", typeof(int));

                if (quotationTermList != null && quotationTermList.Count > 0)
                {
                    foreach (QuotationTermsDetail item in quotationTermList)
                    {
                        DataRow dtrow = dtQuotationTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtQuotationTerm.Rows.Add(dtrow);
                    }
                    dtQuotationTerm.AcceptChanges();
                }

                DataTable dtREVISEDQUOTATIONDocument = new DataTable();
                dtREVISEDQUOTATIONDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtREVISEDQUOTATIONDocument.Columns.Add("DocumentName", typeof(string));
                dtREVISEDQUOTATIONDocument.Columns.Add("DocumentPath", typeof(string));

                if (RevisedQuotationDocuments != null && RevisedQuotationDocuments.Count > 0)
                {
                    foreach (QuotationSupportingDocument item in RevisedQuotationDocuments)
                    {
                        DataRow dtrow = dtREVISEDQUOTATIONDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtREVISEDQUOTATIONDocument.Rows.Add(dtrow);
                    }
                    dtREVISEDQUOTATIONDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddRevisedQuotation", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuotationId", quotation.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationNo", quotation.QuotationNo);
                        sqlCommand.Parameters.AddWithValue("@QuotationDate", quotation.QuotationDate);

                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", quotation.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", quotation.CurrencyCode);

                        sqlCommand.Parameters.AddWithValue("@CustomerId", quotation.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", quotation.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", quotation.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", quotation.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", quotation.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", quotation.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", quotation.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", quotation.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", quotation.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", quotation.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", quotation.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", quotation.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@RefNo", quotation.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", quotation.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", quotation.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", quotation.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", quotation.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", quotation.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", quotation.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", quotation.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", quotation.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", quotation.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", quotation.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", quotation.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", quotation.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", quotation.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", quotation.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", quotation.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", quotation.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", quotation.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", quotation.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", quotation.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", quotation.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", quotation.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", quotation.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", quotation.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", quotation.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", quotation.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", quotation.ReverseChargeAmount);





                        sqlCommand.Parameters.AddWithValue("@Remarks1", quotation.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", quotation.Remarks2);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", quotation.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", quotation.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", quotation.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", quotation.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@QuotationProductDetail", dtQuotationProduct);
                        sqlCommand.Parameters.AddWithValue("@QuotationTaxDetail", dtQuotationTax);
                        sqlCommand.Parameters.AddWithValue("@QuotationTermDetail", dtQuotationTerm);

                        sqlCommand.Parameters.AddWithValue("@RevisedQuotationSupportingDocument", dtREVISEDQUOTATIONDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetQuotationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {

                            responseOut.message = ActionMessage.RevisedQuotationCreatedSuccess;


                            if (sqlCommand.Parameters["@RetQuotationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetQuotationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        #endregion





        #region Revised PO
        public ResponseOut AddRevisedPO(PO po, List<POProductDetail> poProductList, List<POTaxDetail> poTaxList, List<POTermsDetail> poTermList, List<POSupportingDocument> revisedPODocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPOProduct = new DataTable();
                dtPOProduct.Columns.Add("POProductDetailId", typeof(Int64));
                dtPOProduct.Columns.Add("POId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductId", typeof(Int64));
                dtPOProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPOProduct.Columns.Add("Price", typeof(decimal));
                dtPOProduct.Columns.Add("Quantity", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPOProduct.Columns.Add("TaxId", typeof(Int64));
                dtPOProduct.Columns.Add("TaxName", typeof(string));
                dtPOProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeName_1", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPOProduct.Columns.Add("SurchargeName_2", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPOProduct.Columns.Add("SurchargeName_3", typeof(string));
                dtPOProduct.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPOProduct.Columns.Add("SurchargeAmount_3", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPOProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPOProduct.Columns.Add("HSN_Code", typeof(string));
                dtPOProduct.Columns.Add("TotalPrice", typeof(decimal));
                if (poProductList != null && poProductList.Count > 0)
                {
                    foreach (POProductDetail item in poProductList)
                    {
                        DataRow dtrow = dtPOProduct.NewRow();
                        dtrow["POProductDetailId"] = 0;
                        dtrow["POId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;

                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtPOProduct.Rows.Add(dtrow);
                    }
                    dtPOProduct.AcceptChanges();

                }
                DataTable dtPOTax = new DataTable();
                dtPOTax.Columns.Add("TaxId", typeof(Int64));
                dtPOTax.Columns.Add("TaxName", typeof(string));
                dtPOTax.Columns.Add("TaxPercentage", typeof(decimal));
                dtPOTax.Columns.Add("TaxAmount", typeof(decimal));


                dtPOTax.Columns.Add("SurchargeName_1", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_1", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_1", typeof(decimal));

                dtPOTax.Columns.Add("SurchargeName_2", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_2", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_2", typeof(decimal));

                dtPOTax.Columns.Add("SurchargeName_3", typeof(string));
                dtPOTax.Columns.Add("SurchargePercentage_3", typeof(decimal));
                dtPOTax.Columns.Add("SurchargeAmount_3", typeof(decimal));

                if (poTaxList != null && poTaxList.Count > 0)
                {
                    foreach (POTaxDetail item in poTaxList)
                    {
                        DataRow dtrow = dtPOTax.NewRow();
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;

                        dtrow["SurchargeName_1"] = item.SurchargeName_1;
                        dtrow["SurchargePercentage_1"] = item.SurchargePercentage_1;
                        dtrow["SurchargeAmount_1"] = item.SurchargeAmount_1;

                        dtrow["SurchargeName_2"] = item.SurchargeName_2;
                        dtrow["SurchargePercentage_2"] = item.SurchargePercentage_2;
                        dtrow["SurchargeAmount_2"] = item.SurchargeAmount_2;

                        dtrow["SurchargeName_3"] = item.SurchargeName_3;
                        dtrow["SurchargePercentage_3"] = item.SurchargePercentage_3;
                        dtrow["SurchargeAmount_3"] = item.SurchargeAmount_3;
                        dtPOTax.Rows.Add(dtrow);
                    }
                    dtPOTax.AcceptChanges();
                }

                DataTable dtPOTerm = new DataTable();
                dtPOTerm.Columns.Add("TermDesc", typeof(string));
                dtPOTerm.Columns.Add("TermSequence", typeof(int));

                if (poTermList != null && poTermList.Count > 0)
                {
                    foreach (POTermsDetail item in poTermList)
                    {
                        DataRow dtrow = dtPOTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtPOTerm.Rows.Add(dtrow);
                    }
                    dtPOTerm.AcceptChanges();
                }


                DataTable dtREVISEDPODocument = new DataTable();
                dtREVISEDPODocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtREVISEDPODocument.Columns.Add("DocumentName", typeof(string));
                dtREVISEDPODocument.Columns.Add("DocumentPath", typeof(string));

                if (revisedPODocumentList != null && revisedPODocumentList.Count > 0)
                {
                    foreach (POSupportingDocument item in revisedPODocumentList)
                    {
                        DataRow dtrow = dtREVISEDPODocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtREVISEDPODocument.Rows.Add(dtrow);
                    }
                    dtREVISEDPODocument.AcceptChanges();
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddRevisedPO", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@POId", po.POId);
                        sqlCommand.Parameters.AddWithValue("@PONo", po.PONo);
                        sqlCommand.Parameters.AddWithValue("@PODate", po.PODate);

                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", po.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@VendorId", po.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", po.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", po.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingAddress", po.ShippingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", po.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", po.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", po.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", po.PinCode);
                        sqlCommand.Parameters.AddWithValue("@CSTNo", po.CSTNo);
                        sqlCommand.Parameters.AddWithValue("@TINNo", po.TINNo);
                        sqlCommand.Parameters.AddWithValue("@PANNo", po.PANNo);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", po.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@ExciseNo", po.ExciseNo);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", po.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@RefNo", po.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", po.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", po.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", po.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", po.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", po.ConsigneeName);

                        sqlCommand.Parameters.AddWithValue("@ShippingCity", po.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", po.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", po.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", po.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeGSTNo", po.ConsigneeGSTNo);


                        sqlCommand.Parameters.AddWithValue("@FreightValue", po.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", po.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", po.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", po.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", po.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", po.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", po.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", po.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", po.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", po.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", po.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", po.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", po.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", po.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", po.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", po.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", po.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", po.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", po.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", po.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", po.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", po.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", po.ReverseChargeAmount);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", po.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", po.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", po.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", po.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", po.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@POProductDetail", dtPOProduct);
                        sqlCommand.Parameters.AddWithValue("@POTaxDetail", dtPOTax);
                        sqlCommand.Parameters.AddWithValue("@POTermDetail", dtPOTerm);

                        sqlCommand.Parameters.AddWithValue("@REVISEDPOSupportingDocument", dtREVISEDPODocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPOId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {

                            responseOut.message = ActionMessage.RevisedPOCreatedSuccess;

                            if (sqlCommand.Parameters["@RetPOId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPOId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        #endregion

        #region MRN
        public ResponseOut AddEditMRN(MRN mrn, List<MRNProductDetail> mrnProductList, List<MRNSupportingDocument> mrnDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtmrnProduct = new DataTable();
                dtmrnProduct.Columns.Add("MRNProductDetailId", typeof(Int64));
                dtmrnProduct.Columns.Add("MRNId", typeof(Int64));
                dtmrnProduct.Columns.Add("ProductId", typeof(Int64));
                dtmrnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtmrnProduct.Columns.Add("Price", typeof(decimal));
                dtmrnProduct.Columns.Add("Quantity", typeof(decimal));
                dtmrnProduct.Columns.Add("ReceivedQuantity", typeof(decimal));
                dtmrnProduct.Columns.Add("AcceptQuantity", typeof(decimal));
                dtmrnProduct.Columns.Add("RejectQuantity", typeof(decimal));

                if (mrnProductList != null && mrnProductList.Count > 0)
                {
                    foreach (MRNProductDetail item in mrnProductList)
                    {
                        DataRow dtrow = dtmrnProduct.NewRow();
                        dtrow["MRNProductDetailId"] = 0;
                        dtrow["MRNId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["ReceivedQuantity"] = item.ReceivedQuantity;
                        dtrow["AcceptQuantity"] = item.AcceptQuantity;
                        dtrow["RejectQuantity"] = item.RejectQuantity;
                        dtmrnProduct.Rows.Add(dtrow);
                    }
                    dtmrnProduct.AcceptChanges();

                }


                DataTable dtMRNDocument = new DataTable();
                dtMRNDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtMRNDocument.Columns.Add("DocumentName", typeof(string));
                dtMRNDocument.Columns.Add("DocumentPath", typeof(string));

                if (mrnDocumentList != null && mrnDocumentList.Count > 0)
                {
                    foreach (MRNSupportingDocument item in mrnDocumentList)
                    {
                        DataRow dtrow = dtMRNDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtMRNDocument.Rows.Add(dtrow);
                    }
                    dtMRNDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditMRN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@MRNId", mrn.MRNId);
                        sqlCommand.Parameters.AddWithValue("@MRNDate", mrn.MRNDate);
                        sqlCommand.Parameters.AddWithValue("@GRNo", mrn.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", mrn.GRDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", mrn.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", mrn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@POId", mrn.POId);
                        sqlCommand.Parameters.AddWithValue("@PONo", mrn.PONo);
                        sqlCommand.Parameters.AddWithValue("@ChallanNo", mrn.ChallanNo);
                        sqlCommand.Parameters.AddWithValue("@VendorId", mrn.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", mrn.VendorName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", mrn.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", mrn.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", mrn.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", mrn.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", mrn.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", mrn.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", mrn.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", mrn.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", mrn.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", mrn.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", mrn.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", mrn.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", mrn.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", mrn.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", mrn.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", mrn.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", mrn.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", mrn.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", mrn.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", mrn.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", mrn.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", mrn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", mrn.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", mrn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@MRNProductDetail", dtmrnProduct);

                        sqlCommand.Parameters.AddWithValue("@MRNSupportingDocument", dtMRNDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetMRNId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", mrn.ApprovalStatus);

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (mrn.MRNId == 0)
                            {
                                responseOut.message = ActionMessage.MRNCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.MRNUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetMRNId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetMRNId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetMRNProductList(long mrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNId", mrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetMRNDetail(long mrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNId", mrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetMRNList(string mrnNo, string vendorName, string dispatchrefNo, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MRNNo", mrnNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetMRNSupportingDocumentList(long mrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@mrnId", mrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Quotation Register
        public DataTable GetQuotationRegisterList(int customerId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateQuotationRegisterReports(int customerId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetQuotationRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region  PO Register
        public DataTable GetPORegisterList(string vendorId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPORegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  PurchaseInvoiceRegister
        public DataTable GetPurchaseInvoiceRegisterList(string vendorId, int stateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseInvoiceRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  SO Register
        public DataTable GetSORegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSORegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateSORegisterReports(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSORegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region  Sale Invoice Register
        public DataTable GetSaleInvoiceRegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateSaleInvoiceRegisterReports(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleInvoiceRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  MRN Register
        public DataTable GetMRNRegisterList(int vendorId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetMRNRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion 


        #region STN
        public ResponseOut AddEditSTN(STN stn, List<STNProductDetail> stnProductList, List<STNSupportingDocument> stnDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtstnProduct = new DataTable();
                dtstnProduct.Columns.Add("STNProductDetailId", typeof(Int64));
                dtstnProduct.Columns.Add("STNId", typeof(Int64));
                dtstnProduct.Columns.Add("ProductId", typeof(Int64));
                dtstnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtstnProduct.Columns.Add("Price", typeof(decimal));
                dtstnProduct.Columns.Add("Quantity", typeof(decimal));
                dtstnProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (stnProductList != null && stnProductList.Count > 0)
                {
                    foreach (STNProductDetail item in stnProductList)
                    {
                        DataRow dtrow = dtstnProduct.NewRow();
                        dtrow["STNProductDetailId"] = 0;
                        dtrow["STNId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["TotalPrice"] = item.TotalPrice;

                        dtstnProduct.Rows.Add(dtrow);
                    }
                    dtstnProduct.AcceptChanges();

                }

                DataTable dtSTNDocument = new DataTable();
                dtSTNDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtSTNDocument.Columns.Add("DocumentName", typeof(string));
                dtSTNDocument.Columns.Add("DocumentPath", typeof(string));

                if (stnDocumentList != null && stnDocumentList.Count > 0)
                {
                    foreach (STNSupportingDocument item in stnDocumentList)
                    {
                        DataRow dtrow = dtSTNDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtSTNDocument.Rows.Add(dtrow);
                    }
                    dtSTNDocument.AcceptChanges();
                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSTN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@STNId", stn.STNId);
                        sqlCommand.Parameters.AddWithValue("@STNDate", stn.STNDate);
                        sqlCommand.Parameters.AddWithValue("@GRNo", stn.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", stn.GRDate);
                        // sqlCommand.Parameters.AddWithValue("@InvoiceId", stn.InvoiceId);
                        //  sqlCommand.Parameters.AddWithValue("@InvoiceNo", stn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", stn.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@FromWarehouseId", stn.FromWarehouseId);
                        sqlCommand.Parameters.AddWithValue("@ToWarehouseId", stn.ToWarehouseId);

                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", stn.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", stn.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", stn.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", stn.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", stn.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", stn.NoOfPackets);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", stn.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", stn.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", stn.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", stn.LoadingValue);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", stn.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", stn.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", stn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", stn.CompanyId);


                        sqlCommand.Parameters.AddWithValue("@CreatedBy", stn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@STNProductDetail", dtstnProduct);

                        sqlCommand.Parameters.AddWithValue("@STNSupportingDocument", dtSTNDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSTNId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", stn.ApprovalStatus);
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (stn.STNId == 0)
                            {
                                responseOut.message = ActionMessage.STNCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.STNUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSTNId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSTNId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSTNProductList(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTNList(string stnNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, string displayType, string approvalStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTNDetail(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GetSTNSupportingDocumentList(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region STR
        public ResponseOut AddEditSTR(STR str, List<STRProductDetail> strProductList, List<STRSupportingDocument> strDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtstrProduct = new DataTable();
                dtstrProduct.Columns.Add("STRProductDetailId", typeof(Int64));
                dtstrProduct.Columns.Add("STRId", typeof(Int64));
                dtstrProduct.Columns.Add("ProductId", typeof(Int64));
                dtstrProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtstrProduct.Columns.Add("Price", typeof(decimal));
                dtstrProduct.Columns.Add("Quantity", typeof(decimal));
                dtstrProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (strProductList != null && strProductList.Count > 0)
                {
                    foreach (STRProductDetail item in strProductList)
                    {
                        DataRow dtrow = dtstrProduct.NewRow();
                        dtrow["STRProductDetailId"] = 0;
                        dtrow["STRId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtstrProduct.Rows.Add(dtrow);
                    }
                    dtstrProduct.AcceptChanges();

                }

                DataTable dtSTRDocument = new DataTable();
                dtSTRDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtSTRDocument.Columns.Add("DocumentName", typeof(string));
                dtSTRDocument.Columns.Add("DocumentPath", typeof(string));

                if (strDocumentList != null && strDocumentList.Count > 0)
                {
                    foreach (STRSupportingDocument item in strDocumentList)
                    {
                        DataRow dtrow = dtSTRDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtSTRDocument.Rows.Add(dtrow);
                    }
                    dtSTRDocument.AcceptChanges();
                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSTR", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@STRId", str.STRId);
                        sqlCommand.Parameters.AddWithValue("@STRDate", str.STRDate);

                        sqlCommand.Parameters.AddWithValue("@STNId", str.STNId);
                        sqlCommand.Parameters.AddWithValue("@STNNo", str.STNNo);


                        sqlCommand.Parameters.AddWithValue("@GRNo", str.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", str.GRDate);

                        sqlCommand.Parameters.AddWithValue("@ContactPerson", str.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@FromWarehouseId", str.FromWarehouseId);
                        sqlCommand.Parameters.AddWithValue("@ToWarehouseId", str.ToWarehouseId);

                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", str.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", str.DispatchRefDate);


                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", str.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@LRNo", str.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", str.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", str.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", str.NoOfPackets);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", str.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", str.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", str.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", str.LoadingValue);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", str.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", str.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", str.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", str.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", str.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@STRProductDetail", dtstrProduct);

                        sqlCommand.Parameters.AddWithValue("@STRSupportingDocument", dtSTRDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSTRId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (str.STRId == 0)
                            {
                                responseOut.message = ActionMessage.STRCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.STRUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSTRId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSTRId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSTRProductList(long stnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRId", stnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTRList(string stnNo, string grNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", grNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSTRDetail(long strId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRId", strId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        public DataTable GetSTRSupportingDocumentList(long strId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRId", strId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Stock Ledger
        public DataTable GetStockLedgerDetail(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintStockLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetStockLedgerSummary(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintStockSummary", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public decimal GetProductAvailableStock(long productId, int finYearId, int companyId, int companyBranchId, int trnId, string trnType)
        {
            decimal availableStock = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_GetProductAvailableStock", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@FinYearId", finYearId);
                        cmd.Parameters.AddWithValue("@CompanyId", companyId);
                        cmd.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                        cmd.Parameters.AddWithValue("@TrnId", trnId);
                        cmd.Parameters.AddWithValue("@TrnType", trnType);
                        object qty = cmd.ExecuteScalar();
                        if (qty != null)
                        {
                            availableStock = Convert.ToDecimal(qty);
                        }
                        else
                        {
                            availableStock = 0;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return availableStock;
        }

        public DataTable GetStockLedgerList(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_StockLedgerList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetStockLedgerDrilDownList(long productId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_DrilDownStockLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@productId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetStockLedgerReports(long productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_StockLedgerList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetStockLedgerReport(long productId, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_DrilDownStockLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@productId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public string GetBranchName(int companyBranchID)
        {
            string str = "";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBranchName", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@companyBranchID", companyBranchID);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        str = Convert.ToString(dt.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return str;

        }
        #endregion

        #region  DeliveryChallanRegister

        public DataTable GetDeliveryChallanRegisterList(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDeliveryChallanRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GenerateDeliveryChallanRegisterReports(int customerId, int stateId, int shippingstateId, DateTime fromDate, DateTime toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDeliveryChallanRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region CustomerFormRegister
        public DataTable GetCFormRegisterList(string formStatus, int customerId, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFormRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", formStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateCustomerFormRegisterReports(string formStatus, int customerId, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFormRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", formStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region STN Register

        public DataTable GetSTNRegisterList(string stnNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTNRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STNNo", stnNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region STR Register
        public DataTable GetSTRRegisterList(string strNo, string glNo, int fromLocation, int toLocation, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSTRRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@STRNo", strNo);
                    da.SelectCommand.Parameters.AddWithValue("@GRNo", glNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocation);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocation);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region CustomerPaymentRegister
        public DataTable GetCustomerPaymentRegisterList(int customerId, int paymentModeId, string sortBy, string sortOrder, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerPaymentRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.SelectCommand.Parameters.AddWithValue("@FormPaymentDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToPaymentDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region VendorPaymentRegister
        public DataTable GetVendorPaymentRegisterList(int vendorId, int paymentModeId, string sortBy, string sortOrder, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorPaymentRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@PaymentModeId", paymentModeId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.SelectCommand.Parameters.AddWithValue("@FormPaymentDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToPaymentDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId); ;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region UserEmailSetting 
        public DataTable GetUserEmailSettingList(string fullName, string userName)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUsereMailsettings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FullName", fullName);
                    da.SelectCommand.Parameters.AddWithValue("@UserName", userName);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserEmailSettingDetail(int settingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserEmailSettingDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SettingId", settingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUserEmailSettingDetailByUserId(int userId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUserEmailSettingByUserId", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Sale Summary Register
        public DataTable GetSaleSummaryRegister(int customerId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintSaleSummaryRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@Userid", userId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GenerateSaleInvoiceSummaryReports(int customerId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintSaleSummaryRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@Userid", userId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region VendorForm
        public DataTable GetVendorFormList(string FormStatus, string vendorName, string invoiceNo, string refNo, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorForm", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FormStatus", FormStatus);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVendorFormDetail(long vendorFormTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVendorFormDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorFormTrnId", vendorFormTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion



        #region Purchase Summary Register
        public DataTable GetPurchaseSummaryRegister(int vendorId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_PrintPurchaseSummaryRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@Userid", userId);
                    da.SelectCommand.Parameters.AddWithValue("@StateId", stateId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region GL
        public ResponseOut AddEditGL(GL gl, GLDetail glDetail)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditGL", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@GLId", gl.GLId);
                        sqlCommand.Parameters.AddWithValue("@GLCode", gl.GLCode);
                        sqlCommand.Parameters.AddWithValue("@GLHead", gl.GLHead);
                        sqlCommand.Parameters.AddWithValue("@GLType", gl.GLType);
                        sqlCommand.Parameters.AddWithValue("@GLSubGroupId", gl.GLSubGroupId);
                        sqlCommand.Parameters.AddWithValue("@SLTypeId", gl.SLTypeId);
                        sqlCommand.Parameters.AddWithValue("@IsBookGL", gl.IsBookGL);
                        sqlCommand.Parameters.AddWithValue("@IsBranchGL", gl.IsBranchGL);
                        sqlCommand.Parameters.AddWithValue("@IsDebtorGL", gl.IsDebtorGL);

                        sqlCommand.Parameters.AddWithValue("@IsCreditorGL", gl.IsCreditorGL);
                        sqlCommand.Parameters.AddWithValue("@IsTaxGL", gl.IsTaxGL);
                        sqlCommand.Parameters.AddWithValue("@IsPostGL", gl.IsPostGL);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", gl.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", gl.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@GLStatus", gl.Status);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", glDetail.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceDebit", glDetail.OpeningBalanceDebit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceCredit", glDetail.OpeningBalanceCredit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalance", glDetail.OpeningBalance);
                        sqlCommand.Parameters.AddWithValue("@GLDetailStatus", gl.Status);


                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }

                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (gl.GLId == 0)
                            {
                                responseOut.message = ActionMessage.GLCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.GLUpdatedSuccess;
                            }

                        }





                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public ResponseOut AddEditGLDetail(GLDetail glDetail)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditGLDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@GLId", glDetail.GLId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", glDetail.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceDebit", glDetail.OpeningBalanceDebit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalanceCredit", glDetail.OpeningBalanceCredit);
                        sqlCommand.Parameters.AddWithValue("@OpeningBalance", glDetail.OpeningBalance);
                        sqlCommand.Parameters.AddWithValue("@GLDetailStatus", glDetail.Status);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", glDetail.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", glDetail.CreatedBy);


                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }

                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            //if (gl.GLId == 0)
                            //{
                            //    responseOut.message = ActionMessage.GLCreatedSuccess;
                            //}
                            //else
                            //{
                            //    responseOut.message = ActionMessage.GLUpdatedSuccess;
                            //}

                        }





                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetGLList(string GLHead = "", string GLCode = "", string GLType = "", int GLMainGroupId = 0, int GLSubGroupId = 0, int SLTypeId = 0, int companyId = 0, int finYear = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLHead", GLHead);
                    da.SelectCommand.Parameters.AddWithValue("@GLCode", GLCode);
                    da.SelectCommand.Parameters.AddWithValue("@GLType", GLType);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", GLMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", GLSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYear", finYear);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetGLDetail(int GLId, int finYearId = 2017)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGLDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GLId", GLId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetGLAutoCompleteList(string searchTerm, int bookId, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAutoCompleteGLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SearchTerms", searchTerm);
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetJVGLAutoCompleteList(string searchTerm, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAutoCompleteJVGLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SearchTerms", searchTerm);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Bank Voucher

        public DataTable GetBankVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditBankVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList, List<VoucherSupportingDocument> voucherDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }

                DataTable dtVoucherDocument = new DataTable();
                dtVoucherDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtVoucherDocument.Columns.Add("DocumentName", typeof(string));
                dtVoucherDocument.Columns.Add("DocumentPath", typeof(string));

                if (voucherDocumentList != null && voucherDocumentList.Count > 0)
                {
                    foreach (VoucherSupportingDocument item in voucherDocumentList)
                    {
                        DataRow dtrow = dtVoucherDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtVoucherDocument.Rows.Add(dtrow);
                    }
                    dtVoucherDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditBankVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.AddWithValue("@VoucherSupportingDocument", dtVoucherDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.BankVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.BankVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetBankVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetBankVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }



        public DataTable GenerateBankVoucherReports(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetApprovedBankVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetBankVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVoucherSupportingDocumentList(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVoucherSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion


        #region Journal Voucher

        public DataTable GetJournalVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJournalVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditJournalVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList, List<VoucherSupportingDocument> journalvoucherDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }

                DataTable dtJournalVoucherDocument = new DataTable();
                dtJournalVoucherDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtJournalVoucherDocument.Columns.Add("DocumentName", typeof(string));
                dtJournalVoucherDocument.Columns.Add("DocumentPath", typeof(string));

                if (journalvoucherDocumentList != null && journalvoucherDocumentList.Count > 0)
                {
                    foreach (VoucherSupportingDocument item in journalvoucherDocumentList)
                    {
                        DataRow dtrow = dtJournalVoucherDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtJournalVoucherDocument.Rows.Add(dtrow);
                    }
                    dtJournalVoucherDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditJournalVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.AddWithValue("@JournalVoucherSupportingDocument", dtJournalVoucherDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.JournalVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.JournalVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetJournalVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJournalVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetJournalVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJournalVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateJournalVoucherReports(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJournalVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetApprovedJournalVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJournalVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetJournalVoucherSupportingDocumentList(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVoucherSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Credit Note Voucher

        public DataTable GetCreditNoteVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCreditNoteVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditCreditNoteVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList, List<VoucherSupportingDocument> creditNotevoucherDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }


                DataTable dtCreditNoteVoucherDocument = new DataTable();
                dtCreditNoteVoucherDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtCreditNoteVoucherDocument.Columns.Add("DocumentName", typeof(string));
                dtCreditNoteVoucherDocument.Columns.Add("DocumentPath", typeof(string));

                if (creditNotevoucherDocumentList != null && creditNotevoucherDocumentList.Count > 0)
                {
                    foreach (VoucherSupportingDocument item in creditNotevoucherDocumentList)
                    {
                        DataRow dtrow = dtCreditNoteVoucherDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtCreditNoteVoucherDocument.Rows.Add(dtrow);
                    }
                    dtCreditNoteVoucherDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditCreditNoteVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.AddWithValue("@CreditNoteVoucherSupportingDocument", dtCreditNoteVoucherDocument);


                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.CreditNoteVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.CreditNoteVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetCreditNoteVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCreditNoteVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetCreditNoteVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCreditNoteVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetApprovedCreditNoteVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCreditNoteVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }




        #endregion

        #region Cost Center
        public DataTable GetCostCenterList(string costcenterName, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCostCenters", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterName", costcenterName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCostCenterDetail(int costcenterId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCostCenterDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterId", costcenterId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region SL
        public DataTable GetSLList(string SLHead, string SLCode, int SLTypeId, int CostCenterId, int CompanyId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLHead", SLHead);
                    da.SelectCommand.Parameters.AddWithValue("@SLCode", SLCode);
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CostCenterId", CostCenterId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSLDetail(int sLId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLId", sLId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region SL Detail
        public DataTable GetSLDetailList(int SLTypeId, int GLId, int SLId, int FinYearId, int CompanyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSLDetailList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SLTypeId", SLTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", GLId);
                    da.SelectCommand.Parameters.AddWithValue("@SLId", SLId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", FinYearId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Cash Voucher

        public DataTable GetCashVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCashVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditCashVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList, List<VoucherSupportingDocument> cashvoucherDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }
                DataTable dtCashVoucherDocument = new DataTable();
                dtCashVoucherDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtCashVoucherDocument.Columns.Add("DocumentName", typeof(string));
                dtCashVoucherDocument.Columns.Add("DocumentPath", typeof(string));

                if (cashvoucherDocumentList != null && cashvoucherDocumentList.Count > 0)
                {
                    foreach (VoucherSupportingDocument item in cashvoucherDocumentList)
                    {
                        DataRow dtrow = dtCashVoucherDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtCashVoucherDocument.Rows.Add(dtrow);
                    }
                    dtCashVoucherDocument.AcceptChanges();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditCashVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.AddWithValue("@CashVoucherSupportingDocument", dtCashVoucherDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.CashVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.CashVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetCashVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCashVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetCashVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCashVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GenerateCashVoucherReports(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCashVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetApprovedCashVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCashVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCashVoucherSupportingDocumentList(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVoucherSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Debit Note Voucher

        public DataTable GetDebitNoteVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDebitNoteVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditDebitNoteVoucher(Voucher voucher, List<VoucherDetail> voucherEntryList, List<VoucherSupportingDocument> debitNotevoucherDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtVoucherDetail = new DataTable();
                dtVoucherDetail.Columns.Add("SequenceNo", typeof(Int16));
                dtVoucherDetail.Columns.Add("EntryMode", typeof(string));
                dtVoucherDetail.Columns.Add("GLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("GLCode", typeof(string));
                dtVoucherDetail.Columns.Add("SLId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SLCode", typeof(string));
                dtVoucherDetail.Columns.Add("Narration", typeof(string));
                dtVoucherDetail.Columns.Add("PaymentModeId", typeof(Int16));
                dtVoucherDetail.Columns.Add("ChequeRefNo", typeof(string));
                dtVoucherDetail.Columns.Add("ChequeRefDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("Amount", typeof(decimal));
                dtVoucherDetail.Columns.Add("CostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("SubCostCenterId", typeof(Int32));
                dtVoucherDetail.Columns.Add("ValueDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("DrawnOnBank", typeof(string));
                dtVoucherDetail.Columns.Add("PO_SONo", typeof(string));
                dtVoucherDetail.Columns.Add("BillNo", typeof(string));
                dtVoucherDetail.Columns.Add("BillDate", typeof(DateTime));
                dtVoucherDetail.Columns.Add("PayeeId", typeof(Int32));
                dtVoucherDetail.Columns.Add("PayeeName", typeof(string));

                if (voucherEntryList != null && voucherEntryList.Count > 0)
                {
                    foreach (VoucherDetail item in voucherEntryList)
                    {
                        DataRow dtrow = dtVoucherDetail.NewRow();
                        dtrow["SequenceNo"] = item.SequenceNo;
                        dtrow["EntryMode"] = item.EntryMode;
                        dtrow["GLId"] = item.GLId;
                        dtrow["GLCode"] = item.GLCode;
                        dtrow["SLId"] = item.SLId;
                        dtrow["SLCode"] = item.SLCode;
                        dtrow["Narration"] = item.Narration;
                        dtrow["PaymentModeId"] = item.PaymentModeId;
                        dtrow["ChequeRefNo"] = item.ChequeRefNo;
                        dtrow["ChequeRefDate"] = item.ChequeRefDate;
                        dtrow["Amount"] = item.Amount;
                        dtrow["CostCenterId"] = item.CostCenterId;
                        dtrow["SubCostCenterId"] = item.SubCostCenterId;
                        dtrow["ValueDate"] = item.ValueDate;
                        dtrow["DrawnOnBank"] = item.DrawnOnBank;
                        dtrow["PO_SONo"] = item.PO_SONo;
                        dtrow["BillNo"] = item.BillNo;
                        dtrow["BillDate"] = item.BillDate;
                        dtrow["PayeeId"] = item.PayeeId;
                        dtrow["PayeeName"] = item.PayeeName;
                        dtVoucherDetail.Rows.Add(dtrow);
                    }
                    dtVoucherDetail.AcceptChanges();

                }

                DataTable dtDebitNoteVoucherDocument = new DataTable();
                dtDebitNoteVoucherDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtDebitNoteVoucherDocument.Columns.Add("DocumentName", typeof(string));
                dtDebitNoteVoucherDocument.Columns.Add("DocumentPath", typeof(string));

                if (debitNotevoucherDocumentList != null && debitNotevoucherDocumentList.Count > 0)
                {
                    foreach (VoucherSupportingDocument item in debitNotevoucherDocumentList)
                    {
                        DataRow dtrow = dtDebitNoteVoucherDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtDebitNoteVoucherDocument.Rows.Add(dtrow);
                    }
                    dtDebitNoteVoucherDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditDebitNoteVoucher", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                        sqlCommand.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        sqlCommand.Parameters.AddWithValue("@VoucherMode", voucher.VoucherMode);
                        sqlCommand.Parameters.AddWithValue("@VoucherAmount", voucher.VoucherAmount);
                        sqlCommand.Parameters.AddWithValue("@PayeeSLTypeId", voucher.PayeeSLTypeId);
                        sqlCommand.Parameters.AddWithValue("@BookId", voucher.BookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", voucher.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", voucher.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@VoucherStatus", voucher.VoucherStatus);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@VoucherDetail", dtVoucherDetail);

                        sqlCommand.Parameters.AddWithValue("@DebitNoteVoucherSupportingDocument", dtDebitNoteVoucherDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetVoucherId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (voucher.VoucherId == 0)
                            {
                                responseOut.message = ActionMessage.DebitNoteVoucherCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.DebitNoteVoucherUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetVoucherId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetVoucherId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetDebitNoteVoucherDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDebitNoteVoucherDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDebitNoteVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDebitNoteVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetApprovedDebitNoteVoucherList(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDebitNoteVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetDebitNoteVoucherSupportingDocumentList(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVoucherSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GenerateDebitNoteVoucherReports(Int32 bookId, string voucherMode, string voucherNo, string voucherStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDebitNoteVouchers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@BookId", bookId);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherMode", voucherMode);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherNo", voucherNo);
                    da.SelectCommand.Parameters.AddWithValue("@VoucherStatus", voucherStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Bank Book Print

        public ResponseOut GenerateBankBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintBankBook", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetBankBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region Journal Book Print

        public ResponseOut GenerateJournalBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetJournalBookDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetJournalBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Cash Book Print

        public ResponseOut GenerateCashBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintCashBook", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetCashBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Sale Book Print

        public ResponseOut GenerateSaleBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetSaleBookDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSaleBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        # region Purchase Book Print

        public ResponseOut GeneratePurchaseBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetPurchaseBookDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPurchaseBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Credit Note Book Print

        public ResponseOut GenerateCreditNoteBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetCreditNoteBookDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetCreditNoteBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Debit Note Book Print

        public ResponseOut GenerateDebitNoteBook(int bookId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetDebitNoteBookDetail", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetDebitNoteBookDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPCASHBANKBOOKPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region General Ledger Print

        public ResponseOut GenerateGeneralLedgerWithoutFinalcialYear(int glId, int companyId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintGeneralLedgerWithoutFinancialYear", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public decimal GetGeneralLedgerBalance(int glId, int companyId, int finYearId)
        {
            decimal glBalance = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetGeneralLedgerBalance", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        con.Open();
                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                        {
                            glBalance = Convert.ToDecimal(result);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return glBalance;

        }

        public DataTable GetGeneralLedgerWithoutFinancialYearDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPGLLEDGERPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Sub Ledger Print

        public ResponseOut GenerateSubLedgerWithoutFinalcialYear(int slTypeId, int glId, long slId, int companyId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintSubLedgerWithoutFinancialYear", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PSLTypeId", slTypeId);
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@PSLId", slId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSubLedgerWithoutFinancialYearDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPSLLEDGERPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public decimal GetSubLedgerBalance(int glId, long slId, int companyId, int finYearId)
        {
            decimal slBalance = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetSubLedgerBalance", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@PSLId", slId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        con.Open();
                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                        {
                            slBalance = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return slBalance;
        }
        #endregion


        #region Product GL Mapping 
        public DataTable GetProductGLMappingList(int productSubGroupId, int glId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductGLMapping", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Productsubgroupid", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@glId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProductGLDetail(int MappingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProductGLDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MappingId", MappingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region GL Trial

        public ResponseOut GenerateGLTrial2Column(int companyId, int finYearId, DateTime asOnDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_Print2ColumnGLTrialBalance", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@AsOnDate", asOnDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetGLTrial2ColumnDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPGLTRIALBALANCEPRINT_2COLUMN where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region SL Trial

        public ResponseOut GenerateSLTrial(int slTypeId, int glId, int companyId, int finYearId, DateTime asOnDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintSLTrialBalance", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PSLTypeId", slTypeId);
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@AsOnDate", asOnDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSLTrialDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPSLTRIALBALANCEPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Profit Loss Statement

        public ResponseOut GenerateProfitLossStatement(int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintProfitLossStatement", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetProfitLossStatementDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPPROFITLOSSSTATEMENTPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public Decimal GetNetProfitLoss(int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            decimal netProfitLoss = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GetNetProfitLoss", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@NetProfitLoss", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@NetProfitLoss"].Value != null)
                        {
                            netProfitLoss = Convert.ToDecimal(sqlCommand.Parameters["@NetProfitLoss"].Value);
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                netProfitLoss = 0;
                throw ex;
            }
            return netProfitLoss;

        }

        #endregion
        #region Balance Sheet

        public ResponseOut GenerateBalanceSheet(int companyId, int finYearId, DateTime asOnDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_PrintBalanceSheet", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@AsOnDate", asOnDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetBalanceSheetDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPBALANCESHEETPRINT where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Trial Balance Drill Down

        public ResponseOut GenerateTrialBalanceDrillDown(int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_TrialBalanceDrillDown", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetTrialBalanceDrillDownDetail(int reportUserId, string sessionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            string selectQuery = "Select * from TEMPSLTRIALBALANCEDRILLDOWN where ReportUserId=" + reportUserId + " and SessionId='" + sessionId + "'";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter(selectQuery, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetTBDrillDown_GLTypeList(int reportUserId, string sessionId, int glMainGroupId = 0, int glSubGroupId = 0, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_TBDrillDown_GetGLTypeList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTBDrillDown_MainGroupList(int reportUserId, string sessionId, int glMainGroupId = 0, int glSubGroupId = 0, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_TBDrillDown_GetMainGroupList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTBDrillDown_SubGroupList(int reportUserId, string sessionId, int glMainGroupId = 0, int glSubGroupId = 0, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_TBDrillDown_GetSubGroupList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTBDrillDown_GLWiseList(int reportUserId, string sessionId, int glMainGroupId = 0, int glSubGroupId = 0, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_TBDrillDown_GetGLWiseList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTBDrillDown_SLWiseList(int reportUserId, string sessionId, int glMainGroupId = 0, int glSubGroupId = 0, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_TBDrillDown_GetSLWiseList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLMainGroupId", glMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLSubGroupId", glSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut GenerateGLLedgerDrillDown(int glId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_GeneralLedgerDrillDown", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetGLDrillDown_GLOpening(int reportUserId, string sessionId, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GLDrillDown_GetGLOpening", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetGLDrillDown_GLLedger(int reportUserId, string sessionId, int glId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GLDrillDown_GetGLLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut GenerateSubLedgerDrillDown(int glId, Int32 slId, int companyId, int finYearId, DateTime fromDate, DateTime toDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_SubLedgerDrillDown", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PGLId", glId);
                        sqlCommand.Parameters.AddWithValue("@PSLId", slId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", companyId);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                        sqlCommand.Parameters.AddWithValue("@FromDate", fromDate);
                        sqlCommand.Parameters.AddWithValue("@ToDate", toDate);
                        sqlCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                        sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSLDrillDown_SLOpening(int reportUserId, string sessionId, int glId = 0, Int32 slId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_SLDrillDown_GetSLOpening", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.SelectCommand.Parameters.AddWithValue("@SLId", slId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSLDrillDown_SLLedger(int reportUserId, string sessionId, int glId = 0, Int32 slId = 0)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_SLDrillDown_GetSLLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ReportUserId", reportUserId);
                    da.SelectCommand.Parameters.AddWithValue("@SessionId", sessionId);
                    da.SelectCommand.Parameters.AddWithValue("@GLId", glId);
                    da.SelectCommand.Parameters.AddWithValue("@SLId", slId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Account Dashboard Data
        public DataTable GetDashboard_BookBalance(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetBookBalances", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_MonthWiseBankCashTransactionSummary(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetMonthWiseBankCashTransactionSummary", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        #endregion


        #region Sale Dashboard Data
        public DataTable GetDashboard_QuatationCount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetQuatationCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_SaleOrderCount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSaleOrderCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_TotalSaleCount(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSOTotalCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_SICount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetSICount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }


        #endregion
        #region Purchase Dashboard Data
        public DataTable GetDashboard_POCount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetPOCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPendingPOCountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetPendingMDApprovalPOCount", con);//proc_Dashboard_GetPendingMDApprovalPOCount
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPendingPOLessThan25KCountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetPendingPOLessThan25KCount", con);//proc_Dashboard_GetPendingMDApprovalPOCount
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_PICount(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetPICount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_TodayPOSumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayPOSumAmount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_TodayPISumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayPISumAmount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPOPendingForMDApprovals(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardPOPendingForMDApproval", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPORejectedByMD(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardPORejectByMD", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region CRM Dashboard Data
        public DataTable GetDashboard_LeadStatusCount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetLeadStatusCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_LeadSourceCount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetLeadSourceCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_LeadFollowUpCount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetLeadFollowUpCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_DateWiseLeadConversionCount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetDateWiseLeadConversionCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDashboard_LeadFollowUpReminderDueDateCount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetLeadFollowUpReminderDueDateCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_LeadFollowUpReminderDueDateList(int companyId, int userId, int reportingUserId, int reportingRoleId, int FollowUpActivityTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetLeadFollowUpReminderDueDateList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.SelectCommand.Parameters.AddWithValue("@FollowUpActivityTypeId", FollowUpActivityTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Inventory Dashboard
        public DataTable GetDashboard_ReorderPointProductCount(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetReorderPointProductCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public int GetTotalProductCount(int companyId)
        {
            int totalProductCount = 0;
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTotalProductCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        totalProductCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return totalProductCount;
        }
        public int GetTodayProduct(int companyId)
        {

            int todayProductCount = 0;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetTodayNewProduct", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        todayProductCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return todayProductCount;
        }
        public DataTable GetDashboard_InOutProductQuantityCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_MiniStockSummary", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboard_SINProductQuantityCount(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardSINProductQuantityCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Customer Follow Up
        public DataTable GetCustomerFollowUpList(int customerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerFollowUp", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@customerId", customerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Sale Voucher

        public DataTable GetSaleVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Purchase Voucher

        public DataTable GetPurchaseVoucherEntryDetail(long voucherId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseVoucherEntryDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VoucherId", voucherId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Resource Requisition
        public ResponseOut AddEditResourceRequisition(HR_ResourceRequisition resourceRequisition, List<HR_ResourceRequisitionSkill> resourceRequisitionSkillList, List<HR_ResourceRequisitionInterviewStage> resourceRequisitionInterviewStageList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtResourcesRequisitionSkill = new DataTable();
                dtResourcesRequisitionSkill.Columns.Add("SkillId", typeof(int));
                if (resourceRequisitionSkillList != null && resourceRequisitionSkillList.Count > 0)
                {
                    foreach (HR_ResourceRequisitionSkill item in resourceRequisitionSkillList)
                    {
                        DataRow dtrow = dtResourcesRequisitionSkill.NewRow();
                        dtrow["SkillId"] = item.SkillId;
                        dtResourcesRequisitionSkill.Rows.Add(dtrow);
                    }
                    dtResourcesRequisitionSkill.AcceptChanges();
                }

                DataTable dtResourcesRequisitionInterviewStages = new DataTable();
                dtResourcesRequisitionInterviewStages.Columns.Add("InterviewTypeId", typeof(int));
                dtResourcesRequisitionInterviewStages.Columns.Add("InterviewDescription", typeof(string));
                dtResourcesRequisitionInterviewStages.Columns.Add("InterviewAssignToUserId", typeof(int));

                if (resourceRequisitionInterviewStageList != null && resourceRequisitionInterviewStageList.Count > 0)
                {
                    foreach (HR_ResourceRequisitionInterviewStage item in resourceRequisitionInterviewStageList)
                    {
                        DataRow dtrow = dtResourcesRequisitionInterviewStages.NewRow();
                        dtrow["InterviewTypeId"] = item.InterviewTypeId;
                        dtrow["InterviewDescription"] = item.InterviewDescription;
                        dtrow["InterviewAssignToUserId"] = item.InterviewAssignToUserId;
                        dtResourcesRequisitionInterviewStages.Rows.Add(dtrow);
                    }
                    dtResourcesRequisitionInterviewStages.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditResourcesRequisition", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", resourceRequisition.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@NumberOfResources", resourceRequisition.NumberOfResources);
                        sqlCommand.Parameters.AddWithValue("@PositionLevelId", resourceRequisition.PositionLevelId);
                        sqlCommand.Parameters.AddWithValue("@PriorityLevel", resourceRequisition.PriorityLevel);
                        sqlCommand.Parameters.AddWithValue("@PositionTypeId", resourceRequisition.PositionTypeId);
                        sqlCommand.Parameters.AddWithValue("@ContractPeriod", resourceRequisition.ContractPeriod);
                        sqlCommand.Parameters.AddWithValue("@DepartmentId", resourceRequisition.DepartmentId);
                        sqlCommand.Parameters.AddWithValue("@DesignationId", resourceRequisition.DesignationId);
                        sqlCommand.Parameters.AddWithValue("@EducationId", resourceRequisition.EducationId);

                        sqlCommand.Parameters.AddWithValue("@JobDescription", resourceRequisition.JobDescription);
                        sqlCommand.Parameters.AddWithValue("@OtherQualification", resourceRequisition.OtherQualification);
                        sqlCommand.Parameters.AddWithValue("@MinExp", resourceRequisition.MinExp);
                        sqlCommand.Parameters.AddWithValue("@MaxExp", resourceRequisition.MaxExp);
                        sqlCommand.Parameters.AddWithValue("@MinSalary", resourceRequisition.MinSalary);
                        sqlCommand.Parameters.AddWithValue("@MaxSalary", resourceRequisition.MaxSalary);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", resourceRequisition.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@Remarks", resourceRequisition.Remarks);
                        sqlCommand.Parameters.AddWithValue("@JustificationNotes", resourceRequisition.JustificationNotes);
                        sqlCommand.Parameters.AddWithValue("@InterviewStartDate", resourceRequisition.InterviewStartDate);
                        sqlCommand.Parameters.AddWithValue("@HireByDate", resourceRequisition.HireByDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionStatus", resourceRequisition.RequisitionStatus);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", resourceRequisition.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", resourceRequisition.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@HR_RequisitionSkill", dtResourcesRequisitionSkill);
                        sqlCommand.Parameters.AddWithValue("@HR_RequisitionInterviewStages", dtResourcesRequisitionInterviewStages);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetRequisitionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }
                        }
                        else
                        {
                            if (resourceRequisition.RequisitionId == 0)
                            {
                                responseOut.message = ActionMessage.ResourceRequisitionCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.ResourceRequisitionUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetRequisitionId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetRequisitionId"].Value);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetResourceRequisitionSkillList(long resourceRequisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResourceRequisitionSkills", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ResourceRequisitionId", resourceRequisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetResourceRequisitionInterviewStageList(long resourceRequisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResourceRequisitionInterviewStages", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ResourceRequisitionId", resourceRequisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetResourceRequisitionDetail(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResourceRequisitionDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetResourceRequisitionList(string requisitionNo, int positionLevelId, string priorityLevel, int positionTypeId, int departmentId, string approvalStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResourceRequisitions", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@PositionLevelId", positionLevelId);
                    da.SelectCommand.Parameters.AddWithValue("@PriorityLevel", priorityLevel);
                    da.SelectCommand.Parameters.AddWithValue("@PositionTypeId", positionTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);


                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion
        #region Resource Requisition Approval
        public DataTable GetResourceRequisitionApprovalList(string requisitionNo, int positionLevelId, string priorityLevel, int positionTypeId, int departmentId, string approvalStatus, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResourceRequisitionApprovalList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@PositionLevelId", positionLevelId);
                    da.SelectCommand.Parameters.AddWithValue("@PriorityLevel", priorityLevel);
                    da.SelectCommand.Parameters.AddWithValue("@PositionTypeId", positionTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);


                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region PositionType
        public DataTable GetPositionTypeList(string positiontypeName, string positiontypeCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPositionTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PositionTypeName", positiontypeName);
                    da.SelectCommand.Parameters.AddWithValue("@PositionTypeCode", positiontypeCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPositionTypeDetail(int positiontypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPositionTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PositionTypeId", positiontypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PositionLevel
        public DataTable GetPositionLevelList(string positionlevelName, string positionlevelCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPositionLevels", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PositionLevelName", positionlevelName);
                    da.SelectCommand.Parameters.AddWithValue("@PositionLevelCode", positionlevelCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPositionLevelDetail(int positionlevelId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPositionLevelDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PositionLevelId", positionlevelId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region InterviewType
        public DataTable GetInterviewTypeList(string interviewtypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetInterviewTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InterviewTypeName", interviewtypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetInterviewTypeDetail(int interviewtypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetInterviewTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@InterviewTypeId", interviewtypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Calender
        public DataTable GetCalenderList(string calenderName, int calenderYear, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCalenders", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CalenderName", calenderName);
                    da.SelectCommand.Parameters.AddWithValue("@CalenderYear", calenderYear);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCalenderDetail(int calenderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCalenderDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CalenderId", calenderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region Activity Calender 
        public DataTable GetActivityCalenderList(int calenderId, DateTime fromDate, DateTime toDate, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetActivityCalenders", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CalenderId", calenderId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetActivityCalenderDetail(int activitycalenderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetActivityCalenderDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ActivityCalenderId", activitycalenderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetUpcomingActivityDetail()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUpComingActivity", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region HolidayType
        public DataTable GetHolidayTypeList(string holidaytypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHolidayTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@HolidayTypeName", holidaytypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetHolidayTypeDetail(int holidaytypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHolidayTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@HolidayTypeId", holidaytypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUpComingHolidayDetail()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUpComingHoliday", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region Holiday Calender 
        public DataTable GetHolidayCalenderList(int calenderId, int holidayTypeId, DateTime fromDate, DateTime toDate, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHolidayCalenders", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CalenderId", calenderId);
                    da.SelectCommand.Parameters.AddWithValue("@HolidayTypeId", holidayTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetHolidayCalenderDetail(int holidaycalenderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHolidayCalenderDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@HolidayCalenderId", holidaycalenderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region AssetType
        public DataTable GetAssetTypeList(string assettypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssetTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssetTypeName", assettypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetAssetTypeDetail(int assettypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAssetTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AssetTypeId", assettypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeAssetApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeAssetApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region Travel Type
        public DataTable GetTravelTypeList(string traveltypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTravelTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TravelTypeName", traveltypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetTravelTypeDetail(int traveltypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetTravelTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@TravelTypeId", traveltypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeTravelApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeTravelApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion



        #region Advance Type
        public DataTable GetAdvanceTypeList(string advancetypeName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAdvanceTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AdvanceTypeName", advancetypeName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetAdvanceTypeDetail(int advancetypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetAdvanceTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@AdvanceTypeId", advancetypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeAdvanceApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeAdvanceApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Loan Type
        public DataTable GetLoanTypeList(string loantypeName, string loaninterestcalcOn, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLoanTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LoanTypeName", loantypeName);
                    da.SelectCommand.Parameters.AddWithValue("@InterestCalcOn", loaninterestcalcOn);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLoanTypeDetail(int loantypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLoanTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LoanTypeId", loantypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmployeeLoanApplicationDetails(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEmployeeLoanApplicationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingUserId", reportingUserId);
                    da.SelectCommand.Parameters.AddWithValue("@ReportingRoleId", reportingRoleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion
        #region VerificationAgency
        public DataTable GetVerificationAgencyList(string verificationagencyName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVerificationAgencys", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VerificationAgencyName", verificationagencyName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetVerificationAgencyDetail(int verificationagencyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetVerificationAgencyDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VerificationAgencyId", verificationagencyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region ResumeSource
        public DataTable GetResumeSourceList(string resumesourceName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResumeSources", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ResumeSourceName", resumesourceName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetResumeSourceDetail(int resumesourceId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetResumeSourceDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ResumeSourceId", resumesourceId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region Roaster
        public DataTable GetRoasterList(string roasterName, DateTime fromDate, DateTime toDate, string roasterType, int department, int companyId, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoasters", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoasterName", roasterName);
                    da.SelectCommand.Parameters.AddWithValue("@RoasterType", roasterType);
                    da.SelectCommand.Parameters.AddWithValue("@DepartmentId", department);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetRoasterDetail(int roasterId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoasterDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoasterId", roasterId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetRoasterWeekList(long roasterId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetRoasterWeeks", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RoasterId", roasterId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public ResponseOut AddEditRoaster(HR_Roaster roaster, List<HR_RoasterWeek> weekList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtRoasterWeek = new DataTable();
                dtRoasterWeek.Columns.Add("WeekNo", typeof(int));
                dtRoasterWeek.Columns.Add("Mon_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Tue_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Wed_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Thu_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Fri_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Sat_ShiftId", typeof(int));
                dtRoasterWeek.Columns.Add("Sun_ShiftId", typeof(int));
                if (weekList != null && weekList.Count > 0)
                {
                    foreach (HR_RoasterWeek item in weekList)
                    {
                        DataRow dtrow = dtRoasterWeek.NewRow();
                        dtrow["WeekNo"] = item.WeekNo;
                        dtrow["Mon_ShiftId"] = item.Mon_ShiftId;
                        dtrow["Tue_ShiftId"] = item.Tue_ShiftId;
                        dtrow["Wed_ShiftId"] = item.Wed_ShiftId;
                        dtrow["Thu_ShiftId"] = item.Thu_ShiftId;
                        dtrow["Fri_ShiftId"] = item.Fri_ShiftId;
                        dtrow["Sat_ShiftId"] = item.Sat_ShiftId;
                        dtrow["Sun_ShiftId"] = item.Sun_ShiftId;
                        dtRoasterWeek.Rows.Add(dtrow);
                    }
                    dtRoasterWeek.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditRoster", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@RoasterId", roaster.RoasterId);
                        sqlCommand.Parameters.AddWithValue("@RoasterName", roaster.RoasterName);
                        sqlCommand.Parameters.AddWithValue("@RoasterDesc", roaster.RoasterDesc);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", roaster.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@RoasterStartDate", roaster.RoasterStartDate);
                        sqlCommand.Parameters.AddWithValue("@RoasterType", roaster.RoasterType);
                        sqlCommand.Parameters.AddWithValue("@Remarks", roaster.Remarks);
                        sqlCommand.Parameters.AddWithValue("@DepartmentId", roaster.DepartmentId);
                        sqlCommand.Parameters.AddWithValue("@NoOfWeeks", roaster.NoOfWeeks);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", roaster.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@Roster_Status", roaster.Status);
                        sqlCommand.Parameters.AddWithValue("@RosterWeekDetail", dtRoasterWeek);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetRoasterId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }
                        }
                        else
                        {
                            if (roaster.RoasterId == 0)
                            {
                                responseOut.message = ActionMessage.RoasterCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.RoasterUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetRoasterId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt32(sqlCommand.Parameters["@RetRoasterId"].Value);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }


        public DataTable GetESSEmployeeRoasterList(int employeeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetESSEmployeeRoasterList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId ", employeeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion




        #region ChasisSerial
        public DataTable GetChasisSerialList(int productId, string ChasisSerialNo, string MotorNo, string ControllerNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChasisSerialMappings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@ChasisSerialNo", ChasisSerialNo);
                    da.SelectCommand.Parameters.AddWithValue("@MotorNo", MotorNo);
                    da.SelectCommand.Parameters.AddWithValue("@ControllerNo", ControllerNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetChasisSerialDetail(long mappingId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetChasisSerialMapping", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@MappingId", mappingId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region JobOpening
        public ResponseOut AddEditHRJobOpening(HR_JobOpening jobOpening)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtJobOpening = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditHRJobOpening", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@JobOpeningId", jobOpening.JobOpeningId);

                        sqlCommand.Parameters.AddWithValue("@JobOpeningDate", jobOpening.JobOpeningDate);
                        sqlCommand.Parameters.AddWithValue("@JobTitle", jobOpening.JobTitle);
                        sqlCommand.Parameters.AddWithValue("@JobPortalRefNo", jobOpening.JobPortalRefNo);
                        sqlCommand.Parameters.AddWithValue("@NoOfOpening", jobOpening.NoOfOpening);
                        sqlCommand.Parameters.AddWithValue("@MinExp", jobOpening.MinExp);
                        sqlCommand.Parameters.AddWithValue("@MaxExp", jobOpening.MaxExp);
                        sqlCommand.Parameters.AddWithValue("@MinSalary", jobOpening.MinSalary);
                        sqlCommand.Parameters.AddWithValue("@MaxSalary", jobOpening.MaxSalary);
                        sqlCommand.Parameters.AddWithValue("@KeySkills", jobOpening.KeySkills);
                        sqlCommand.Parameters.AddWithValue("@JobDescription", jobOpening.JobDescription);
                        sqlCommand.Parameters.AddWithValue("@JobStartDate", jobOpening.JobStartDate);
                        sqlCommand.Parameters.AddWithValue("@JobExpiryDate", jobOpening.JobExpiryDate);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", jobOpening.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", jobOpening.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@JobStatus", jobOpening.JobStatus);
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", jobOpening.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@EducationId", jobOpening.EducationId);
                        sqlCommand.Parameters.AddWithValue("@OtherQualification", jobOpening.OtherQualification);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", jobOpening.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@Remarks", jobOpening.Remarks);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetJobOpeningId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }
                        }
                        else
                        {
                            if (jobOpening.JobOpeningId == 0)
                            {
                                responseOut.message = ActionMessage.JobOpeningCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.JobOpeningUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetJobOpeningId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetJobOpeningId"].Value);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetJobOpeningList(string jobOpeningNo, string requisitionNo, string jobPortalRefNo, string jobTitle, DateTime fromDate, DateTime toDate, int companyId, string jobStatus = "Final")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJobOpenings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@JobOpeningNo", jobOpeningNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@JobPortalRefNo", jobPortalRefNo);
                    da.SelectCommand.Parameters.AddWithValue("@JobTitle", jobTitle);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@JobStatus", jobStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region CTC
        public DataTable GetCTCList(string designationId, int companyId, string ctcStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCTCs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DesignationId", designationId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", ctcStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetCTCDetail(int ctcId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCTCDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CTCId", ctcId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region Separation Clear List
        public DataTable GetSeparationClearList(string separationclearlistName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSeparationClearLists", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SeparationClearListName", separationclearlistName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSeparationClearListDetail(int separationclearlistId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSeparationClearListDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SeparationClearListId", separationclearlistId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Separation Category
        public DataTable GetSeparationCategory(string separationcategoryName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSeparationCategorys", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SeparationCategoryName", separationcategoryName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSeparationCategoryDetail(int separationcategoryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("GetSeparationCategoryDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SeparationCategoryId", separationcategoryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetJobOpeningDetail(long jobOpeningId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetJobOpeningDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@JobOpeningId", jobOpeningId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PMS_Section
        public DataTable GetSectionList(string sectionName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSectionTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SectionName", sectionName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSectionDetail(int sectionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSectionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SectionId", sectionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region PMS_PerformanceCycle
        public DataTable GetPerformanceCycleList(string performancecycleName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPerformanceCycleTypes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PerformanceCycleName", performancecycleName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPerformanceCycleDetail(int performanceCycleId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPerformanceCycleDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PerformanceCycleId", performanceCycleId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region PMSGoalCategory
        public DataTable GetGoalCategoryList(string goalCategoryName, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGoalCategory", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GoalCategoryName", goalCategoryName);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetGoalCategoryDetail(int goalCategoryId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGoalCategoryDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@GoalCategoryId", goalCategoryId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion
        #region Shift
        public DataTable GetShiftList(string shiftName, int shiftTypeId, string Status, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetShift", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ShiftName", shiftName);
                    da.SelectCommand.Parameters.AddWithValue("@ShiftTypeId", shiftTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetShiftDetail(int shiftId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetShiftDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ShiftId", shiftId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion.

        #region Email Template
        public DataTable GetEmailTemplateList(string emailTemplateSubject, int emailTemplateTypeId, int companyId, string status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmailTemplates", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmailTemplateSubject", emailTemplateSubject);
                    da.SelectCommand.Parameters.AddWithValue("@EmailTemplateTypeId", emailTemplateTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Status", status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEmailTemplateDetail(int emailTemplateId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmailTemplateDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmailTemplateId", emailTemplateId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion


        #region SIN
        public ResponseOut AddEditSIN(StockIssueNote sin, List<StockIssueNoteProductDetail> sinProductList, List<StockIssueNoteSupportingDocument> sinDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtsinProduct = new DataTable();
                dtsinProduct.Columns.Add("SINProductDetailId", typeof(Int64));
                dtsinProduct.Columns.Add("SINId", typeof(Int64));
                dtsinProduct.Columns.Add("ProductId", typeof(Int64));
                dtsinProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtsinProduct.Columns.Add("Quantity", typeof(decimal));

                if (sinProductList != null && sinProductList.Count > 0)
                {
                    foreach (StockIssueNoteProductDetail item in sinProductList)
                    {
                        DataRow dtrow = dtsinProduct.NewRow();
                        dtrow["SINProductDetailId"] = 0;
                        dtrow["SINId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Quantity"] = item.Quantity;

                        dtsinProduct.Rows.Add(dtrow);
                    }
                    dtsinProduct.AcceptChanges();

                }

                DataTable dtSINDocument = new DataTable();
                dtSINDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtSINDocument.Columns.Add("DocumentName", typeof(string));
                dtSINDocument.Columns.Add("DocumentPath", typeof(string));

                if (sinDocumentList != null && sinDocumentList.Count > 0)
                {
                    foreach (StockIssueNoteSupportingDocument item in sinDocumentList)
                    {
                        DataRow dtrow = dtSINDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtSINDocument.Rows.Add(dtrow);
                    }
                    dtSINDocument.AcceptChanges();
                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSIN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@SINId", sin.SINId);
                        sqlCommand.Parameters.AddWithValue("@SINDate", sin.SINDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", sin.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionNo", sin.RequisitionNo);
                        sqlCommand.Parameters.AddWithValue("@JobId", sin.JobId);
                        sqlCommand.Parameters.AddWithValue("@JobNo", sin.JobNo);
                        sqlCommand.Parameters.AddWithValue("@JobDate", sin.JobDate);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", sin.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@FromLocationId", sin.FromLocationId);
                        sqlCommand.Parameters.AddWithValue("@ToLocationId", sin.ToLocationId);

                        sqlCommand.Parameters.AddWithValue("@RefNo", sin.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", sin.RefDate);


                        sqlCommand.Parameters.AddWithValue("@Remarks1", sin.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", sin.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@ReceivedByUserId", sin.ReceivedByUserId);
                        sqlCommand.Parameters.AddWithValue("@EmployeeName", sin.EmployeeName);

                        sqlCommand.Parameters.AddWithValue("@FinYearId", sin.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", sin.CompanyId);


                        sqlCommand.Parameters.AddWithValue("@CreatedBy", sin.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@SINProductDetail", dtsinProduct);

                        sqlCommand.Parameters.AddWithValue("@SINSupportingDocument", dtSINDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSINId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.AddWithValue("@SINStatus", sin.SINStatus);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", sin.CustomerId); //sin.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId",sin.CustomerBranchId); //sin.CustomerBranchId);
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (sin.SINId == 0)
                            {
                                responseOut.message = ActionMessage.SINCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SINUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSINId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSINId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSINProductList(long sinId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINId", sinId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSINList(string sinNo, string requisitionNo, string jobNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string sINStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINNo", sinNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@JobNo", jobNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SINStatus", sINStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetDeliveryChallanSINList(string sinNo, string requisitionNo, string jobNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string sINStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINsDeliveryChallan", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINNo", sinNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@JobNo", jobNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SINStatus", sINStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSINDetail(long sinId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINId", sinId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSINSupportingDocumentList(long sinId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINId", sinId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSINStoreRequisitionList(string requisitionNo, string workOrderNo, string requisitionType, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINStoreRequisitions", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSINStoreRequisitionProductList(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINRequisitionProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion



        #region Location
        public DataTable GetLocationList(string locationName, string locationCode, int companybranchid, int companyId, string locationStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLocations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LocationName", locationName);
                    da.SelectCommand.Parameters.AddWithValue("@LocationCode", locationCode);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companybranchid);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@LocationStatus", locationStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLocationDetail(int locationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLocationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@LocationId", locationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion






        #region Thought
        public DataTable GetThoughtList(string thoughtName = "", string thoughtType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetThoughtList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ThoughtMessage", thoughtName);
                    da.SelectCommand.Parameters.AddWithValue("@ThoughtType", thoughtType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetThoughtDetail(int thoughtId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetThoughtDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ThoughtID", thoughtId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboardThoughtDetail()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDashboardThoughts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region StickyNotes
        public DataTable GetStickyNotestDetail(int UserId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStickyNotesDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UserID", UserId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetEmailTemplateDetailByEmailType(int emailTemplateTypeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEmailTemplateByTemplateTypeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmailTemplateTypeId", emailTemplateTypeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion 

        #region Calender
        public DataTable GetCalenderDetail()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetHolidayandActivityDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion



        #region Sale Return
        public ResponseOut AddEditSaleReturn(SaleReturn saleReturn, List<SaleReturnProductDetail> saleReturnProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSaleReturnProduct = new DataTable();
                dtSaleReturnProduct.Columns.Add("SaleReturnProductDetailId", typeof(Int64));
                dtSaleReturnProduct.Columns.Add("SaleReturnId", typeof(Int64));
                dtSaleReturnProduct.Columns.Add("ProductId", typeof(Int64));
                dtSaleReturnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtSaleReturnProduct.Columns.Add("Price", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("Quantity", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtSaleReturnProduct.Columns.Add("HSN_Code", typeof(string));
                dtSaleReturnProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (saleReturnProductList != null && saleReturnProductList.Count > 0)
                {
                    foreach (SaleReturnProductDetail item in saleReturnProductList)
                    {
                        DataRow dtrow = dtSaleReturnProduct.NewRow();
                        dtrow["SaleReturnProductDetailId"] = 0;
                        dtrow["SaleReturnId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtSaleReturnProduct.Rows.Add(dtrow);
                    }
                    dtSaleReturnProduct.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSaleReturn", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@SaleReturnId", saleReturn.SaleReturnId);
                        sqlCommand.Parameters.AddWithValue("@SaleReturnDate", saleReturn.SaleReturnDate);
                        sqlCommand.Parameters.AddWithValue("@ReturnType", saleReturn.ReturnType);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", saleReturn.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", saleReturn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", saleReturn.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", saleReturn.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", saleReturn.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", saleReturn.ContactPerson);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", saleReturn.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", saleReturn.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", saleReturn.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", saleReturn.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", saleReturn.PinCode);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", saleReturn.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@Email", saleReturn.Email);
                        sqlCommand.Parameters.AddWithValue("@MobileNo", saleReturn.MobileNo);
                        sqlCommand.Parameters.AddWithValue("@ContactNo", saleReturn.ContactNo);
                        sqlCommand.Parameters.AddWithValue("@Fax", saleReturn.Fax);
                        sqlCommand.Parameters.AddWithValue("@RefNo", saleReturn.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", saleReturn.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", saleReturn.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", saleReturn.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", saleReturn.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", saleReturn.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", saleReturn.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", saleReturn.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", saleReturn.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", saleReturn.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", saleReturn.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", saleReturn.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", saleReturn.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", saleReturn.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", saleReturn.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", saleReturn.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", saleReturn.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", saleReturn.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", saleReturn.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", saleReturn.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", saleReturn.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", saleReturn.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", saleReturn.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", saleReturn.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", saleReturn.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@Remarks", saleReturn.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", saleReturn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", saleReturn.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", saleReturn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", saleReturn.ApprovalStatus);
                        //sqlCommand.Parameters.AddWithValue("@InvoiceStatus", saleReturn.InvoiceStatus);
                        sqlCommand.Parameters.AddWithValue("@SaleReturnProductDetail", dtSaleReturnProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSaleReturnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (saleReturn.SaleReturnId == 0)
                            {
                                responseOut.message = ActionMessage.SaleReturnCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SaleReturnUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSaleReturnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSaleReturnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetSaleReturnProductList(long saleReturnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleReturnProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SaleReturnId", saleReturnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSaleReturnList(string saleReturnNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleReturnLists", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SaleReturnNo", saleReturnNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSaleReturnDetail(long saleReturnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleReturnDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SaleReturnId", saleReturnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region SIN Register

        public DataTable GetSINRegisterList(string sinNo, string requisitionNo, string jobNo, int companyBranchId, int fromLocationId, int toLocationId, DateTime fromDate, DateTime toDate, int companyId, string employee, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSINRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SINNo", sinNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@JobNo", jobNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromLocation", fromLocationId);
                    da.SelectCommand.Parameters.AddWithValue("@ToLocation", toLocationId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@Employee", employee);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion


        #region proc_GetGSTR1

        public DataTable GetGSTR1B2B(DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGSTR1_B2B", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetGSTR1B2CL(DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGSTR1_B2CL", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetGSTR1B2CS(DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGSTR1_B2CS", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetGSTR1CDNR(DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGSTR1_CDNR", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetGSTR1CDNUR(DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetGSTR1_CDNUR", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Purchase Return
        public ResponseOut AddEditPurchaseReturn(PurchaseReturn purchaseReturn, List<PurchaseReturnProductDetail> purchaseReturnProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPurchaseReturnProduct = new DataTable();
                dtPurchaseReturnProduct.Columns.Add("PurchaseReturnProductDetailId", typeof(Int64));
                dtPurchaseReturnProduct.Columns.Add("PurchaseReturnId", typeof(Int64));
                dtPurchaseReturnProduct.Columns.Add("ProductId", typeof(Int64));
                dtPurchaseReturnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPurchaseReturnProduct.Columns.Add("Price", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("Quantity", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtPurchaseReturnProduct.Columns.Add("HSN_Code", typeof(string));
                dtPurchaseReturnProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (purchaseReturnProductList != null && purchaseReturnProductList.Count > 0)
                {
                    foreach (PurchaseReturnProductDetail item in purchaseReturnProductList)
                    {
                        DataRow dtrow = dtPurchaseReturnProduct.NewRow();
                        dtrow["PurchaseReturnProductDetailId"] = 0;
                        dtrow["PurchaseReturnId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtPurchaseReturnProduct.Rows.Add(dtrow);
                    }
                    dtPurchaseReturnProduct.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPurchaseReturn", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturn.PurchaseReturnId);
                        sqlCommand.Parameters.AddWithValue("@PurchaseReturnDate", purchaseReturn.PurchaseReturnDate);
                        sqlCommand.Parameters.AddWithValue("@ReturnType", purchaseReturn.ReturnType);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", purchaseReturn.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", purchaseReturn.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", purchaseReturn.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@VendorId", purchaseReturn.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", purchaseReturn.VendorName);
                        sqlCommand.Parameters.AddWithValue("@BillingAddress", purchaseReturn.BillingAddress);
                        sqlCommand.Parameters.AddWithValue("@City", purchaseReturn.City);
                        sqlCommand.Parameters.AddWithValue("@StateId", purchaseReturn.StateId);
                        sqlCommand.Parameters.AddWithValue("@CountryId", purchaseReturn.CountryId);
                        sqlCommand.Parameters.AddWithValue("@PinCode", purchaseReturn.PinCode);
                        sqlCommand.Parameters.AddWithValue("@GSTNo", purchaseReturn.GSTNo);
                        sqlCommand.Parameters.AddWithValue("@RefNo", purchaseReturn.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", purchaseReturn.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", purchaseReturn.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", purchaseReturn.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", purchaseReturn.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", purchaseReturn.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", purchaseReturn.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", purchaseReturn.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", purchaseReturn.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", purchaseReturn.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", purchaseReturn.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", purchaseReturn.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", purchaseReturn.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", purchaseReturn.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", purchaseReturn.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", purchaseReturn.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", purchaseReturn.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", purchaseReturn.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", purchaseReturn.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", purchaseReturn.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", purchaseReturn.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", purchaseReturn.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", purchaseReturn.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", purchaseReturn.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", purchaseReturn.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@Remarks", purchaseReturn.Remarks);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", purchaseReturn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", purchaseReturn.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", purchaseReturn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", purchaseReturn.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@PurchaseReturnProductDetail", dtPurchaseReturnProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetPurchaseReturnId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (purchaseReturn.PurchaseReturnId == 0)
                            {
                                responseOut.message = ActionMessage.PurchaseReturnCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.PurchaseReturnUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetPurchaseReturnId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetPurchaseReturnId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPurchaseReturnProductList(long purchaseReturnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseReturnProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseReturnList(string purchaseReturnNo, string vendorName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseReturnLists", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PurchaseReturnNo", purchaseReturnNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPurchaseReturnDetail(long purchaseReturnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseReturnDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion
        #region Manufacturer
        public DataTable GetManufacturerList(string manufacturerName, string manufacturerCode, string Status)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetManufacturers", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ManufacturerName", manufacturerName);
                    da.SelectCommand.Parameters.AddWithValue("@ManufacturerCode", manufacturerCode);
                    da.SelectCommand.Parameters.AddWithValue("@Status", Status);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetManufacturereDetail(int manufacturerId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetManufacturerDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ManufacturerId", manufacturerId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Size

        public DataTable GetSizeList(string sizeDesc, string sizeCode, int productSubGroupId, int productMainGroupId, string sizeStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSizes", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SizeCode", sizeCode);
                    da.SelectCommand.Parameters.AddWithValue("@SizeDesc", sizeDesc);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@SizeStatus", sizeStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSizeDetail(long sizeId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSizeDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SizeId", sizeId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion



        #region  SaleReturn Register
        public DataTable GetSaleReturnRegisterList(string saleReturnNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleReturnRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SaleReturnNo", saleReturnNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }



        public DataTable GenerateSaleReturnRegisterReports(string saleReturnNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSaleReturnRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SaleReturnNo", saleReturnNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion

        #region Work Order
        public ResponseOut AddEditWorkOrder(WorkOrder workOrder, List<WorkOrderProductDetail> workOrderProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtWorkOrderProduct = new DataTable();
                dtWorkOrderProduct.Columns.Add("WorkOrderProductDetailId", typeof(Int64));
                dtWorkOrderProduct.Columns.Add("WorkOrderId", typeof(Int64));
                dtWorkOrderProduct.Columns.Add("ProductId", typeof(Int64));
                dtWorkOrderProduct.Columns.Add("Quantity", typeof(decimal));

                if (workOrderProductList != null && workOrderProductList.Count > 0)
                {
                    foreach (WorkOrderProductDetail item in workOrderProductList)
                    {
                        DataRow dtrow = dtWorkOrderProduct.NewRow();
                        dtrow["WorkOrderProductDetailId"] = 0;
                        dtrow["WorkOrderId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["Quantity"] = item.Quantity;

                        dtWorkOrderProduct.Rows.Add(dtrow);
                    }
                    dtWorkOrderProduct.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditWorkOrder", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@WorkOrderId", workOrder.WorkOrderId);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderDate", workOrder.WorkOrderDate);
                        sqlCommand.Parameters.AddWithValue("@TargetFromDate", workOrder.TargetFromDate);
                        sqlCommand.Parameters.AddWithValue("@TargetToDate", workOrder.TargetToDate);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", workOrder.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", workOrder.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", workOrder.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", workOrder.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", workOrder.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderStatus", workOrder.WorkOrderStatus);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderProductDetail", dtWorkOrderProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetWorkOrderId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (workOrder.WorkOrderId == 0)
                            {
                                responseOut.message = ActionMessage.WorkOrderCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.WorkOrderUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetWorkOrderId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetWorkOrderId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetWorkOrderProductList(long workOrderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetWorkOrderProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderId", workOrderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetWorkOrderDetail(long workOrderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetWorkOrderDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderId", workOrderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetWorkOrderList(string workOrderNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetWorkOrders", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region StoreRequisition
        public ResponseOut AddEditStoreRequisition(StoreRequisition storeRequisition, List<StoreRequisitionProductDetail> storeRequisitionProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtStoreRequisitionProduct = new DataTable();
                dtStoreRequisitionProduct.Columns.Add("RequisitionProductDetailId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("RequisitionId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("ProductId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtStoreRequisitionProduct.Columns.Add("Quantity", typeof(decimal));
                dtStoreRequisitionProduct.Columns.Add("IssuedQuantity", typeof(decimal));


                if (storeRequisitionProductList != null && storeRequisitionProductList.Count > 0)
                {
                    foreach (StoreRequisitionProductDetail item in storeRequisitionProductList)
                    {
                        DataRow dtrow = dtStoreRequisitionProduct.NewRow();
                        dtrow["RequisitionProductDetailId"] = 0;
                        dtrow["RequisitionId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["IssuedQuantity"] = item.IssuedQuantity;
                        dtStoreRequisitionProduct.Rows.Add(dtrow);
                    }
                    dtStoreRequisitionProduct.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditStoreRequisition", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", storeRequisition.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionDate", storeRequisition.RequisitionDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionType", storeRequisition.RequisitionType);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", storeRequisition.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@LocationId", storeRequisition.LocationId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionByUserId", storeRequisition.RequisitionByUserId);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", storeRequisition.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", storeRequisition.CustomerBranchId);

                        sqlCommand.Parameters.AddWithValue("@Remarks1", storeRequisition.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", storeRequisition.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", storeRequisition.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", storeRequisition.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", storeRequisition.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@RequisitionStatus", storeRequisition.RequisitionStatus);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderId", storeRequisition.WorkOrderId);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderNo", storeRequisition.WorkOrderNo);
                        sqlCommand.Parameters.AddWithValue("@StoreRequisitionProductDetail", dtStoreRequisitionProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetRequisitionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (storeRequisition.RequisitionId == 0)
                            {
                                responseOut.message = ActionMessage.StoreRequisitionCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.StoreRequisitionUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetRequisitionId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetRequisitionId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        
        public DataTable GetStoreRequisitionDetail(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetLogReportNewStoreRequisitionDetail(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLogReportNewStoreRequisitionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetLogReportOldStoreRequisitionDetail(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLogReportOldStoreRequisitionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetStoreRequisitionProductList(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetLogReportNewStoreRequisitionProductList(long requisitionId, string requisitionNo="", string fromDate="", string toDate="")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLogReportNewStoreRequisitionProductsModify", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    //da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    //da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    //da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLogReportOldStoreRequisitionProductList(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLogReportOldStoreRequisitionProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetLogReportMasterStoreRequisitionProductListDataTable(long requisitionId, string requisitionNo, string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetLogRequisitionReport", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetRequisitionWorkOrderList(string workOrderNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetWorkOrders", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetStoreRequisitionList(string requisitionNo, string workOrderNo, string requisitionType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "",string requisitionStatus="", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitions", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionStatus", requisitionStatus);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetApprovedStoreRequisitionsWithoutPO(string requisitionNo, int customerId, int customerBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovedStoreRequisitionsWithoutPO", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetStoreRequisitionListForMRN(string requisitionNo, string requisitionType, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionsForMRN", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPendingStoreRequisitionsReport(string requisitionNo, string requisitionType, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPendingStoreRequisitionsReport", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                    }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPendingStoreRequisitionList(string requisitionNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPendingStoreRequisitionReport", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetWorkOrderBOMProductList(long workOrderId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetWorkOrderBOMProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderId", workOrderId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetStoreRequisitionEscalationList(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetListPendingRequisitionEscalationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetDashboardPendingStoreRequisition(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardPendingStoreRequisition", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetSupervisorDashboardPendingStoreRequisition(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_SupervisorDashboardPendingStoreRequisition", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboardRejectedStoreRequisition(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardRejectedStoreRequisition", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetSupervisorDashboardRejectedStoreRequisition(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_SupervisorDashboardRejectedStoreRequisition", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetDashboardApprovedStoreRequisitionListWithoutPO(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardApprovedStoreRequisitionListWithoutPO", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetApprovedPOsMRNNotDone(DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, int customerId, int customerBranchId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovedPOsMRNNotDone", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetApprovedPOsCustomerSiteMRNNotDone(DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, int customerId, int customerBranchId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovedPOsCustomerSiteMRNNotDone", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetDashboardRecommendationPOList(int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_dashboardRecommendationPOList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        #endregion

        #region StoreRequisition Approvel
        public DataTable GetStoreRequisitionApprovelList(string requisitionNo, string workOrderNo, string requisitionType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionApprovalList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion

        #region StoreRequisition Approvel Update

        public ResponseOut AddEditStoreRequisitionUpdate(StoreRequisition storeRequisition, List<StoreRequisitionProductDetail> storeRequisitionProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtStoreRequisitionProduct = new DataTable();
                dtStoreRequisitionProduct.Columns.Add("RequisitionProductDetailId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("RequisitionId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("ProductId", typeof(Int64));
                dtStoreRequisitionProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtStoreRequisitionProduct.Columns.Add("Quantity", typeof(decimal));
                dtStoreRequisitionProduct.Columns.Add("IssuedQuantity", typeof(decimal));


                if (storeRequisitionProductList != null && storeRequisitionProductList.Count > 0)
                {
                    foreach (StoreRequisitionProductDetail item in storeRequisitionProductList)
                    {
                        DataRow dtrow = dtStoreRequisitionProduct.NewRow();
                        dtrow["RequisitionProductDetailId"] = 0;
                        dtrow["RequisitionId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["IssuedQuantity"] = item.IssuedQuantity;
                        dtStoreRequisitionProduct.Rows.Add(dtrow);
                    }
                    dtStoreRequisitionProduct.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_ApproveUpdateStoreRequisition", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", storeRequisition.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionDate", storeRequisition.RequisitionDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionType", storeRequisition.RequisitionType);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", storeRequisition.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@LocationId", storeRequisition.LocationId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionByUserId", storeRequisition.RequisitionByUserId);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", storeRequisition.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", storeRequisition.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", storeRequisition.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", storeRequisition.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", storeRequisition.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", storeRequisition.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", storeRequisition.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@RequisitionStatus", storeRequisition.RequisitionStatus);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderId", storeRequisition.WorkOrderId);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderNo", storeRequisition.WorkOrderNo);
                        sqlCommand.Parameters.AddWithValue("@StoreRequisitionProductDetail", dtStoreRequisitionProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetRequisitionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (storeRequisition.RequisitionId == 0)
                            {
                                responseOut.message = ActionMessage.ApprovelStoreRequisitionUpdatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.ApprovelStoreRequisitionUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetRequisitionId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetRequisitionId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetStoreRequisitionApprovelUpdateList(string requisitionNo, string workOrderNo, string requisitionType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionApprovalUpdateList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetStoreRequisitionProductUpdateList(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionUpdateProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Purchase Quotation
        public ResponseOut AddEditPurchaseQuotation(PurchaseQuotation quotation, List<PurchaseQuotationProductDetail> quotationProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtQuotationProduct = new DataTable();
                dtQuotationProduct.Columns.Add("QuotationProductDetailId", typeof(Int64));
                dtQuotationProduct.Columns.Add("QuotationId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductId", typeof(Int64));
                dtQuotationProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtQuotationProduct.Columns.Add("Price", typeof(decimal));
                dtQuotationProduct.Columns.Add("Quantity", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtQuotationProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtQuotationProduct.Columns.Add("TotalPrice", typeof(decimal));
                dtQuotationProduct.Columns.Add("HSN_Code", typeof(string));
                if (quotationProductList != null && quotationProductList.Count > 0)
                {
                    foreach (PurchaseQuotationProductDetail item in quotationProductList)
                    {
                        DataRow dtrow = dtQuotationProduct.NewRow();
                        dtrow["QuotationProductDetailId"] = 0;
                        dtrow["QuotationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtQuotationProduct.Rows.Add(dtrow);
                    }
                    dtQuotationProduct.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPurchaseQuotation", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@QuotationId", quotation.QuotationId);
                        sqlCommand.Parameters.AddWithValue("@QuotationDate", quotation.QuotationDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", quotation.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionNo", quotation.RequisitionNo);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", quotation.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@CurrencyCode", quotation.CurrencyCode);
                        sqlCommand.Parameters.AddWithValue("@VendorId", quotation.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", quotation.VendorName);
                        sqlCommand.Parameters.AddWithValue("@DeliveryDays", quotation.DeliveryDays);
                        sqlCommand.Parameters.AddWithValue("@DeliveryAt", quotation.DeliveryAt);
                        sqlCommand.Parameters.AddWithValue("@RefNo", quotation.RefNo);
                        sqlCommand.Parameters.AddWithValue("@RefDate", quotation.RefDate);
                        sqlCommand.Parameters.AddWithValue("@BasicValue", quotation.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", quotation.TotalValue);
                        sqlCommand.Parameters.AddWithValue("@FreightValue", quotation.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", quotation.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", quotation.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", quotation.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", quotation.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", quotation.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", quotation.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", quotation.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", quotation.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", quotation.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", quotation.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", quotation.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", quotation.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", quotation.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", quotation.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", quotation.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", quotation.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", quotation.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", quotation.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", quotation.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", quotation.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", quotation.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", quotation.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", quotation.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", quotation.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", quotation.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", quotation.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@PurchaseQuotationProductDetail", dtQuotationProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetQuotationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }
                        }
                        else
                        {
                            if (quotation.QuotationId == 0)
                            {
                                responseOut.message = ActionMessage.PurchaseQuotationCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.PurchaseQuotationUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetQuotationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetQuotationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetPurchaseQuotationProductList(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseQuotationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseQuotationList(string quotationNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseQuotations", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationNo", quotationNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseQuotationDetail(long quotationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseQuotationDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@QuotationId", quotationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPurchaseQuotationComparisonList(long indentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseQuotationComparison", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentId", indentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        
        public DataTable GetPurchaseQuotationIndentList(string indentNo, string indentType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndentForQuotation", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentNo", indentNo);
                    da.SelectCommand.Parameters.AddWithValue("@IndentType", indentType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseQuotationIndentProductList(long indentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseQuotationIndentProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentId", indentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PurchaseIndent
        public ResponseOut AddEditPurchaseIndent(PurchaseIndent purchaseIndent, List<PurchaseIndentProductDetail> purchaseIndentProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtPurchaseIndentProduct = new DataTable();
                dtPurchaseIndentProduct.Columns.Add("IndentProductDetailId", typeof(Int64));
                dtPurchaseIndentProduct.Columns.Add("IndentId", typeof(Int64));
                dtPurchaseIndentProduct.Columns.Add("ProductId", typeof(Int64));
                dtPurchaseIndentProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtPurchaseIndentProduct.Columns.Add("Quantity", typeof(decimal));


                if (purchaseIndentProductList != null && purchaseIndentProductList.Count > 0)
                {
                    foreach (PurchaseIndentProductDetail item in purchaseIndentProductList)
                    {
                        DataRow dtrow = dtPurchaseIndentProduct.NewRow();
                        dtrow["IndentProductDetailId"] = 0;
                        dtrow["IndentId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Quantity"] = item.Quantity;
                        dtPurchaseIndentProduct.Rows.Add(dtrow);
                    }
                    dtPurchaseIndentProduct.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditPurchaseIndent", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@IndentId", purchaseIndent.IndentId);
                        sqlCommand.Parameters.AddWithValue("@IndentDate", purchaseIndent.IndentDate);
                        sqlCommand.Parameters.AddWithValue("@IndentType", purchaseIndent.IndentType);
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", purchaseIndent.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@RequisitionNo", purchaseIndent.RequisitionNo);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", purchaseIndent.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@IndentByUserId", purchaseIndent.IndentByUserId);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", purchaseIndent.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", purchaseIndent.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", purchaseIndent.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", purchaseIndent.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", purchaseIndent.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", purchaseIndent.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", purchaseIndent.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@IndentStatus", purchaseIndent.IndentStatus);
                        sqlCommand.Parameters.AddWithValue("@PurchaseIndentProductDetail", dtPurchaseIndentProduct);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetIndentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetIndentMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (sqlCommand.Parameters["@RetIndentMessage"].Value != null)
                        {
                            responseOut.indentMessage = Convert.ToString(sqlCommand.Parameters["@RetIndentMessage"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (purchaseIndent.IndentId == 0)
                            {
                                responseOut.message = ActionMessage.PurchaseIndentCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.PurchaseIndentUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetIndentId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetIndentId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetPurchaseIndentProductList(long indentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndentProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentId", indentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetPurchaseIndentDetail(long indentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndentDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentId", indentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetPurchaseIndentList(string indentNo, string indentType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndent", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentNo", indentNo);
                    da.SelectCommand.Parameters.AddWithValue("@IndentType", indentType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetStoreRequisitionProductListForPurchaseIndent(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionProductListForPurchaseIndent", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetStoreRequisitionListForPurchaseIndent(string requisitionNo, string workOrderNo, string requisitionType, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionsForPurchaseIndent", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        public DataTable GetPurchaseIndentEscalationList(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetListPendingPurchaseIndentEscalationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseIndentForQuotationEscalationList(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetListPendingPurchaseIndentForQuotationEscalationDetails", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region PurchaseIndent Approvel
        public DataTable GetPurchaseIndentApprovelList(string indentNo, string indentType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndentApprovalList", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentNo", indentNo);
                    da.SelectCommand.Parameters.AddWithValue("@IndentType", indentType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion


        #region Fabrication
        public ResponseOut AddEditFabrication(Fabrication fabrication, List<FabricationProductDetail> FabricationProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtFabricationProduct = new DataTable();
                dtFabricationProduct.Columns.Add("FabricationDetailId", typeof(Int64));
                dtFabricationProduct.Columns.Add("FabricationId", typeof(Int64));
                dtFabricationProduct.Columns.Add("ProductId", typeof(Int64));
                dtFabricationProduct.Columns.Add("Quantity", typeof(decimal));

                if (FabricationProductList != null && FabricationProductList.Count > 0)
                {
                    foreach (FabricationProductDetail item in FabricationProductList)
                    {
                        DataRow dtrow = dtFabricationProduct.NewRow();
                        dtrow["FabricationDetailId"] = 0;
                        dtrow["FabricationId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["Quantity"] = item.Quantity;

                        dtFabricationProduct.Rows.Add(dtrow);
                    }
                    dtFabricationProduct.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditFabrication", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@FabricationId", fabrication.FabricationId);
                        sqlCommand.Parameters.AddWithValue("@FabricationDate", fabrication.FabricationDate);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderId", fabrication.WorkOrderId);
                        sqlCommand.Parameters.AddWithValue("@WorkOrderNo", fabrication.WorkOrderNo);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", fabrication.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", fabrication.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", fabrication.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", fabrication.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", fabrication.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@FabricationStatus", fabrication.FabricationStatus);
                        sqlCommand.Parameters.AddWithValue("@FabricationProductDetail", dtFabricationProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetFabricationId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (fabrication.FabricationId == 0)
                            {
                                responseOut.message = ActionMessage.FabricationCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.FabricationUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetFabricationId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetFabricationId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetFabricationProductList(long fabricationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFabricationProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FabricationId", fabricationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetFabricationDetail(long fabricationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFabricationoDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FabricationId", fabricationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetFabricationList(string fabricationNo, string workOrderNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string fabricationStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetFabrications", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FabricationNo", fabricationNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", fabricationStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region PO Approvel
        public DataTable GetApprovelPOList(string poNo, string vendorName, string refNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovelPOs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@PONo", poNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@RefNo", refNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetApprovelPODetail(long poId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovelPODetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@POId", poId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetApprovedPOListByMD(DateTime fromDate, DateTime toDate, string approvalStatus, int companyId, int customerId, int customerBranchId, string displayType = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetApprovedPOsByMD", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region PO PurchaseIndent
        public DataTable GetPurchaseOrderIndentList(string indentNo, string indentType, string customerName, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseIndentForPO", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentNo", indentNo);
                    da.SelectCommand.Parameters.AddWithValue("@IndentType", indentType);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetPurchaseOrderIndentProductList(long indentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetPurchaseOrderIndentProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IndentId", indentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  Project 
        public DataTable GetProjectList(string projectName, int projectCode, int customerId, int customerBranchId, string projectStatus)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    da = new SqlDataAdapter("proc_GetProjects", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProjectName", projectName);
                    da.SelectCommand.Parameters.AddWithValue("@ProjectCode", projectCode);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@ProjectStatus", projectStatus);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetProjectDetail(int projectId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetProjectDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProjectId", projectId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region SiteConsumption
        public DataTable GetSiteConsumptionProductList(int siteConsumptionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteConsumptionProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteConsumptionId", siteConsumptionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public ResponseOut AddEditSiteConsumption(SiteConsumption siteConsumption, List<SiteConsumptionDetail> SiteConsumptionProductList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtSiteConsumptionProduct = new DataTable();
                dtSiteConsumptionProduct.Columns.Add("SiteConsumptionDetailId", typeof(Int64));
                dtSiteConsumptionProduct.Columns.Add("SiteConsumptionId", typeof(Int64));
                dtSiteConsumptionProduct.Columns.Add("ProductId", typeof(Int64));
                dtSiteConsumptionProduct.Columns.Add("Quantity", typeof(decimal));

                if (SiteConsumptionProductList != null && SiteConsumptionProductList.Count > 0)
                {
                    foreach (SiteConsumptionDetail item in SiteConsumptionProductList)
                    {
                        DataRow dtrow = dtSiteConsumptionProduct.NewRow();
                        dtrow["SiteConsumptionDetailId"] = 0;
                        dtrow["SiteConsumptionId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["Quantity"] = item.Quantity;

                        dtSiteConsumptionProduct.Rows.Add(dtrow);
                    }
                    dtSiteConsumptionProduct.AcceptChanges();

                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSiteConsumption", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@SiteConsumptionId", siteConsumption.SiteConsumptionId);
                        sqlCommand.Parameters.AddWithValue("@SiteConsumptionDate", siteConsumption.SiteConsumptionDate);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", siteConsumption.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@ConsumedByUser", siteConsumption.ConsumedByUser);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", siteConsumption.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", siteConsumption.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", siteConsumption.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", siteConsumption.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", siteConsumption.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", siteConsumption.CompanyId);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", siteConsumption.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ConsumptionStatus", siteConsumption.ConsumptionStatus);
                        sqlCommand.Parameters.AddWithValue("@ModifiedBy", siteConsumption.ModifiedBy);

                        sqlCommand.Parameters.AddWithValue("@SiteConsumptionProductDetail", dtSiteConsumptionProduct);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSiteConsumptionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (siteConsumption.SiteConsumptionId == 0)
                            {
                                responseOut.message = ActionMessage.SiteConsumptionCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.SiteConsumptionUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSiteConsumptionId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSiteConsumptionId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }

        public DataTable GetSiteConsumptionList(string siteConsumptionNo, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string consumptionStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteConsumption", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteConsumptionNo", siteConsumptionNo);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", consumptionStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSiteConsumptionDetail(long siteConsumptionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteConsumptionDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteConsumptionId", siteConsumptionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductSiteConsumptionListReport(int customerId, int customerBranchId, int productId, int companyBranchId,string fromDate,string toDate,int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteConsumptionListReport", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    //da.SelectCommand.Parameters.AddWithValue("@SiteConsumptionNo", assemblyId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId",customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyId);
                
                    //da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", companyid);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public decimal GetSiteProductAvailableStock(long productId, int finYearId, int customerId, int customerBranchId)
        {
            decimal availableStock = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_GetSiteProductAvailableStock", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@FinYearId", finYearId);
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        cmd.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                        object qty = cmd.ExecuteScalar();
                        if (qty != null)
                        {
                            availableStock = Convert.ToDecimal(qty);
                        }
                        else
                        {
                            availableStock = 0;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return availableStock;
        }
        #endregion

        #region CustomerSiteMRN
        public ResponseOut AddEditCustomerSiteMRN(CustomerSiteMRN customerSiteMrn, List<CustomerSiteMRNProductDetail> customerSiteMrnProductList, List<CustomerSiteMRNSupportingDocument> customerSiteMrnDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtcustomerSiteMrnProduct = new DataTable();
                dtcustomerSiteMrnProduct.Columns.Add("CustomerSiteMRNProductDetailId", typeof(Int64));
                dtcustomerSiteMrnProduct.Columns.Add("CustomerSiteMRNId", typeof(Int64));
                dtcustomerSiteMrnProduct.Columns.Add("ProductId", typeof(Int64));
                dtcustomerSiteMrnProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtcustomerSiteMrnProduct.Columns.Add("Price", typeof(decimal));
                dtcustomerSiteMrnProduct.Columns.Add("Quantity", typeof(decimal));
                dtcustomerSiteMrnProduct.Columns.Add("ReceivedQuantity", typeof(decimal));
                dtcustomerSiteMrnProduct.Columns.Add("AcceptQuantity", typeof(decimal));
                dtcustomerSiteMrnProduct.Columns.Add("RejectQuantity", typeof(decimal));
                dtcustomerSiteMrnProduct.Columns.Add("RequisitionId", typeof(Int64));

                if (customerSiteMrnProductList != null && customerSiteMrnProductList.Count > 0)
                {
                    foreach (CustomerSiteMRNProductDetail item in customerSiteMrnProductList)
                    {
                        DataRow dtrow = dtcustomerSiteMrnProduct.NewRow();
                        dtrow["CustomerSiteMRNProductDetailId"] = 0;
                        dtrow["CustomerSiteMRNId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["ReceivedQuantity"] = item.ReceivedQuantity;
                        dtrow["AcceptQuantity"] = item.AcceptQuantity;
                        dtrow["RejectQuantity"] = item.RejectQuantity;
                        dtrow["RequisitionId"] = item.RequisitionId;
                        dtcustomerSiteMrnProduct.Rows.Add(dtrow);
                    }
                    dtcustomerSiteMrnProduct.AcceptChanges();

                }


                DataTable dtCustomerSiteMRNDocument = new DataTable();
                dtCustomerSiteMRNDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtCustomerSiteMRNDocument.Columns.Add("DocumentName", typeof(string));
                dtCustomerSiteMRNDocument.Columns.Add("DocumentPath", typeof(string));

                if (customerSiteMrnDocumentList != null && customerSiteMrnDocumentList.Count > 0)
                {
                    foreach (CustomerSiteMRNSupportingDocument item in customerSiteMrnDocumentList)
                    {
                        DataRow dtrow = dtCustomerSiteMRNDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtCustomerSiteMRNDocument.Rows.Add(dtrow);
                    }
                    dtCustomerSiteMRNDocument.AcceptChanges();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditCustomerSiteMRN", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CustomerSiteMRNId", customerSiteMrn.CustomerSiteMRNId);
                        sqlCommand.Parameters.AddWithValue("@CustomerSiteMRNDate", customerSiteMrn.CustomerSiteMRNDate);
                        sqlCommand.Parameters.AddWithValue("@GRNo", customerSiteMrn.GRNo);
                        sqlCommand.Parameters.AddWithValue("@GRDate", customerSiteMrn.GRDate);
                        sqlCommand.Parameters.AddWithValue("@RequisitionId", customerSiteMrn.RequisitionId);
                        sqlCommand.Parameters.AddWithValue("@DeliveryChallanNo", customerSiteMrn.DeliveryChallanNo);
                        sqlCommand.Parameters.AddWithValue("@PONo", customerSiteMrn.PONo);
                        sqlCommand.Parameters.AddWithValue("@POId", customerSiteMrn.POId);
                        sqlCommand.Parameters.AddWithValue("@VendorId", customerSiteMrn.VendorId);
                        sqlCommand.Parameters.AddWithValue("@VendorName", customerSiteMrn.VendorName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", customerSiteMrn.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", customerSiteMrn.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", customerSiteMrn.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", customerSiteMrn.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", customerSiteMrn.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", customerSiteMrn.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", customerSiteMrn.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", customerSiteMrn.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", customerSiteMrn.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", customerSiteMrn.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", customerSiteMrn.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", customerSiteMrn.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", customerSiteMrn.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", customerSiteMrn.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", customerSiteMrn.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", customerSiteMrn.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", customerSiteMrn.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", customerSiteMrn.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", customerSiteMrn.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", customerSiteMrn.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", customerSiteMrn.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", customerSiteMrn.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", customerSiteMrn.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", customerSiteMrn.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@CustomerSiteMRNProductDetail", dtcustomerSiteMrnProduct);

                        sqlCommand.Parameters.AddWithValue("@CustomerSiteMRNSupportingDocument", dtCustomerSiteMRNDocument);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetCustomerSiteMRNId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", customerSiteMrn.ApprovalStatus);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", customerSiteMrn.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", customerSiteMrn.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@IsLocal", customerSiteMrn.IsLocal);
                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (customerSiteMrn.CustomerSiteMRNId == 0)
                            {
                                responseOut.message = ActionMessage.CustomerSiteMRNCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.CustomerSiteMRNUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetCustomerSiteMRNId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetCustomerSiteMRNId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetCustomerSiteMRNProductList(long customerSiteMrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerSiteMRNId", customerSiteMrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCustomerSiteMRNDetail(long customerSiteMrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerSiteMRNId", customerSiteMrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        public DataTable GetCustomerSiteMRNList(string customerSiteMrnNo, string vendorName, string dispatchrefNo, DateTime fromDate, DateTime toDate, int companyId, string approvalStatus = "",string isLocal="")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNs", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CustomerSiteMRNNo", customerSiteMrnNo);
                    da.SelectCommand.Parameters.AddWithValue("@VendorName", vendorName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@IsLocal", isLocal);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCustomerSiteMRNSupportingDocumentList(long customerSiteMrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@customerSiteMrnId", customerSiteMrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetStoreRequisitionProductListForCustSiteMRN(long requisitionId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetStoreRequisitionProductListForCustSiteMRN", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionId", requisitionId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetCustomerSiteMRNStoreRequisitionList(string requisitionNo, string workOrderNo, string requisitionType, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNStoreRequisitions", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionNo", requisitionNo);
                    da.SelectCommand.Parameters.AddWithValue("@WorkOrderNo", workOrderNo);
                    da.SelectCommand.Parameters.AddWithValue("@RequisitionType", requisitionType);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyBranchId", companyBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@DisplayType", displayType);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region  CustomerSiteMRN Register
        public DataTable GetCustomerSiteMRNRegisterList(int vendorId, int shippingstateId, DateTime fromDate, DateTime toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetCustomerSiteMRNRegister", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@VendorId", vendorId);
                    da.SelectCommand.Parameters.AddWithValue("@ShippingStateId", shippingstateId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortBy", sortBy);
                    da.SelectCommand.Parameters.AddWithValue("@SortOrder", sortOrder);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }


        #endregion

        #region Site Product Opening Stock
        public DataTable GetSiteProductOpeningList(string productName, int companyid, int finYearId, int companyBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteProductOpenings", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductName", productName);
                    da.SelectCommand.Parameters.AddWithValue("@Companyid", companyid);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.SelectCommand.Parameters.AddWithValue("@companyBranchId", companyBranchId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetSiteProductOpeningDetail(long openingTrnId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteProductOpeningDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteOpeningTrnId", openingTrnId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Site Ledger
        public DataTable GetSiteLedgerDetail(int productTypeId, string assemblyType, int productMainGroupId, int productSubGroupId, long productId, DateTime fromDate, DateTime toDate, int companyId, int customerId, int customerBranchId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_SitePrintStockLedger", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductTypeId", productTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@AssemblyType", assemblyType);
                    da.SelectCommand.Parameters.AddWithValue("@ProductMainGroupId", productMainGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductSubGroupId", productSubGroupId);
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchId);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion


        #region Site Delivery Challan

        public ResponseOut AddEditSiteDeliveryChallan(SiteDeliveryChallan siteDeliverychallan, List<SiteChallanProductDetail> siteChallanProductList, List<SiteChallanTermsDetail> siteChallanTermList, List<SiteDeliveryChallanSupportingDocument> siteDeliveryChallanDocumentList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                DataTable dtChallanProduct = new DataTable();
                dtChallanProduct.Columns.Add("SiteChallanProductDetailId", typeof(Int64));
                dtChallanProduct.Columns.Add("SiteChallanId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductId", typeof(Int64));
                dtChallanProduct.Columns.Add("ProductShortDesc", typeof(string));
                dtChallanProduct.Columns.Add("Price", typeof(decimal));
                dtChallanProduct.Columns.Add("Quantity", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("DiscountAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxId", typeof(Int64));
                dtChallanProduct.Columns.Add("TaxName", typeof(string));
                dtChallanProduct.Columns.Add("TaxPercentage", typeof(decimal));
                dtChallanProduct.Columns.Add("TaxAmount", typeof(decimal));
                dtChallanProduct.Columns.Add("CGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("CGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("SGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("SGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("IGST_Perc", typeof(decimal));
                dtChallanProduct.Columns.Add("IGST_Amount", typeof(decimal));
                dtChallanProduct.Columns.Add("HSN_Code", typeof(string));
                dtChallanProduct.Columns.Add("TotalPrice", typeof(decimal));

                if (siteChallanProductList != null && siteChallanProductList.Count > 0)
                {
                    foreach (SiteChallanProductDetail item in siteChallanProductList)
                    {
                        DataRow dtrow = dtChallanProduct.NewRow();
                        dtrow["SiteChallanProductDetailId"] = 0;
                        dtrow["SiteChallanId"] = 0;
                        dtrow["ProductId"] = item.ProductId;
                        dtrow["ProductShortDesc"] = item.ProductShortDesc;
                        dtrow["Price"] = item.Price;
                        dtrow["Quantity"] = item.Quantity;
                        dtrow["DiscountPercentage"] = item.DiscountPercentage;
                        dtrow["DiscountAmount"] = item.DiscountAmount;
                        dtrow["TaxId"] = item.TaxId;
                        dtrow["TaxName"] = item.TaxName;
                        dtrow["TaxPercentage"] = item.TaxPercentage;
                        dtrow["TaxAmount"] = item.TaxAmount;
                        dtrow["CGST_Perc"] = item.CGST_Perc;
                        dtrow["CGST_Amount"] = item.CGST_Amount;
                        dtrow["SGST_Perc"] = item.SGST_Perc;
                        dtrow["SGST_Amount"] = item.SGST_Amount;
                        dtrow["IGST_Perc"] = item.IGST_Perc;
                        dtrow["IGST_Amount"] = item.IGST_Amount;
                        dtrow["HSN_Code"] = item.HSN_Code;
                        dtrow["TotalPrice"] = item.TotalPrice;
                        dtChallanProduct.Rows.Add(dtrow);
                    }
                    dtChallanProduct.AcceptChanges();

                }

                //DataTable dtChallanTax = new DataTable();
                //dtChallanTax.Columns.Add("TaxId", typeof(Int64));
                //dtChallanTax.Columns.Add("TaxName", typeof(string));
                //dtChallanTax.Columns.Add("TaxPercentage", typeof(decimal));
                //dtChallanTax.Columns.Add("TaxAmount", typeof(decimal));

                //if (challanTaxList != null && challanTaxList.Count > 0)
                //{
                //    foreach (ChallanTaxDetail item in challanTaxList)
                //    {
                //        DataRow dtrow = dtChallanTax.NewRow();
                //        dtrow["TaxId"] = item.TaxId;
                //        dtrow["TaxName"] = item.TaxName;
                //        dtrow["TaxPercentage"] = item.TaxPercentage;
                //        dtrow["TaxAmount"] = item.TaxAmount;

                //        dtChallanTax.Rows.Add(dtrow);
                //    }
                //    dtChallanTax.AcceptChanges();
                //}

                DataTable dtChallanTerm = new DataTable();
                dtChallanTerm.Columns.Add("TermDesc", typeof(string));
                dtChallanTerm.Columns.Add("TermSequence", typeof(int));

                if (siteChallanTermList != null && siteChallanTermList.Count > 0)
                {
                    foreach (SiteChallanTermsDetail item in siteChallanTermList)
                    {
                        DataRow dtrow = dtChallanTerm.NewRow();
                        dtrow["TermDesc"] = item.TermDesc;
                        dtrow["TermSequence"] = item.TermSequence;
                        dtChallanTerm.Rows.Add(dtrow);
                    }
                    dtChallanTerm.AcceptChanges();
                }


                DataTable dtDeliveryChallanDocument = new DataTable();
                dtDeliveryChallanDocument.Columns.Add("DocumentTypeId", typeof(Int32));
                dtDeliveryChallanDocument.Columns.Add("DocumentName", typeof(string));
                dtDeliveryChallanDocument.Columns.Add("DocumentPath", typeof(string));

                if (siteDeliveryChallanDocumentList != null && siteDeliveryChallanDocumentList.Count > 0)
                {
                    foreach (SiteDeliveryChallanSupportingDocument item in siteDeliveryChallanDocumentList)
                    {
                        DataRow dtrow = dtDeliveryChallanDocument.NewRow();
                        dtrow["DocumentTypeId"] = item.DocumentTypeId;
                        dtrow["DocumentName"] = item.DocumentName;
                        dtrow["DocumentPath"] = item.DocumentPath;
                        dtDeliveryChallanDocument.Rows.Add(dtrow);
                    }
                    dtDeliveryChallanDocument.AcceptChanges();
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditSiteDeliveryChallan", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@SiteChallanId", siteDeliverychallan.SiteChallanId);
                        sqlCommand.Parameters.AddWithValue("@SiteChallanDate", siteDeliverychallan.SiteChallanDate);
                        sqlCommand.Parameters.AddWithValue("@InvoiceId", siteDeliverychallan.InvoiceId);
                        sqlCommand.Parameters.AddWithValue("@InvoiceNo", siteDeliverychallan.InvoiceNo);
                        sqlCommand.Parameters.AddWithValue("@SinId", siteDeliverychallan.SinId);
                        sqlCommand.Parameters.AddWithValue("@SinNo", siteDeliverychallan.SinNo);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", siteDeliverychallan.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerName", siteDeliverychallan.CustomerName);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeId", siteDeliverychallan.ConsigneeId);
                        sqlCommand.Parameters.AddWithValue("@ConsigneeName", siteDeliverychallan.ConsigeeName);
                        sqlCommand.Parameters.AddWithValue("@ContactPerson", siteDeliverychallan.ContactPerson);

                        sqlCommand.Parameters.AddWithValue("@ShippingContactPerson", siteDeliverychallan.ShippingContactPerson);
                        sqlCommand.Parameters.AddWithValue("@ShippingBillingAddress", siteDeliverychallan.ShippingBillingAddress);
                        sqlCommand.Parameters.AddWithValue("@ShippingCity", siteDeliverychallan.ShippingCity);
                        sqlCommand.Parameters.AddWithValue("@ShippingStateId", siteDeliverychallan.ShippingStateId);
                        sqlCommand.Parameters.AddWithValue("@ShippingCountryId", siteDeliverychallan.ShippingCountryId);
                        sqlCommand.Parameters.AddWithValue("@ShippingPinCode", siteDeliverychallan.ShippingPinCode);
                        sqlCommand.Parameters.AddWithValue("@ShippingTINNo", siteDeliverychallan.ShippingTINNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingEmail", siteDeliverychallan.ShippingEmail);
                        sqlCommand.Parameters.AddWithValue("@ShippingMobileNo", siteDeliverychallan.ShippingMobileNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingContactNo", siteDeliverychallan.ShippingContactNo);
                        sqlCommand.Parameters.AddWithValue("@ShippingFax", siteDeliverychallan.ShippingFax);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", siteDeliverychallan.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefNo", siteDeliverychallan.DispatchRefNo);
                        sqlCommand.Parameters.AddWithValue("@DispatchRefDate", siteDeliverychallan.DispatchRefDate);

                        sqlCommand.Parameters.AddWithValue("@LRNo", siteDeliverychallan.LRNo);
                        sqlCommand.Parameters.AddWithValue("@LRDate", siteDeliverychallan.LRDate);
                        sqlCommand.Parameters.AddWithValue("@TransportVia", siteDeliverychallan.TransportVia);
                        sqlCommand.Parameters.AddWithValue("@NoOfPackets", siteDeliverychallan.NoOfPackets);
                        sqlCommand.Parameters.AddWithValue("@ApprovalStatus", siteDeliverychallan.ApprovalStatus);

                        sqlCommand.Parameters.AddWithValue("@BasicValue", siteDeliverychallan.BasicValue);
                        sqlCommand.Parameters.AddWithValue("@TotalValue", siteDeliverychallan.TotalValue);

                        sqlCommand.Parameters.AddWithValue("@FreightValue", siteDeliverychallan.FreightValue);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Perc", siteDeliverychallan.FreightCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightCGST_Amt", siteDeliverychallan.FreightCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Perc", siteDeliverychallan.FreightSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightSGST_Amt", siteDeliverychallan.FreightSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Perc", siteDeliverychallan.FreightIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@FreightIGST_Amt", siteDeliverychallan.FreightIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingValue", siteDeliverychallan.LoadingValue);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Perc", siteDeliverychallan.LoadingCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingCGST_Amt", siteDeliverychallan.LoadingCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Perc", siteDeliverychallan.LoadingSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingSGST_Amt", siteDeliverychallan.LoadingSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Perc", siteDeliverychallan.LoadingIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@LoadingIGST_Amt", siteDeliverychallan.LoadingIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceValue", siteDeliverychallan.InsuranceValue);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Perc", siteDeliverychallan.InsuranceCGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceCGST_Amt", siteDeliverychallan.InsuranceCGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Perc", siteDeliverychallan.InsuranceSGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceSGST_Amt", siteDeliverychallan.InsuranceSGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Perc", siteDeliverychallan.InsuranceIGST_Perc);
                        sqlCommand.Parameters.AddWithValue("@InsuranceIGST_Amt", siteDeliverychallan.InsuranceIGST_Amt);
                        sqlCommand.Parameters.AddWithValue("@Remarks1", siteDeliverychallan.Remarks1);
                        sqlCommand.Parameters.AddWithValue("@Remarks2", siteDeliverychallan.Remarks2);
                        sqlCommand.Parameters.AddWithValue("@FinYearId", siteDeliverychallan.FinYearId);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", siteDeliverychallan.CompanyId);

                        sqlCommand.Parameters.AddWithValue("@CreatedBy", siteDeliverychallan.CreatedBy);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeApplicable", siteDeliverychallan.ReverseChargeApplicable);
                        sqlCommand.Parameters.AddWithValue("@ReverseChargeAmount", siteDeliverychallan.ReverseChargeAmount);
                        sqlCommand.Parameters.AddWithValue("@SiteChallanProductDetail", dtChallanProduct);
                        //sqlCommand.Parameters.AddWithValue("@ChallanTaxDetail", dtChallanTax);
                        sqlCommand.Parameters.AddWithValue("@SiteChallanTermDetail", dtChallanTerm);

                        sqlCommand.Parameters.AddWithValue("@SiteDeliveryChallanSupportingDocument", dtDeliveryChallanDocument);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", siteDeliverychallan.CustomerBranchId);
                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetSiteChallanId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (siteDeliverychallan.SiteChallanId == 0)
                            {
                                responseOut.message = ActionMessage.ChallanCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.ChallanUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetSiteChallanId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetSiteChallanId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }



        public DataTable GetSiteChallanProductList(long siteChallanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteChallanProducts", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteChallanId", siteChallanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
      
        public DataTable GetSiteChallanTermList(long challanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteChallanTerms", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteChallanId", challanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSiteChallanList(string siteChallanNo, string customerName, string dispatchrefNo, DateTime fromDate, DateTime toDate, string approvalStatus, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteChallans", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteChallanNo", siteChallanNo);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    da.SelectCommand.Parameters.AddWithValue("@DispatchRefNo", dispatchrefNo);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetSiteChallanDetail(long siteChallanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteChallanDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteChallanId", siteChallanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetSiteDeliveryChallanSupportingDocumentList(long siteChallanId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetSiteDeliveryChallanSupportingDocument", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SiteDeliveryChallanId", siteChallanId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region MD Dashboard
        public DataTable GetDashboard_EscalationMatrixCount(int companyId, int finYearId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_Dashboard_GetEscalationMatrixCount", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.SelectCommand.Parameters.AddWithValue("@FinYearId", finYearId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }

        #endregion

        #region EscalationMaster
        public DataTable GetEscalationMasterList(string processType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEscalationMasters", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProcessType", processType);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        public DataTable GetEscalationMasterDetail(int EscalationId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetEscalationMasterDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EscalationId", EscalationId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }
        #endregion

        #region Daily Expense
        public ResponseOut AddEditDailyExpense(DailyExpense dailyExpense)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("proc_AddEditDailyExpense", con))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@DailyExpenseId", dailyExpense.DailyExpenseId);
                        sqlCommand.Parameters.AddWithValue("@DailyExpenseCode", dailyExpense.DailyExpenseCode);
                        sqlCommand.Parameters.AddWithValue("@DailyExpenseDate", dailyExpense.DailyExpenseDate);
                        sqlCommand.Parameters.AddWithValue("@EmployeeId", dailyExpense.EmployeeId);
                        sqlCommand.Parameters.AddWithValue("@TimeTypeId", dailyExpense.TimeTypeId);
                        sqlCommand.Parameters.AddWithValue("@ConveyanceAmt", dailyExpense.ConveyanceAmt);
                        sqlCommand.Parameters.AddWithValue("@FoodTeaExpense", dailyExpense.FoodTeaExpense);
                        sqlCommand.Parameters.AddWithValue("@CustomerId", dailyExpense.CustomerId);
                        sqlCommand.Parameters.AddWithValue("@CustomerBranchId", dailyExpense.CustomerBranchId);
                        sqlCommand.Parameters.AddWithValue("@CompanyBranchId", dailyExpense.CompanyBranchId);
                        sqlCommand.Parameters.AddWithValue("@DailyExpenseStatus", dailyExpense.DailyExpenseStatus);
                        sqlCommand.Parameters.AddWithValue("@CompanyId", dailyExpense.CompanyId);

                        sqlCommand.Parameters.Add("@status", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add("@RetDailyExpenseId", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        con.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlCommand.Parameters["@status"].Value != null)
                        {
                            responseOut.status = Convert.ToString(sqlCommand.Parameters["@status"].Value);
                        }
                        if (responseOut.status != ActionStatus.Success)
                        {
                            if (sqlCommand.Parameters["@message"].Value != null)
                            {
                                responseOut.message = Convert.ToString(sqlCommand.Parameters["@message"].Value);
                            }

                        }
                        else
                        {
                            if (dailyExpense.DailyExpenseId == 0)
                            {
                                responseOut.message = ActionMessage.DailyExpenseCreatedSuccess;
                            }
                            else
                            {
                                responseOut.message = ActionMessage.DailyExpenseUpdatedSuccess;
                            }
                            if (sqlCommand.Parameters["@RetDailyExpenseId"].Value != null)
                            {
                                responseOut.trnId = Convert.ToInt64(sqlCommand.Parameters["@RetDailyExpenseId"].Value);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return responseOut;

        }
        public DataTable GetDailyExpensesDetail(long dailyExpensesId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDailyExpensesDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DailyExpensesId", dailyExpensesId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;
        }
        public DataTable GetDailyExpensesList(long employeeId, long timeTypeId, long customerBranchid, DateTime fromDate, DateTime toDate, int companyId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetDailyExpenses", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    da.SelectCommand.Parameters.AddWithValue("@TimeTypeId", timeTypeId);
                    da.SelectCommand.Parameters.AddWithValue("@CustomerBranchId", customerBranchid);
                    da.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                    da.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);
                    da.SelectCommand.Parameters.AddWithValue("@CompanyId", companyId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion

        #region Unit
        public DataTable GetUnitList(string unitName, string unitShortName,int parentId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUnits", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UnitName", unitName);
                    da.SelectCommand.Parameters.AddWithValue("@UnitShortName", unitShortName);
                    da.SelectCommand.Parameters.AddWithValue("@ParentId", parentId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        public DataTable GetUnitDetail(long unitId)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    da = new SqlDataAdapter("proc_GetUnitDetail", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@UnitId", unitId);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dt;

        }

        #endregion




    }
}

