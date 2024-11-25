using LMSApp.Model.Entities.Loan;
using LMSApp.Model.Entities.MASTERS;
using LMSApp.Model.ServiceProviderMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Repository.Repositories.Interfaces
{
    public interface ILoanLead
    {
        Task<List<LoanLeadDetails>> ViewLoanLeads(LoanLeadDetails loanLeadDetails);
        Task<LoanLeadDetails> ViewLoanLeadsById(int Id);
        Task<int> SaveLoanLeads(LoanLeadDetails loanLeadDetails);
        Task<int> UpdateLoanLeads(LoanLeadDetails loanLeadDetails);
        Task<List<LoanLeadDetails>> ViewProfession();      //Profession
        Task<List<LoanLeadDetails>> ViewDistrict();        //District
        Task<List<LoanLeadDetails>> ViewProductCategory(); //ProductCategpry
        Task<List<ServiceSubCategory>> ViewProductSubCategory(int Id); //ProductSubCategpry
        //---------------------------------------------------------------------------------
        //--FollowUp History
        Task<int> SaveFollowupHistory(FollowupHistory followUp);
        Task<List<FollowupHistory>> ViewFollowupHistory(FollowupHistory followUp);
        Task<int> SaveApplied(ApplicationAppliedDetailViewModel applicationAppliedDetail);
        Task<int> UpdateApplied(ApplicationAppliedDetailViewModel applicationAppliedDetail);
        Task<int> DeleteApplied(int AppliedID);
        Task<List<ApplicationAppliedDetailViewModel>> ViewApliedDetails(int AppliedID);
        Task<int> LeadConvertStatusUpdate(int LeadID);
        //------------------------------------------------------------------
        //--Lead Conversion.
        Task<List<ServiceProviderMaster>> ViewServiceProviderByLeadId(int LeadID);
        
    }
}
