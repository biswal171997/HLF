using System;
using LMSApp.Model.BaseEntityModel;
namespace LMSApp.Model.ServiceProviderMaster
{
    public class ServiceProviderMaster
    {
        //public int Id { get; set; }
        //public string? ServiceProviderName { get; set; }
        //public string? Logo { get; set; }

        public int? Id { get; set; } = 0;
        public string ProviderName { get; set; } = "";
        public int UserID { get; set; } = 0;
        public string Logo { get; set; } = "";
        public int IsDeleted { get; set; } = 0;
        public int IsActive { get; set; } = 0;
        public int LeadID { get; set; } = 0;
    }
}

