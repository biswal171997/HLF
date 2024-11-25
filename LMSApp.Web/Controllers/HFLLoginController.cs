using ClientsideEncryption;
using CodeGen.Model.LoginModel;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Web.Controllers
{
    public class HFLLoginController : Controller
    { 
        public IConfiguration Configuration;
        private readonly ICodeGenLoginRepository _CodeGenLoginRepository;
        private readonly ISendSMSRepository _sms;
        public HFLLoginController(IConfiguration configuration, ICodeGenLoginRepository CodeGenLoginRepository, ISendSMSRepository sms)
        {
            Configuration = configuration;
            _CodeGenLoginRepository = CodeGenLoginRepository;
            _sms = sms;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> UserLogin(CodeGenLogin log)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join("|", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));
                    return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
                }
                else
                {
                    
                    //log.UserName = AESEncrytDecry.DecryptStringAES(log.UserName);
                    //log.Password = AESEncrytDecry.DecryptStringAES(log.Password);
                    var data = await _CodeGenLoginRepository.GetLoginDetails(log);
                    if (data != null)
                    {
                        HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        HttpContext.Session.SetString("UserName", data.VCHUSERNAME);
                        if (data.ROLEID != 0)
                        {
                            HttpContext.Session.SetInt32("roleId", (int)data.ROLEID);
                        }
                        if (data.INTDESIGNATIONID != 0)
                        {
                            HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        }
                        if (data.ROLENAME != null)
                        {
                            HttpContext.Session.SetString("RoleName", data.ROLENAME);
                        }
                        //HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        //HttpContext.Session.SetInt32("roleId", (int)data.ROLEID);
                        //HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        ////HttpContext.Session.SetString("RoleName", data.ROLENAME);
                        //HttpContext.Session.SetString("UserName", data.VCHUSERNAME);
                        //HttpContext.Session.SetString("FullName", data.VCHFULLNAME);

                        return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
                    }
                    else
                    {
                        var message = "Invalid UserId Or password";
                        return Json(new { sucess = false, responseMessage = message, responseText = "Login Failed !", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
