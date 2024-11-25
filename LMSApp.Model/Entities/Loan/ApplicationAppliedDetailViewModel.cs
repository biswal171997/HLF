using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Model.Entities.Loan
{
    public class ApplicationAppliedDetailViewModel
    {
        // Application Applied Details
        public int AppliedID { get; set; } = 0;
        public int LeadID { get; set; } = 0;
        public int ServiceProviderID { get; set; } = 0;
        public string AppliedDate { get; set; } = "";
        public decimal AppliedAmount { get; set; } = 0;
        public decimal ApprovedAmount { get; set; } = 0;
        public string ApprovedDate { get; set; } = "";
        public int NoOfInstallment { get; set; } = 0;
        public string Status { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string CreateOn { get; set; } = "";
        public int CreatedBy { get; set; } = 0;
        public string UpdatedOn { get; set; } = "";
        public int UpdatedBy { get; set; } = 0;
        public string DeletedOn { get; set; } = "";
        public int DeletedBy { get; set; } = 0;
        public int IsDeleted { get; set; } = 0;

        // Service Provider Details (from ServiceProviderMaster table)
        public string ServiceProviderName { get; set; } = "";

        // Lead Details (from LeadMaster table)
        public string LeadName { get; set; } = "";    
        public string Mobile { get; set; } = "";
    }

}
