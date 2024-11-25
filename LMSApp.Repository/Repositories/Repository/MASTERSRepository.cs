using System.Collections.Generic;
using LMSApp.Repository.Factory;
using LMSApp.Repository.BaseRepository;
using LMSApp.Repository.Repositories.Interfaces;
using LMSApp.Repository;

using LMSApp.Model.ServiceProviderMaster;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Http;
using LMSApp.Model.Entities.MASTERS;
using static LMSApp.Repository.Repository.MASTERSRepository;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Data.Common;
using LMSApp.Model.Entities.Loan;
namespace LMSApp.Repository.Repository
{
    public class MASTERSRepository : MyAppRepositoryBase, IMASTERSRepository
    {
        public MASTERSRepository(IMyAppConnectionFactory MyAppConnectionFactory) : base(MyAppConnectionFactory)
        {

        }

        #region Repo Methods ServiceProviderMaster
        public async Task<int> SaveServiceProvider(ServiceProviderMaster serviceProviderMaster, IFormFile logoFile)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@P_ServiceProviderID", serviceProviderMaster.Id);
                param.Add("@P_ServiceProviderName", serviceProviderMaster.ProviderName);

                string logoFileName = null;

                if (logoFile != null && logoFile.Length > 0)
                {
                    // Generate a unique filename for the logo
                    logoFileName = $"{Guid.NewGuid()}_{logoFile.FileName}";

                    // Define the path to save the logo (ensure this folder exists)
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "logos");

                    // Ensure the folder exists
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Save the file to the server
                    string filePath = Path.Combine(uploadPath, logoFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await logoFile.CopyToAsync(stream);
                    }
                }
                else
                {
                    // Handle the case when no file is uploaded (if needed)
                    logoFileName = serviceProviderMaster.Logo;  // Use existing logo name if not updated
                }

                param.Add("@P_Logo", logoFileName); // Save the file name, not the file content
                param.Add("@P_UserID", serviceProviderMaster.UserID);
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Set action (Insert or Update)
                if (serviceProviderMaster.Id == 0)
                {
                    param.Add("@P_Action", "I");
                }
                else
                {
                    param.Add("@P_Action", "U");
                }

                // Execute the stored procedure
                Connection.Execute("USP_ServiceProviderMaster", param, commandType: CommandType.StoredProcedure);

