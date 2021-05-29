using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using TrainingService.DBRepository;
using Microsoft.EntityFrameworkCore;
using TrainingService.Models;
using Microsoft.AspNetCore.Identity;

namespace TrainingService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TrainingServiceContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>(options => {
                //options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 5;   // минимальная длина
                options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                options.Password.RequireDigit = false; // требуются ли цифры
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz@.1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // допустимые символы
            }).AddEntityFrameworkStores<TrainingServiceContext>();

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration.GetConnectionString("GoogleClientId");
                options.ClientSecret = Configuration.GetConnectionString("GoogleClientSecret");
            }).AddVkontakte(options =>
            {
                options.ClientId = Configuration.GetConnectionString("VKontakteClientId");
                options.ClientSecret = Configuration.GetConnectionString("VKontakteClientSecret");
                options.Scope.Add("email");
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddControllersWithViews();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });           
        }
    }
}
