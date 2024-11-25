using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Model.Entities.MASTERS
{
    public class CommisionConfigMaster
    {
        public int? CommisionConfigId { get; set; } = 0;
        public int? ServiceProviderID { get; set; } = 0;
        public int? CategoryID { get; set; } = 0;
        public int? SubCategoryID { get; set; } = 0;
        public string? CategoryName { get; set; } = "";
        public string? SubCategoryName { get; set; } = "";
        public string? CommisionType { get; set; } = "";
        public decimal CommissionAmount { get; set; } = 0;
        public int? CreatedBy { get; set; } = 0;
        public string? ServiceProviderName { get; set; } = "";
        //CommisionConfigId	ServiceProviderID	ServiceProviderName	CategoryID	CategoryName	
        //SubCategoryID	SubCategoryName	CommisionType	CommissionAmount	CreateOn	CreatedBy	Logo

    }
    public class ProviderCategory
    {
        public int? Id { get; set; } = 0;
        public string ProviderName { get; set; } = "";
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = "";
    }

}
