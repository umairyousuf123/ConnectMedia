using ConnectMedia.BackgroundServices;
using ConnectMedia.BackgroundServices.Services.Implementation;
using ConnectMedia.BackgroundServices.Services.Interface;
using ConnectMedia.Common.Database;
using ConnectMedia.Common.DTO;
using ConnectMedia.Common.Helper;
using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Repository;
using ConnectMedia.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConnectMedia
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddRazorPages();


            services.AddControllersWithViews();
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            #region   AppsSetting
            services.Configure<DocumentPath>(Configuration.GetSection("DocumentPath"));
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            services.Configure<NexmoDTO>(Configuration.GetSection("Nexmo"));
            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));


            //add database context 
            services.AddDbContext<ConnectMediaDB>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnectMediaLiveDb"),
            providerOptions => providerOptions.EnableRetryOnFailure()
              ));
            //end
            #endregion



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication("CookieAuthentication")
      .AddCookie("CookieAuthentication", config =>
      {
          config.Cookie.Name = "UserLoginCookie";
          config.LoginPath = "/Authentication/Login";
      });

            services.AddControllersWithViews();
         //   services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         //.AddJwtBearer(options =>
         //{
         //    options.TokenValidationParameters = new TokenValidationParameters
         //    {
         //        ValidateIssuer = true,
         //        ValidateAudience = true,
         //        ValidateLifetime = true,
         //        ValidateIssuerSigningKey = true,
         //        RequireExpirationTime = true,
         //        ValidIssuer = Configuration.GetSection("AppSettings")["Isuser"].ToString(),
         //        ValidAudience = Configuration.GetSection("AppSettings")["Audience"].ToString(),
         //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings")["JWTSecurityKey"].ToString()))
         //    };
         //});


            #region   Service Interface and Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IClassifiedService, ClassifiedService>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<ITutorialService, TutorialService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAndroidService, AndroidService>();
            services.AddScoped<ISendSMS, SendSMS>();
            services.AddScoped<IVideosService, VideosService>();
            #endregion

            #region Repo Interface and Repo
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IClassifiedRepository, ClassifiedRepository>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<ITutorialRepository, TutorialRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAndroidRepository, AndroidRepository>();
            services.AddScoped<IVideosRepository, VideosRepository>();
            #endregion
            services.AddScoped<IBackgroundExecutionService, BackgroundExecutionService>();

            services.UseQuartz(typeof(PlayListActivateJob));
            services.UseQuartz(typeof(PlayListDeActivateJob));
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // who are you?
            app.UseAuthentication();

            // are you allowed?
            app.UseAuthorization();
            var sch1 = app.ApplicationServices.GetService<IScheduler>();
            QuartzServicesUtilities.StartJob<PlayListActivateJob>(sch1);
            var sch2 = app.ApplicationServices.GetService<IScheduler>();
            QuartzServicesUtilities.StartJob<PlayListDeActivateJob>(sch2);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=Login}/{id?}");
            });
        }
    }
}
