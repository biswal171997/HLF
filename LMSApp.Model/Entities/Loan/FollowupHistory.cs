using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Model.Entities.Loan
{
    public class FollowupHistory
    {
        public string ActionCode { get; set; } = "";
        public int LeadID { get; set; } = 0;
        public string? FollowupDate { get; set; } 
        public string? NextFallowUpDate { get; set; }
        
        public string Remarks { get; set; } = "";
        public int CreatedBy { get; set; } = 0;
        public string CustomerName { get; set; } = "";
        public string FallowUpBy { get; set; } = "";
    }
}
