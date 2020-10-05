using System.Collections.Generic;
using Vcpmc.Mis.Application.Catalog.Products;
using Vcpmc.Mis.Application.Common;
using Vcpmc.Mis.Application.System.Languages;
using Vcpmc.Mis.Application.System.Roles;
using Vcpmc.Mis.Application.System.Users;
using Vcpmc.Mis.Utilities.Constants;
using Vcpmc.Mis.ViewModels.System.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Vcpmc.Mis.Data.Entities;
using Vcpmc.Mis.Data.EF;
using Vcpmc.Mis.Application.Media.Youtube;
using Vcpmc.Mis.Data.Entities.Mongo;
using Microsoft.Extensions.Options;
using AutoMapper;
using Vcpmc.Mis.Data.Entities.Media.Youtube;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.Application.map;
using Vcpmc.Mis.Application.Mis.Works;
using Vcpmc.Mis.Application.Mis.Works.Tracking;
using Vcpmc.Mis.Application.Mis.Monopolys;
using Vcpmc.Mis.Application.MasterLists;
using Vcpmc.Mis.Application.Mis.Members;
using Vcpmc.Mis.Application.System.Para;
using Vcpmc.Mis.Application.Mis.WorkHistorys;

namespace Vcpmc.Mis.BackendApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region mongo
            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<PreclaimService>();
            services.AddSingleton<WorkService>();
            services.AddSingleton<WorkHistoryService>();
            services.AddSingleton<WorkTrackingService>();
            services.AddSingleton<MonopolyService>();
            services.AddSingleton<UserService2>();
            services.AddSingleton<RoleService2>();
            services.AddSingleton<AppClaimService>();
            services.AddSingleton<MasterListService>();
            services.AddSingleton<MemberService>();
            services.AddSingleton<FixParameterService>();
            #endregion

            #region SQL           
            services.AddDbContext<EShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<EShopDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Declare DI
            services.AddTransient<IStorageService, FileStorageService>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            //warehouse
            services.AddTransient<IPreclaimService, PreclaimService>();
            services.AddTransient<IWorkService, WorkService>();
            services.AddTransient<IWorkHistoryService, WorkHistoryService>();
            services.AddTransient<IWorkTrackingService, WorkTrackingService>();
            services.AddTransient<IMonopolyService, MonopolyService>();
            services.AddTransient<IUserService2, UserService2>();
            services.AddTransient<IRoleService2, RoleService2>();
            services.AddTransient<IAppClaimService, AppClaimService>();
            services.AddTransient<IMasterListService, MasterListService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IFixParameterService, FixParameterService>();
            //services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            //services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
            #endregion

            #region Auto mapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddControllers()
                //Add them json
                .AddNewtonsoftJson(options => options.UseMemberCasing())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

            #region Swagger            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });
            #endregion

            #region bearer
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
            #endregion
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

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSolution V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}