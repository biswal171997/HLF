using Dapper;
using LMSApp.Model.Entities.Loan;
using LMSApp.Model.Entities.MASTERS;
using LMSApp.Model.ServiceProviderMaster;
using LMSApp.Repository.BaseRepository;
using LMSApp.Repository.Factory;
using LMSApp.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Repository.Repositories.Repository
{
    public class LoanLeads : MyAppRepositoryBase, ILoanLead
    {
        public LoanLeads(IMyAppConnectionFactory MyAppConnectionFactory) : base(MyAppConnectionFactory)
        {

        }

        public async Task<int> SaveLoanLeads(LoanLeadDetails loanLeadDetails)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@ActionCode", "Insert");  // CategoryID, will be 0 for new entries
                param.Add("@Name", loanLeadDetails.Name);
                param.Add("@Mobile", loanLeadDetails.Mobile);
                param.Add("@Whatsup", loanLeadDetails.Whatsup);
                param.Add("@Email", loanLeadDetails.Email);
                param.Add("@StateID", loanLeadDetails.StateID);
                param.Add("@DistrictId", loanLeadDetails.DistrictId);
                param.Add("@City", loanLeadDetails.City);
                param.Add("@ReferenceBy", loanLeadDetails.ReferenceBy);
                param.Add("@LGLC_Code", loanLeadDetails.LGLC_Code);
                param.Add("@Remarks", loanLeadDetails.Remarks);
                param.Add("@CreatedBy", loanLeadDetails.CreatedBy);
                param.Add("@NextFollowupDate", loanLeadDetails.NextFollowupDate);

                param.Add("@CategoryID", loanLeadDetails.CategoryID);
                param.Add("@SubCategoryID", loanLeadDetails.SubCategoryID);
                param.Add("@ProfessionID", loanLeadDetails.ProfessionID);
                param.Add("@RequestAmount", loanLeadDetails.RequestAmount);
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Execute the stored procedure
                await Connection.ExecuteAsync("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ReturnMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the leads.", ex);
            }
        }

        public async Task<int> UpdateLoanLeads(LoanLeadDetails loanLeadDetails)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@ActionCode", "Update");  // CategoryID, will be 0 for new entries
                param.Add("@LeadID", loanLeadDetails.LeadID);
                param.Add("@Name", loanLeadDetails.Name);
                param.Add("@Mobile", loanLeadDetails.Mobile);
                param.Add("@Whatsup", loanLeadDetails.Whatsup);
                param.Add("@Email", loanLeadDetails.Email);
                param.Add("@StateID", loanLeadDetails.StateID);
                param.Add("@DistrictId", loanLeadDetails.DistrictId);
                param.Add("@City", loanLeadDetails.City);
                param.Add("@ReferenceBy", loanLeadDetails.ReferenceBy);
                param.Add("@LGLC_Code", loanLeadDetails.LGLC_Code);
                param.Add("@Remarks", loanLeadDetails.Remarks);
                param.Add("@CreatedBy", loanLeadDetails.CreatedBy);
                param.Add("@NextFollowupDate", loanLeadDetails.NextFollowupDate);
                param.Add("@CategoryID", loanLeadDetails.CategoryID);
                param.Add("@SubCategoryID", loanLeadDetails.SubCategoryID);
                param.Add("@ProfessionID", loanLeadDetails.ProfessionID);
                param.Add("@RequestAmount", loanLeadDetails.RequestAmount);
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Execute the stored procedure
                await Connection.ExecuteAsync("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ReturnMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the leads.", ex);
            }
        }

        public async Task<List<LoanLeadDetails>> ViewLoanLeads(LoanLeadDetails loanLeadDetails)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ActionCode", "View");
                p.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585); // Corrected parameter name
                var results = await Connection.QueryAsync<LoanLeadDetails>("sp_ManageLeadMaster", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }


            catch (Exception EX)
            {
                throw EX;
            }
        }

        //--EditBy Id LoanLeads
        public async Task<LoanLeadDetails> ViewLoanLeadsById(int LeadID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LeadID", LeadID);
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "Edit");
                var x = Connection.Query<LoanLeadDetails>("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--Profession
        public async Task<List<LoanLeadDetails>> ViewProfession()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "Profession");
                var x = Connection.Query<LoanLeadDetails>("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--District
        public async Task<List<LoanLeadDetails>> ViewDistrict()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "District");
                var x = Connection.Query<LoanLeadDetails>("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--Product Category
        public async Task<List<LoanLeadDetails>> ViewProductCategory()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "Ptype");
                var x = Connection.Query<LoanLeadDetails>("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ViewProductSubCategory
        public async Task<List<ServiceSubCategory>> ViewProductSubCategory(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "Psubtype");
                param.Add("@CategoryID", Id);
                var x = Connection.Query<ServiceSubCategory>("sp_ManageLeadMaster", param, commandType: CommandType.StoredProcedure).ToList();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--Add Follow Up History

        public async Task<int> SaveFollowupHistory(FollowupHistory followUp)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@ActionCode", "Insert");  
                param.Add("@LeadID", followUp.LeadID);
                param.Add("@FollowupDate", followUp.FollowupDate);
                param.Add("@NextFollowUpDate", followUp.NextFallowUpDate);
                param.Add("@Remarks", followUp.Remarks);
                param.Add("@CreatedBy", followUp.CreatedBy);

                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Execute the stored procedure
                await Connection.ExecuteAsync("InsertFollowupHistory", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ReturnMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the leads.", ex);
            }
        }

        //--View Follow Up History
        public async Task<List<FollowupHistory>> ViewFollowupHistory(FollowupHistory followup)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ActionCode", "View");
                p.Add("@LeadID", followup.LeadID);
                p.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585); // Corrected parameter name
                var results = await Connection.QueryAsync<FollowupHistory>("InsertFollowupHistory", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public async Task<int> SaveApplied(ApplicationAppliedDetailViewModel applicationAppliedDetail)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@ActionCode", "INSERT");  // CategoryID, will be 0 for new entries
                param.Add("@AppliedID", applicationAppliedDetail.AppliedID);
                param.Add("@LeadID", applicationAppliedDetail.LeadID);
                param.Add("@ServiceProviderID", applicationAppliedDetail.ServiceProviderID);
                param.Add("@AppliedDate", applicationAppliedDetail.AppliedDate);
                param.Add("@AppliedAmount", applicationAppliedDetail.AppliedAmount);
                param.Add("@ApprovedAmount", applicationAppliedDetail.ApprovedAmount);
                param.Add("@NoOfInstallment", applicationAppliedDetail.NoOfInstallment);
                param.Add("@Status", applicationAppliedDetail.Status);
                param.Add("@Remarks", applicationAppliedDetail.Remarks);
                param.Add("@CreatedBy", applicationAppliedDetail.CreatedBy);
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Execute the stored procedure
                await Connection.ExecuteAsync("ManageApplicationAppliedDetails", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ReturnMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the leads.", ex);
            }
        }

        public Task<int> UpdateApplied(ApplicationAppliedDetailViewModel applicationAppliedDetail)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteApplied(int AppliedID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationAppliedDetailViewModel>> ViewApliedDetails(int AppliedID)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ActionCode", "View");
                p.Add("@LeadID", AppliedID);
                p.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585); // Corrected parameter name
                var results = await Connection.QueryAsync<ApplicationAppliedDetailViewModel>("ManageApplicationAppliedDetails", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public async Task<int> LeadConvertStatusUpdate(int LeadID)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@ActionCode", "CLead");  // CategoryID, will be 0 for new entries
                param.Add("@LeadID", LeadID);
                
                param.Add("@CreatedBy", 1);
                param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Execute the stored procedure
                await Connection.ExecuteAsync("ManageApplicationAppliedDetails", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ReturnMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the leads.", ex);
            }
        }

        //View ServiceProviderByLeadId


        public async Task<List<ServiceProviderMaster>> ViewServiceProviderByLeadId(int LeadID)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ActionCode", "View");
                p.Add("@LeadID", LeadID);
                //p.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585); 
                var results = await Connection.QueryAsync<ServiceProviderMaster>("SuggestServiceProviderbyLead", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        //public async Task<List<ServiceProviderMaster>> ViewServiceProviderByLeadId1(int LeadID)
        //{
        //    try
        //    {
        //        DynamicParameters param = new DynamicParameters();
        //        //param.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
        //        param.Add("@ActionCode", "View");
        //        param.Add("@LeadID", LeadID);
        //        var x = Connection.Query<ServiceProviderMaster>("SuggestServiceProviderbyLead", param, commandType: CommandType.StoredProcedure).ToList();
        //        return x;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
