using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using LMSApp.Repository.Factory;
using LMSApp.Repository.Repository;
using LMSApp.Repository.Repositories.Interfaces;
using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Repository.Repository;
using LMSApp.Repository.Repositories.Repository;
namespace LMSApp.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		IMyAppConnectionFactory MyAppconnectionFactory=new MyAppConnectionFactory(configuration.GetConnectionString("DBconnectionMyApp"));
		services.AddSingleton<IMyAppConnectionFactory> (MyAppconnectionFactory);

		services.AddScoped<IMASTERSRepository, MASTERSRepository>();
		services.AddScoped<ICodeGenLoginRepository, CodeGenLoginRepository>();
		services.AddScoped<ISendSMSRepository, SendSMSRepository>();
            services.AddScoped<ILoanLead, LoanLeads>();
            //Write code here
        }
	}
}