                // Get the output message from the stored procedure
                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception (log it or throw custom exception as needed)
                throw ex;
            }
        }
        public async Task<int> Delete_ServiceProviderMaster(int Id)
        {

            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Action", "D");
                p.Add("@P_Id", Id);

                var results = await Connection.ExecuteAsync("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
                return results;
            }
            catch (Exception EX)
            {
                throw EX;
            }

        }
        public async Task<ServiceProviderMaster> GetServiceProviderMasterById(int Id)
        {

            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Action", "E");
                p.Add("@P_Id", Id);

                var results = await Connection.QueryAsync<ServiceProviderMaster>("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }

            catch (Exception EX)
            {
                throw EX;
            }
        }
        public async Task<List<ServiceProviderMaster>> Getall_ServiceProviderMaster(ServiceProviderMaster TBL)
        {

            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Action", "V");
                var results = await Connection.QueryAsync<ServiceProviderMaster>("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }


            catch (Exception EX)
            {
                throw EX;
            }

        }
        #endregion

        #region Repo Method ServiceCategory

        public async Task<int> SaveServiceCategory(ServiceCategory category)
        {
            try
            {
                // Create dynamic parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();

                // Add parameters for the stored procedure
                param.Add("@P_CategoryID", category.CategoryID);  // CategoryID, will be 0 for new entries
                param.Add("@P_CategoryName", category.CategoryName);
                param.Add("@P_CreatedBy", category.CreatedBy);
                param.Add("@P_IsDeleted", category.IsDeleted);
                param.Add("@P_IsActive", category.IsActive);
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Set the action (Insert or Update) based on CategoryID
                if (category.CategoryID == 0)
                {
                    param.Add("@P_Action", "I");  // Insert action
                }
                else
                {
                    param.Add("@P_Action", "U");  // Update action
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_ServiceCategory", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the service category.", ex);
            }
        }

        public async Task<List<ServiceCategory>> Getall_ServiceCategory(ServiceCategory TBL)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Action", "V");
                var results = await Connection.QueryAsync<ServiceCategory>("USP_ServiceCategory", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public async Task<ServiceCategory> GetServiceCategoryById(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("P_CategoryID", Id);
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@P_Action", "E");
                var x = Connection.Query<ServiceCategory>("USP_ServiceCategory", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Comment
        //public async Task<ServiceCategory> GetServiceCategoryById(int Id)
        //{
        //    try
        //    {
        //        // Define the dynamic parameters for the stored procedure
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("@P_CategoryID", Id);

        //        // Execute the stored procedure to get the ServiceCategory by ID
        //        var category = await Connection.QuerySingleOrDefaultAsync<ServiceCategory>(
        //            "USP_GetServiceCategoryById",
        //            param,
        //            commandType: CommandType.StoredProcedure
        //        );

        //        // Return the category (will be null if not found)
        //        return category;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that may occur
        //        throw new Exception($"An error occurred while retrieving the service category with ID {Id}.", ex);
        //    }
        //}
        #endregion

        public async Task<int> Delete_ServiceCategory(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_CategoryID", Id);
                param.Add("@P_Action", "D");
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_ServiceCategory", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Repo Method ServiceSubCategory
        public async Task<int> SaveServiceSubCategory(ServiceSubCategory subcat)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                // Add parameters for the stored procedure
                param.Add("@P_SubCategoryId", subcat.SubCategoryID);  // CategoryID, will be 0 for new entries
                param.Add("@P_CategoryId", subcat.CategoryID);  // CategoryID, will be 0 for new entries
                param.Add("@P_SubCategoryName", subcat.SubCategoryName);
                
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Set the action (Insert or Update) based on CategoryID
                if (subcat.SubCategoryID == 0)
                {
                    param.Add("@P_Action", "I");  // Insert action
                }
                else
                {
                    param.Add("@P_Action", "U");  // Update action
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("USP_SubCategory", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the service category.", ex);
            }
        }
        public async Task<List<ServiceSubCategory>> Getall_ServiceSubCategory(ServiceSubCategory TBL)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Action", "V");
                var results = await Connection.QueryAsync<ServiceSubCategory>("USP_SubCategory", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        //--EditBy Id Commision Config
        public async Task<ServiceSubCategory> Getall_ServiceSubCategoryById(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_SubCategoryId", Id);
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@P_Action", "E");
                var x = Connection.Query<ServiceSubCategory>("USP_SubCategory", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> Delete_ServiceSubCategoryById(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_SubCategoryId", Id);
                param.Add("@P_Action", "D");
                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("USP_SubCategory", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Repo Method CommisionConfig
        public async Task<int> SaveCommisionConfig(CommisionConfigMaster config)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                // Add parameters for the stored procedure
                param.Add("@CommisionConfigId", config.CommisionConfigId);  // CategoryID, will be 0 for new entries
                param.Add("@ServiceProviderID", config.ServiceProviderID);  // CategoryID, will be 0 for new entries
                param.Add("@CategoryID", config.CategoryID);
                param.Add("@SubCategoryID", config.SubCategoryID);
                param.Add("@CommisionType", config.CommisionType);
                param.Add("@CommissionAmount", config.CommissionAmount);
                param.Add("@CreatedBy", config.CreatedBy);

                //param.Add("@IsDeleted", config.IsDeleted);
                //param.Add("@IsActive", config.IsActive);
                param.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                // Set the action (Insert or Update) based on CategoryID
                if (config.CommisionConfigId == 0)
                {
                    param.Add("@ActionCode", "INSERT");  // Insert action
                }
                else
                {
                    param.Add("@ActionCode", "UPDATE");  // Update action
                }

                // Execute the stored procedure
                await Connection.ExecuteAsync("sp_ManageCommisionMaster", param, commandType: CommandType.StoredProcedure);

                // Retrieve the output message from the stored procedure (if any)
                int result = Convert.ToInt32(param.Get<string>("@ResultMessage"));
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                throw new Exception("An error occurred while saving the service category.", ex);
            }
        }
        public async Task<List<CommisionConfigMaster>> Getall_CommisionConfig(CommisionConfigMaster TBL)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ActionCode", "View");
                p.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                var results = await Connection.QueryAsync<CommisionConfigMaster>("sp_ManageCommisionMaster", p, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        //--EditBy Id Commision Config
        public async Task<CommisionConfigMaster> Getall_CommisionConfigById(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommisionConfigId", Id);
                param.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                param.Add("@ActionCode", "Edit");
                var x = Connection.Query<CommisionConfigMaster>("sp_ManageCommisionMaster", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete_CommisionConfigById(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommisionConfigId", Id);
                param.Add("@ActionCode", "DELETE");
                param.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Connection.Execute("sp_ManageCommisionMaster", param, commandType: CommandType.StoredProcedure);
                int result = Convert.ToInt32(param.Get<string>("@ResultMessage"));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}












//----------------------------------------------------------------------------------------
//using System.Collections.Generic;
//using LMSApp.Repository.Factory;
//using LMSApp.Repository.BaseRepository;
//using LMSApp.Repository.Repositories.Interfaces;
//using LMSApp.Repository;

//using LMSApp.Model.ServiceProviderMaster;
//using Dapper;
//using System.Data;
//using Microsoft.AspNetCore.Http;
//using LMSApp.Model.Entities.MASTERS;
//using static LMSApp.Repository.Repository.MASTERSRepository;
//using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
//using System.Data.Common;
//namespace LMSApp.Repository.Repository
//{
//    public class MASTERSRepository : MyAppRepositoryBase, IMASTERSRepository
//    {
//        public MASTERSRepository(IMyAppConnectionFactory MyAppConnectionFactory) : base(MyAppConnectionFactory)
//        {

//        }

//        #region Repo Methods ServiceProviderMaster
//        public async Task<int> SaveServiceProvider(ServiceProviderMaster serviceProviderMaster, IFormFile logoFile)
//        {
//            try
//            {
//                DynamicParameters param = new DynamicParameters();

//                param.Add("@P_ServiceProviderID", serviceProviderMaster.Id);
//                param.Add("@P_ServiceProviderName", serviceProviderMaster.ProviderName);

//                string logoFileName = null;

//                if (logoFile != null && logoFile.Length > 0)
//                {
//                    // Generate a unique filename for the logo
//                    logoFileName = $"{Guid.NewGuid()}_{logoFile.FileName}";

//                    // Define the path to save the logo (ensure this folder exists)
//                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "logos");

//                    // Ensure the folder exists
//                    if (!Directory.Exists(uploadPath))
//                    {
//                        Directory.CreateDirectory(uploadPath);
//                    }

//                    // Save the file to the server
//                    string filePath = Path.Combine(uploadPath, logoFileName);
//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await logoFile.CopyToAsync(stream);
//                    }
//                }
//                else
//                {
//                    // Handle the case when no file is uploaded (if needed)
//                    logoFileName = serviceProviderMaster.Logo;  // Use existing logo name if not updated
//                }

//                param.Add("@P_Logo", logoFileName); // Save the file name, not the file content
//                param.Add("@P_UserID", serviceProviderMaster.UserID);
//                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

//                // Set action (Insert or Update)
//                if (serviceProviderMaster.Id == 0)
//                {
//                    param.Add("@P_Action", "I");
//                }
//                else
//                {
//                    param.Add("@P_Action", "U");
//                }

//                // Execute the stored procedure
//                Connection.Execute("USP_ServiceProviderMaster", param, commandType: CommandType.StoredProcedure);

//                // Get the output message from the stored procedure
//                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
//                return result;
//            }
//            catch (Exception ex)
//            {
//                // Handle the exception (log it or throw custom exception as needed)
//                throw ex;
//            }
//        }
//        public async Task<int> Delete_ServiceProviderMaster(int Id)
//        {

//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@P_Action", "D");
//                p.Add("@P_Id", Id);

//                var results = await Connection.ExecuteAsync("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
//                return results;
//            }
//            catch (Exception EX)
//            {
//                throw EX;
//            }

//        }
//        public async Task<ServiceProviderMaster> GetServiceProviderMasterById(int Id)
//        {

//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@P_Action", "E");
//                p.Add("@P_Id", Id);

//                var results = await Connection.QueryAsync<ServiceProviderMaster>("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
//                return results.FirstOrDefault();
//            }

//            catch (Exception EX)
//            {
//                throw EX;
//            }
//        }
//        public async Task<List<ServiceProviderMaster>> Getall_ServiceProviderMaster(ServiceProviderMaster TBL)
//        {

//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@P_Action", "V");
//                var results = await Connection.QueryAsync<ServiceProviderMaster>("USP_ServiceProviderMaster", p, commandType: CommandType.StoredProcedure);
//                return results.ToList();
//            }


//            catch (Exception EX)
//            {
//                throw EX;
//            }

//        }
//        #endregion

//        #region Repo Method ServiceCategory

//        public async Task<int> SaveServiceCategory(ServiceCategory category)
//        {
//            try
//            {
//                // Create dynamic parameters for the stored procedure
//                DynamicParameters param = new DynamicParameters();

//                // Add parameters for the stored procedure
//                param.Add("@P_CategoryID", category.CategoryID);  // CategoryID, will be 0 for new entries
//                param.Add("@P_CategoryName", category.CategoryName);
//                param.Add("@P_CreatedBy", category.CreatedBy);
//                param.Add("@P_IsDeleted", category.IsDeleted);
//                param.Add("@P_IsActive", category.IsActive);
//                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

//                // Set the action (Insert or Update) based on CategoryID
//                if (category.CategoryID == 0)
//                {
//                    param.Add("@P_Action", "I");  // Insert action
//                }
//                else
//                {
//                    param.Add("@P_Action", "U");  // Update action
//                }

//                // Execute the stored procedure
//                await Connection.ExecuteAsync("USP_ServiceCategory", param, commandType: CommandType.StoredProcedure);

//                // Retrieve the output message from the stored procedure (if any)
//                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
//                return result;
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions that may occur
//                throw new Exception("An error occurred while saving the service category.", ex);
//            }
//        }

//        public async Task<List<ServiceCategory>> Getall_ServiceCategory(ServiceCategory TBL)
//        {
//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@P_Action", "V");
//                var results = await Connection.QueryAsync<ServiceCategory>("USP_ServiceCategory", p, commandType: CommandType.StoredProcedure);
//                return results.ToList();
//            }
//            catch (Exception EX)
//            {
//                throw EX;
//            }
//        }


//        public async Task<ServiceCategory> GetServiceCategoryById(int Id)
//        {
//            try
//            {
//                // Define the dynamic parameters for the stored procedure
//                DynamicParameters param = new DynamicParameters();
//                param.Add("@P_CategoryID", Id);

//                // Execute the stored procedure to get the ServiceCategory by ID
//                var category = await Connection.QuerySingleOrDefaultAsync<ServiceCategory>(
//                    "USP_GetServiceCategoryById",
//                    param,
//                    commandType: CommandType.StoredProcedure
//                );

//                // Return the category (will be null if not found)
//                return category;
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions that may occur
//                throw new Exception($"An error occurred while retrieving the service category with ID {Id}.", ex);
//            }
//        }

//        public async Task<int> Delete_SaveServiceCategory(int Id)
//        {
//            try
//            {
//                // Define the dynamic parameters for the stored procedure
//                DynamicParameters param = new DynamicParameters();
//                param.Add("@P_CategoryID", Id);
//                param.Add("@P_IsDeleted", 1);  // Setting IsDeleted to 1 to mark the category as deleted (soft delete)

//                // Execute the stored procedure to mark the category as deleted
//                var result = await Connection.ExecuteAsync(
//                    "USP_Delete_SaveServiceCategory",
//                    param,
//                    commandType: CommandType.StoredProcedure
//                );

//                // Return the result (number of rows affected)
//                return result;
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions
//                throw new Exception($"An error occurred while deleting the service category with ID {Id}.", ex);
//            }
//        }

//        #endregion

//        #region Repo Method ServiceSubCategory
//        public async Task<int> SaveServiceSubCategory(ServiceSubCategory subcat)
//        {
//            try
//            {
//                DynamicParameters param = new DynamicParameters();
//                // Add parameters for the stored procedure
//                param.Add("@P_SubCategoryId", subcat.SubCategoryID);  // CategoryID, will be 0 for new entries
//                param.Add("@P_CategoryId", subcat.CategoryID);  // CategoryID, will be 0 for new entries
//                param.Add("@P_SubCategoryName", subcat.SubCategoryName);
//                param.Add("@P_CreatedBy", subcat.CreatedBy);
//                param.Add("@P_IsDeleted", subcat.IsDeleted);
//                param.Add("@P_IsActive", subcat.IsActive);
//                param.Add("@P_MSG_OUT", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

//                // Set the action (Insert or Update) based on CategoryID
//                if (subcat.SubCategoryID == 0)
//                {
//                    param.Add("@P_Action", "I");  // Insert action
//                }
//                else
//                {
//                    param.Add("@P_Action", "U");  // Update action
//                }

//                // Execute the stored procedure
//                await Connection.ExecuteAsync("USP_SubCategory", param, commandType: CommandType.StoredProcedure);

//                // Retrieve the output message from the stored procedure (if any)
//                int result = Convert.ToInt32(param.Get<string>("@P_MSG_OUT"));
//                return result;
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions that may occur
//                throw new Exception("An error occurred while saving the service category.", ex);
//            }
//        }
//        public async Task<List<ServiceSubCategory>> Getall_ServiceCategory(ServiceSubCategory TBL)
//        {
//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@P_Action", "V");
//                var results = await Connection.QueryAsync<ServiceSubCategory>("USP_SubCategory", p, commandType: CommandType.StoredProcedure);
//                return results.ToList();
//            }
//            catch (Exception EX)
//            {
//                throw EX;
//            }
//        }

//        #endregion

//        #region Repo Method CommisionConfig
//        public async Task<int> SaveCommisionConfig(CommisionConfigMaster config)
//        {
//            try
//            {
//                DynamicParameters param = new DynamicParameters();
//                // Add parameters for the stored procedure
//                param.Add("@CommisionConfigId", config.CommisionConfigId);  // CategoryID, will be 0 for new entries
//                param.Add("@ServiceProviderID", config.ServiceProviderID);  // CategoryID, will be 0 for new entries
//                param.Add("@CategoryID", config.CategoryID);
//                param.Add("@SubCategoryID", config.SubCategoryID);
//                param.Add("@CommisionType", config.CommisionType);
//                param.Add("@CommissionAmount", config.CommissionAmount);
//                param.Add("@CreatedBy", config.CreatedBy);

//                //param.Add("@IsDeleted", config.IsDeleted);
//                //param.Add("@IsActive", config.IsActive);
//                param.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

//                // Set the action (Insert or Update) based on CategoryID
//                if (config.CommisionConfigId == 0)
//                {
//                    param.Add("@ActionCode", "INSERT");  // Insert action
//                }
//                else
//                {
//                    param.Add("@ActionCode", "UPDATE");  // Update action
//                }

//                // Execute the stored procedure
//                await Connection.ExecuteAsync("sp_ManageCommisionMaster", param, commandType: CommandType.StoredProcedure);

//                // Retrieve the output message from the stored procedure (if any)
//                int result = Convert.ToInt32(param.Get<string>("@ResultMessage"));
//                return result;
//            }
//            catch (Exception ex)
//            {
//                // Handle any exceptions that may occur
//                throw new Exception("An error occurred while saving the service category.", ex);
//            }
//        }
//        public async Task<List<CommisionConfigMaster>> Getall_CommisionConfig(CommisionConfigMaster TBL)
//        {
//            try
//            {
//                var p = new DynamicParameters();
//                p.Add("@ActionCode", "View");
//                var results = await Connection.QueryAsync<CommisionConfigMaster>("sp_ManageCommisionMaster", p, commandType: CommandType.StoredProcedure);
//                return results.ToList();
//            }
//            catch (Exception EX)
//            {
//                throw EX;
//            }
//        }

//        //Task<List<ServiceSubCategory>> IMASTERSRepository.Getall_ServiceSubCategory(ServiceSubCategory TBL)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        Task<ServiceSubCategory> IMASTERSRepository.Getall_ServiceSubCategoryById(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<int> IMASTERSRepository.Delete_ServiceSubCategoryById(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<CommisionConfigMaster> IMASTERSRepository.Getall_CommisionConfigById(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<int> IMASTERSRepository.Delete_CommisionConfigById(int Id)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion
//    }
//}
