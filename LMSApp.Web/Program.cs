using LMSApp.Repository.Container;
using ExceptionHandling.Middlewares;   
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCustomContainer(builder.Configuration);
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.Name = ".Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


 app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=CodeGenLogin}/{action=UserLogin}/{id?}");
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{controller=MASTERS}/{action=ServiceProviderMaster}/{id?}");
pattern: "{controller=HFLLogin}/{action=SignIn}/{id?}");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();
