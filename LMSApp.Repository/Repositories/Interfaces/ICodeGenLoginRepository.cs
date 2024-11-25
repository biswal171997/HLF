using System.Data;
using CodeGen.Model.LoginModel;
using CodeGen.Model.forgetpassmodel;
namespace CodeGen.Repository.Repositories.Interfaces
{
     public interface ICodeGenLoginRepository
    {
        /// <summary>
        /// Gets the get connection.
        /// </summary>
       
	   Task<CodeGenLogin> GetLoginDetails (CodeGenLogin Lgm);
       Task<int> ChangePwd(CodeGenLogin Upd );

       Task<IEnumerable<forgetpassmodel>> GetLoginDetails(forgetpassmodel logins);
       Task<int> insertotp(forgetpassmodel TBL);
       Task<forgetpassmodel> Otpcheck(forgetpassmodel TBL);
       Task<int> NewPassword(forgetpassmodel TBL);
      Task<IEnumerable<CodeGenLogin>> validateuser(CodeGenLogin logins);

    }
}
