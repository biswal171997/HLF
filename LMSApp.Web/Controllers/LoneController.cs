using LMSApp.Model.Entities.Loan;
using LMSApp.Model.ServiceProviderMaster;
using LMSApp.Repository.Repositories.Interfaces;
using LMSApp.Repository.Repositories.Repository;
using LMSApp.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMSApp.Web.Controllers
{
    public class LoneController : Controller
    {
        public IConfiguration Configuration;
        private readonly ILoanLead _LoanLead;
        private IWebHostEnvironment _hostingEnvironment;
        public LoneController(IConfiguration configuration, ILoanLead loanLead, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _LoanLead = loanLead;

            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Opportunity()
        {
            return View();
        }
        public IActionResult Lead()
        {
            ViewBag.ResultProfession = _LoanLead.ViewProfession().Result;
            ViewBag.ResultDistrict = _LoanLead.ViewDistrict().Result;
            ViewBag.ResultProductCategory = _LoanLead.ViewProductCategory().Result;
            return View();
        }
        public IActionResult Applied()
        {
            return View();
        }
        public IActionResult Sanctioned()
        {
            return View();
        }
        public IActionResult Disbrusment()
        {
            return View();
        }
        public IActionResult Rejected()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeads()
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
                List<LoanLeadDetails> lst = await _LoanLead.ViewLoanLeads(new LoanLeadDetails());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpPost]
        public IActionResult SaveLoanLeads([FromForm] LoanLeadDetails loanLeadDetails)
        {


            // Save the service provider information to the database
            var data = _LoanLead.SaveLoanLeads(loanLeadDetails);

            return Json(new { success = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
        }
        [HttpPost]
        public IActionResult UpdateLoanLead([FromForm] LoanLeadDetails loanLeadDetails)
        {


            // Save the service provider information to the database
            var data = _LoanLead.UpdateLoanLeads(loanLeadDetails);

            return Json(new { success = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });
        }
        [HttpGet]
        public IActionResult ViewLoanLeadsById(int LeadId)
        {
            var LoanLeadsById = _LoanLead.ViewLoanLeadsById(Convert.ToInt32(LeadId)).Result;
            return Ok(JsonConvert.SerializeObject(LoanLeadsById));
        }

        [HttpGet]
        public JsonResult ViewProductSubCategory(int categoryId)
        {
            var subCategories = _LoanLead.ViewProductSubCategory(categoryId).Result;
            return Json(subCategories);
        }

        #region FollowUp History
        [HttpPost]
        public IActionResult SaveFollowupHistory([FromForm] FollowupHistory followup)
        {

            var data = _LoanLead.SaveFollowupHistory(followup);

            return Json(new { success = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
        }

        public async Task<JsonResult> ViewFollowupHistory(int LeadID)
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
                FollowupHistory followupHistory = new FollowupHistory();
                followupHistory.LeadID = LeadID;
                List<FollowupHistory> lst = await _LoanLead.ViewFollowupHistory(followupHistory);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);

            }
        }

        [HttpPost]
        public IActionResult SaveApplied([FromForm] ApplicationAppliedDetailViewModel applicationAppliedDetailView)
        {

            var data = _LoanLead.SaveApplied(applicationAppliedDetailView);

            return Json(new { success = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
        }
        public async Task<JsonResult> ViewApliedDetails(int LeadID)
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

                List<ApplicationAppliedDetailViewModel> lst = await _LoanLead.ViewApliedDetails(LeadID);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);

            }
        }

        [HttpPost]
        public IActionResult LeadConvertStatusUpdate(int LeadID)
        {

            var data = _LoanLead.LeadConvertStatusUpdate(LeadID);

            return Json(new { success = true, responseMessage = "Lead Converted Successfully.", responseText = "Success", data = data });
        }

        [HttpGet]
        public async Task<JsonResult> ViewServiceProviderByLeadId(int LeadID)
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
                //LeadID = 5;
                List<ServiceProviderMaster> lst = await _LoanLead.ViewServiceProviderByLeadId(LeadID);
                var result = JsonConvert.SerializeObject(lst);
                return Json(result);
            }
        }

        //[HttpGet]
        //public JsonResult ViewServiceProviderByLeadId1(int LeadID)
        //{
        //    var serviceProvider = _LoanLead.ViewServiceProviderByLeadId(LeadID).Result;
        //    return Json(serviceProvider);
        //}
        #endregion

    }
}
