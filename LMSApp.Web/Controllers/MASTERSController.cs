using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using LMSApp.Model.ServiceProviderMaster;
using LMSApp.Repository.Repositories.Interfaces;
using LMSApp.Model.Entities.MASTERS;
namespace LMSApp.Web
{
    public class MASTERSController : Controller
    {

        public IConfiguration Configuration;
        private readonly IMASTERSRepository _MASTERSRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public MASTERSController(IConfiguration configuration, IMASTERSRepository MASTERSRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MASTERSRepository = MASTERSRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        #region Service Provider
        [HttpGet]
        public IActionResult ServiceProviderMaster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveServiceProvider([FromForm] ServiceProviderMaster serviceProviderMaster, IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                // Handle the file upload (save the file, etc.)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "logos", logoFile.FileName);

                // Ensure the directory exists
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    logoFile.CopyTo(stream);
                }

                // Set the logo file path or file name in the service provider object
                serviceProviderMaster.Logo = filePath; // or just use the file name if you prefer
            }

            // Save the service provider information to the database
            var data = _MASTERSRepository.SaveServiceProvider(serviceProviderMaster, logoFile);

            return Json(new { success = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
        }


        #region Comment
        //[HttpPost]
        //public IActionResult ServiceProviderMaster(ServiceProviderMaster TBL)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var message = string.Join(" | ", ModelState.Values
        //         .SelectMany(v => v.Errors)
        //        .Select(e => e.ErrorMessage));
        //        return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
        //    }
        //    else
        //    {
        //        if (TBL.Id == 0 || TBL.Id == null)
        //        {
        //            var data = _MASTERSRepository.Insert_ServiceProviderMaster(TBL);
        //            return Json(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
        //        }
        //        else
        //        {
        //            var data = _MASTERSRepository.Insert_ServiceProviderMaster(TBL);
        //            return Json(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });
        //        }

        //    }
        //}
        #endregion

        [HttpGet]
        public IActionResult ViewServiceProviderMaster()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> Get_ServiceProviderMaster()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
     .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<ServiceProviderMaster> lst = await _MASTERSRepository.Getall_ServiceProviderMaster(new ServiceProviderMaster());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        //[HttpPost("SearchServiceProviderMaster")]
        //public async Task<IActionResult> Search_ServiceProviderMaster([FromBody] ServiceProviderMaster BL)
        //{
        //    List<ServiceProviderMaster> lst = await _MASTERSRepository.Search_ServiceProviderMaster(BL);
        //    var jsonres = JsonConvert.SerializeObject(lst);
        //    return Json(jsonres);
        //}

        [HttpDelete]
        public async Task<JsonResult> Delete_ServiceProviderMaster(int Id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                var data = _MASTERSRepository.Delete_ServiceProviderMaster(Id);
                return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetByID_ServiceProviderMaster(int Id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                ServiceProviderMaster lst = await _MASTERSRepository.GetServiceProviderMasterById(Id);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);
            }
        }
        #endregion

