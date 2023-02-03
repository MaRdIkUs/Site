using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Mocks;
using WebApplication5.Data.Serializers;

namespace WebApplication5
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public static string newsDirectory = "ProgramData\\News\\";
        public static string userDataDirectory = "ProgramData\\UserData\\";
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tokar\\source\\repos\\WebApplication5\\WebApplication5\\DB\\UserData.mdf;Integrated Security=True";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/Account/Login/";
                    options.AccessDeniedPath = "/Home/Index/";
                });
            services.AddAuthorization();
            services.AddTransient<IUserData,UserSerializer>();
            services.AddTransient<IPage, PagesSerializer>();
            services.AddTransient<IRoles, MockRoles>();
            services.AddTransient<ITitle, MockTitle>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
