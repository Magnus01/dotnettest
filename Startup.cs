using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Manager.Services;

namespace Manager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // Add framework services.
            services.AddMvc();


            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=UserManagementDB;Trusted_Connection=True;";
            //var connectionString = Startup.Configuration["connectionString:cityInfoDBConnectionString"];
            services.AddDbContext<UserInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IUserInfoRepository, UserInfoRepository>();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                //options.Authority = domain;
                options.Authority = "https://diggit.eu.auth0.com/";
                options.Audience = "testApi";
                //options.Audience = Configuration["Auth0:ApiIdentifier"];

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
                        // Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
                        // user's information from the /userinfo endpoint
                        if (context.SecurityToken is JwtSecurityToken token)
                        {
                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("access_token", token.RawData));
                            }
                        }

                        return Task.FromResult(0);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserInfoContext userInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseStaticFiles();

            app.UseAuthentication();

            //userInfoContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<UserInfo.Entities.User, UserInfo.Models.UserWithoutTeachersDto>();
                //cfg.CreateMap<Manager.Entities.User, Manager.Models.UserDtoCreation>();

                cfg.CreateMap<Entities.User, Models.UserDtoCreation>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Description, opt => opt.Ignore())
                //.ForMember(dest => dest.LearnerDetails, opt => opt.Ignore());
                //.ForMember(dest => dest.EducatorDetails, opt => opt.Ignore());
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


                //cfg.CreateMap<UserInfo.Models.UsersForCreationDto, UserInfo.Entities.User>();
                //cfg.CreateMap<UserInfo.Models.UsersForUpdateDto, UserInfo.Entities.User>();
                //cfg.CreateMap<UserInfo.Entities.User, UserInfo.Models.UsersForUpdateDto>();

                //cfg.CreateMap<UserInfo.Entities.Institution, UserInfo.Models.InstitutionsDto>();

                cfg.CreateMap<Manager.Entities.EducatorDetail, Manager.Models.EducatorDetailsDto>();
                cfg.CreateMap<Manager.Entities.LearnerDetail, Manager.Models.LearnerDetailsDto>();
                //cfg.CreateMap<UserInfo.Models.TeachersForCreationDto, UserInfo.Entities.Teacher>();
                //cfg.CreateMap<UserInfo.Models.TeachersForUpdateDto, UserInfo.Entities.Teacher>();
                //cfg.CreateMap<UserInfo.Entities.Teacher, UserInfo.Models.TeachersForUpdateDto>();



                //cfg.CreateMap<UserInfo.Entities.TAssignment, UserInfo.Models.TAssignmentsDto>();
                //cfg.CreateMap<UserInfo.Models.TAssignmentsForCreationDto, UserInfo.Entities.TAssignment>();
                //cfg.CreateMap<UserInfo.Models.TAssignmentsForUpdateDto, UserInfo.Entities.TAssignment>();
                //cfg.CreateMap<UserInfo.Entities.TAssignment, UserInfo.Models.TAssignmentsForUpdateDto>();


                cfg.CreateMap<Manager.Entities.Classroom, Manager.Models.ClassroomDto>();
                //cfg.CreateMap<UserInfo.Models.ClassroomsForCreationDto, UserInfo.Entities.Classroom>();
                //cfg.CreateMap<UserInfo.Models.ClassroomsForUpdateDto, UserInfo.Entities.Classroom>();
                //cfg.CreateMap<UserInfo.Entities.Classroom, UserInfo.Models.ClassroomsForUpdateDto>();


                //cfg.CreateMap<Entities.User, Models.LearnerDetailsDto>()
                //        .ForMember(dest => dest.StudentNo, opt => opt.MapFrom(src => src.LearnerDetails.StudentNo));


                //cfg.CreateMap<UserInfo.Entities.Student, UserInfo.Models.StudentsDto>();
                //cfg.CreateMap<UserInfo.Models.StudentsForCreationDto, UserInfo.Entities.Student>();
                //cfg.CreateMap<UserInfo.Models.StudentsForUpdateDto, UserInfo.Entities.Student>();
                //cfg.CreateMap<UserInfo.Entities.Student, UserInfo.Models.StudentsForUpdateDto>();

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
