using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Model.Entities.Loan
{
    public class LoanLeadDetails
    {
        public int LeadID { get; set; } = 0;        // Assuming LeadID is an integer
        public string LeadCode { get; set; } = "";   // Assuming LeadCode is a string
        public string Name { get; set; } = "";        // Assuming Name is a string
        public string Mobile { get; set; } = "";       // Assuming Mobile is a string
        public string Whatsup { get; set; } = "";      // Assuming Whatsup is a string
        public string Email { get; set; } = "";         // Assuming Email is a string
        public int StateID { get; set; } = 0;         // Assuming StateID is an integer
        public int DistrictId { get; set; } = 0;      // Assuming DistrictId is an integer
        public string City { get; set; } = "";        // Assuming City is a string
        public string ReferenceBy { get; set; } = "";   // Assuming ReferenceBy is a string
        public string LGLC_Code { get; set; } = "";    // Assuming LGLC_Code is a string
        public int StatusID { get; set; } = 1;       // Assuming StatusID is an integer
        public string Remarks { get; set; } = "";       // Assuming Remarks is a string
        public string NextFollowupDate { get; set; } // Nullable DateTime for NextFollowupDate
        public DateTime CreateOn { get; set; }   // Assuming CreateOn is a DateTime
        public int CreatedBy { get; set; } = 0;   // Assuming CreatedBy is a string
        public DateTime? UpdatedOn { get; set; } // Nullable DateTime for UpdatedOn
        public int UpdatedBy { get; set; } = 0;    // Assuming UpdatedBy is a string
        public DateTime? DeletedOn { get; set; } // Nullable DateTime for DeletedOn
        public int DeletedBy { get; set; } = 0;   // Assuming DeletedBy is a string
        public int IsDeleted { get; set; } = 0;      // Assuming IsDeleted is a boolean
        public int IsActive { get; set; } = 0;    // Assuming IsActive is a boolean
        public string StateName { get; set; } = "";
        public decimal RequestAmount { get; set; } = 0;
        //--Profession
        public int ProfessionID { get; set; } = 0;
        public string ProfessionName { get; set; } = "";
        //--District
        public int DistrictID { get; set; } = 0;
        public string DistrictName { get; set; } = "";
        //--Category Type (Product Type)
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = "";
        public int SubCategoryID { get; set; } = 0;
        public string SubCategoryName { get; set; } = "";
        public string StatusName { get; set; } = "";
        //--Sub Category

    }
    public class ProfessionEntity
    {
        //public int ProfessionID { get; set; } = 0;
        //public string ProfessionName { get; set; } = "";
    }
    public class DistrictEntity
    {
        //public int DistrictID { get; set; } = 0;
        //public string DistrictName { get; set; } = "";
    }


}
