using System.Collections.Generic;
using LMSApp.Model.Entities.MASTERS;
using LMSApp.Model.ServiceProviderMaster;
using Microsoft.AspNetCore.Http;
namespace LMSApp.Repository.Repositories.Interfaces
{
    public interface IMASTERSRepository
    {
        // Service Provider-----------------------------------
        public Task<int> SaveServiceProvider(ServiceProviderMaster serviceProviderMaster, IFormFile logoFile);
        //Task<int> Insert_ServiceProviderMaster(ServiceProviderMaster TBL);
        Task<int> Delete_ServiceProviderMaster(int Id);
        Task<ServiceProviderMaster> GetServiceProviderMasterById(int Id);
        //Task<List<ServiceProviderMaster>> Search_ServiceProviderMaster(ServiceProviderMaster TBL);
        Task<List<ServiceProviderMaster>> Getall_ServiceProviderMaster(ServiceProviderMaster TBL);

        // Service Category-----------------------------------------------------------------------------------
        public Task<int> SaveServiceCategory(ServiceCategory category);
        Task<List<ServiceCategory>> Getall_ServiceCategory(ServiceCategory TBL);
        Task<ServiceCategory> GetServiceCategoryById(int Id);
        Task<int> Delete_ServiceCategory(int Id);
        //Task<int> Delete_SaveServiceCategory(int Id);

        // Service Sub Category-----------------------------------------------------
        public Task<int> SaveServiceSubCategory(ServiceSubCategory subcategory);
        Task<List<ServiceSubCategory>> Getall_ServiceSubCategory(ServiceSubCategory TBL);
        Task<ServiceSubCategory> Getall_ServiceSubCategoryById(int Id);
        Task<int> Delete_ServiceSubCategoryById(int Id);

        // Commision Config----------------------------------
        public Task<int> SaveCommisionConfig(CommisionConfigMaster commision);
        Task<List<CommisionConfigMaster>> Getall_CommisionConfig(CommisionConfigMaster TBL);
        Task<CommisionConfigMaster> Getall_CommisionConfigById(int Id);
        Task<int> Delete_CommisionConfigById(int Id);
    }

    //public interface IMASTERSRepository
    //{
    //    public Task<int> SaveServiceProvider(ServiceProviderMaster serviceProviderMaster, IFormFile logoFile);
    //    //Task<int> Insert_ServiceProviderMaster(ServiceProviderMaster TBL);
    //    Task<int> Delete_ServiceProviderMaster(int Id);
    //    Task<ServiceProviderMaster> GetServiceProviderMasterById(int Id);
    //    //Task<List<ServiceProviderMaster>> Search_ServiceProviderMaster(ServiceProviderMaster TBL);
    //    Task<List<ServiceProviderMaster>> Getall_ServiceProviderMaster(ServiceProviderMaster TBL);
    //    //-----------------------------------------------------------------------------------


    //    public Task<int> SaveServiceCategory(ServiceCategory category);
    //    Task<List<ServiceCategory>> Getall_ServiceCategory(ServiceCategory TBL);
    //    Task<ServiceCategory> GetServiceCategoryById(int Id);
    //    Task<int> Delete_SaveServiceCategory(int Id);

    //    //-----------------------------------------------------
    //    //-- Service Sub Category
    //    public Task<int> SaveServiceSubCategory(ServiceSubCategory subcategory);
    //    Task<List<ServiceSubCategory>> Getall_ServiceCategory(ServiceSubCategory TBL);
    //    //----------------------------------
    //    public Task<int> SaveCommisionConfig(CommisionConfigMaster commision);
    //    Task<List<CommisionConfigMaster>> Getall_CommisionConfig(CommisionConfigMaster TBL);

    //}
}