        #region ServiceCategory 
        public IActionResult ServiceCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveServiceCategory([FromBody] ServiceCategory category)
        {
            if (category == null)
            {
                return BadRequest(new { success = false, message = "Invalid category data." });
            }

            try
            {
                // Call the repository method to save the category
                category.CreatedBy = 1;
                var result = await _MASTERSRepository.SaveServiceCategory(category);

                // Check the result to determine if the operation was successful
                if (result > 0)
                {
                    //return Json(new { success = true, message = "Category saved successfully.", result });
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "Category saved successfully.", result });
                    }
                    else if (result == 2)
                    {
                        return Json(new { success = true, message = "Category updated successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to save Category." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to save category." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging service)
                return StatusCode(500, new { success = false, message = "An error occurred while saving the category.", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> Get_ServiceCategory()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
     .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<ServiceCategory> lst = await _MASTERSRepository.Getall_ServiceCategory(new ServiceCategory());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        //[HttpGet]
        //public IActionResult Get_ServiceCategoryById(int SubcatId)
        //{
        //    var CommisionConfigById = _MASTERSRepository.Getall_ServiceSubCategoryById(Convert.ToInt32(SubcatId)).Result;
        //    return Ok(JsonConvert.SerializeObject(CommisionConfigById));
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetServiceProvidersByCategory([FromQuery] int categoryId)
        //{
        //    if (categoryId <= 0)
        //    {
        //        return BadRequest(new { success = false, message = "Invalid category ID." });
        //    }

        //    try
        //    {
        //        // Create a new ServiceCategory object using the categoryId
        //        var category = new ServiceCategory { CategoryID = categoryId };

        //        // Call the repository method to get the service providers for the category
        //        var serviceProviders = await _MASTERSRepository.Getall_ServiceCategory(category);

        //        // Check if any service providers were returned
        //        if (serviceProviders != null && serviceProviders.Any())
        //        {
        //            return Json(new { success = true, message = "Service providers retrieved successfully.", data = serviceProviders });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "No service providers found for the specified category." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (optional: consider using a logging service like Serilog or NLog)
        //        return StatusCode(500, new { success = false, message = "An error occurred while retrieving the service providers.", error = ex.Message });
        //    }
        //}



        public IActionResult GetServiceCategoryById(int id)
        {
            var CommisionConfigById = _MASTERSRepository.GetServiceCategoryById(Convert.ToInt32(id)).Result;
            return Ok(JsonConvert.SerializeObject(CommisionConfigById));
        }

        #region Comment
        //[HttpGet("category/{id}")]
        //public async Task<IActionResult> GetServiceCategoryById(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest(new { success = false, message = "Invalid category ID." });
        //    }

        //    try
        //    {
        //        // Call the repository method to get the category by ID
        //        var category = await _MASTERSRepository.GetServiceCategoryById(id);

        //        // Check if the category was found
        //        if (category != null)
        //        {
        //            return Json(new { success = true, message = "Service category retrieved successfully.", data = category });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "Service category not found." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (optional: consider using a logging service like Serilog or NLog)
        //        return StatusCode(500, new { success = false, message = "An error occurred while retrieving the service category.", error = ex.Message });
        //    }
        //}
        #endregion

        [HttpDelete]
        public async Task<IActionResult> DeleteServiceCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid category ID." });
            }

            try
            {
                // Call the repository method to delete (soft delete) the category
                var result = await _MASTERSRepository.Delete_ServiceCategory(id);

                // Check the result to determine if the operation was successful
                if (result > 0)
                {
                    //return Json(new { success = true, message = "Service category deleted successfully." });
                    if (result == 3)
                    {
                        return Json(new { success = true, message = "Category deleted successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to delete Category." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete service category. It might not exist." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional: consider using a logging service like Serilog or NLog)
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the service category.", error = ex.Message });
            }
        }
        #endregion

        #region Service SubCategory
        public IActionResult ServiceSubCategory()
        {
            ViewBag.Result = _MASTERSRepository.Getall_ServiceCategory(new ServiceCategory()).Result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveServiceSubCategory([FromBody] ServiceSubCategory subcategory)
        {
            if (subcategory == null)
            {
                return BadRequest(new { success = false, message = "Invalid Subcategory data." });
            }

            try
            {
                // Call the repository method to save the category
                subcategory.CreatedBy = 1;
                var result = await _MASTERSRepository.SaveServiceSubCategory(subcategory);

                // Check the result to determine if the operation was successful
                if (result > 0)
                {
                    //return Json(new { success = true, message = "SubCategory saved successfully.", result });
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "SubCategory saved successfully.", result });
                    }
                    else if (result == 2)
                    {
                        return Json(new { success = true, message = "SubCategory updated successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to save SubCategory." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to save SubCategory." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging service)
                return StatusCode(500, new { success = false, message = "An error occurred while saving the category.", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> Get_ServiceSubCategory()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
     .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<ServiceSubCategory> lst = await _MASTERSRepository.Getall_ServiceSubCategory(new ServiceSubCategory());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpGet]
        public IActionResult Get_ServiceSubCategoryById(int SubcatId)
        {
            var CommisionConfigById = _MASTERSRepository.Getall_ServiceSubCategoryById(Convert.ToInt32(SubcatId)).Result;
            return Ok(JsonConvert.SerializeObject(CommisionConfigById));
        }

        [HttpDelete]
        public IActionResult Delete_ServiceSubCategoryById(int Id)
        {
            try
            {
                int result = _MASTERSRepository.Delete_ServiceSubCategoryById(Id).Result;
                //return Json(result);

                if (result > 0)
                {
                    if (result == 3)
                    {
                        return Json(new { success = true, message = "SubCategory deleted successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to delete SubCategory." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete SubCategory." });
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region CommisionConfig
        public IActionResult CommisionConfig()
        {
            ViewBag.Result = _MASTERSRepository.Getall_ServiceProviderMaster(new ServiceProviderMaster()).Result;
            ViewBag.ResultCategory = _MASTERSRepository.Getall_ServiceCategory(new ServiceCategory()).Result;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCommisionConfig([FromBody] CommisionConfigMaster config)
        {
            if (config == null)
            {
                return BadRequest(new { success = false, message = "Invalid Commision data." });
            }

            try
            {
                // Ensure CreatedBy is set
                config.CreatedBy = 1;

                // Call the repository method to save the category
                var result = await _MASTERSRepository.SaveCommisionConfig(config);

                // Check the result to determine if the operation was successful
                if (result > 0)
                {
                    if (result == 1)
                    {
                        return Json(new { success = true, message = "Commision Config saved successfully.", result });
                    }
                    else if (result == 2)
                    {
                        return Json(new { success = true, message = "Commision Config updated successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to save Commision Config." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to save Commision Config." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging service)
                Console.WriteLine(ex.ToString()); // Replace with your logging mechanism
                return StatusCode(500, new { success = false, message = "An error occurred while saving the Commision Config.", error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<JsonResult> Getall_CommisionConfig()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
     .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                List<CommisionConfigMaster> lst = await _MASTERSRepository.Getall_CommisionConfig(new CommisionConfigMaster());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpGet]
        public IActionResult Get_CommisionConfigById(int ConfigId)
        {
            var CommisionConfigById = _MASTERSRepository.Getall_CommisionConfigById(Convert.ToInt32(ConfigId)).Result;
            return Ok(JsonConvert.SerializeObject(CommisionConfigById));
        }

        [HttpDelete]
        public IActionResult Delete_CommisionConfigById(int Id)
        {
            try
            {
                int result = _MASTERSRepository.Delete_CommisionConfigById(Id).Result;
                //return Json(result);

                if (result > 0)
                {
                    if (result == 3)
                    {
                        return Json(new { success = true, message = "Commision Config deleted successfully.", result });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed to delete Commision Config." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete Commision Config." });
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
