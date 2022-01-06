using Administration;
using Administration.Interfaces;
using Administration.Jwt;
using Administration.Services;
using AutoMapper;
using BLL;
using BLL.AddModels;
using BLL.Interfaces;
using BLL.Services;
using BLL.VievModels;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Respositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;

namespace CardIndex
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)

            .CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperProfile());
            });

            services.AddMvcCore().AddApiExplorer();

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            ConfigureSertviceMethods.ServicesDI(services);
            ConfigureSertviceMethods.RepositoryDI(services);
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddDbContext<ICardContext, CardDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CardDB")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            ConfigureSertviceMethods.AdministrationDI(services, Configuration);

        }

        private static class ConfigureSertviceMethods
        {
            public static void ServicesDI(IServiceCollection services)
            {
                services.AddScoped<IRepository<Theme>, ThemeRepository>();
                services.AddScoped<IRepository<Article>, ArticleRepository>();
                services.AddScoped<IBaseRepository<ArticleRate>, ArticleRateRepository>();
            }

            public static void RepositoryDI(IServiceCollection services)
            {
                services.AddScoped<IArticleService, ArticleService>();
                services.AddScoped<IBaseService<ThemeAddModel, ThemeVievModel>, ThemeService>();
                services.AddScoped<IBaseService<ArticleRateAddModel, ArticleRateVievModel>, ArticleRateService>();
                services.AddScoped<IUserService, UserService>();
            }

            public static void AdministrationDI(IServiceCollection services, IConfiguration Configuration)
            {
                services.AddDbContext<AdministrationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AdministrationDB")));

                services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AdministrationDbContext>();

                services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

                services.AddControllersWithViews().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

                var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
                services
                    .AddAuthorization()
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }



            app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                    //name: "default",
                    //pattern: "{controller}/{action=Index}/{id?}");
            });



            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
