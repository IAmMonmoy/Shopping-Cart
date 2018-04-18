using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shopping_Cart_Api.Services;

namespace Shopping_Cart
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //Add identity
            services.AddIdentity<ApplicationUser,IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            
            services.AddTransient<ITagService,TagService>();
            services.AddTransient<IProductService,ProductService>();
            services.AddTransient<IProductTagService,ProductTagService>();

            services.AddCors(options => 
            {
                options.AddPolicy("AllowSpecificOrigin",
                                    builder => builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader().AllowAnyMethod());
            });

            //token validation parameters
           var tokenValidationParameters = new TokenValidationParameters 
           {
               ValidateIssuer = true,
               ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],

               ValidateAudience = true,
               ValidAudience = Configuration["JwtIssuerOptions:Audience"],

               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtIssuerOptions:Key"])),

               RequireExpirationTime = false,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
            
            //add jwt bearer token autentication
           services.AddAuthentication(options => 
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer( option => 
           {
               option.ClaimsIssuer = Configuration["JwtIssuerOptions:Issuer"];
               option.TokenValidationParameters = tokenValidationParameters;
               option.SaveToken = true;
           });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //create roles needed for application

            EnsureRolesAsync(roleManager).Wait();

            //Create an account and make it administrator
            AssignAdminRole(userManager).Wait();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();

            app.UseMvc();
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExistsAdmin = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            var alreadyExistsSimpleUser = await roleManager.RoleExistsAsync(Constants.SimpleUser);

            if(alreadyExistsAdmin) return; 
            else await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));

            if(alreadyExistsSimpleUser) return;
            else await roleManager.CreateAsync(new IdentityRole(Constants.SimpleUser));
        }

        public static async Task AssignAdminRole(UserManager<ApplicationUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "IAmMonmoy").SingleOrDefaultAsync();
            if(testAdmin == null)
            {
                testAdmin = new ApplicationUser
                {
                    UserName = "IAmMonmoy",
                    Email = "iammonmoy@gmail.com"
                };

                await userManager.CreateAsync(testAdmin,"512345Rrm_");
            }
            else
            {
               var isAdmin = await userManager.IsInRoleAsync(testAdmin,Constants.AdministratorRole);
               if(!isAdmin) await userManager.AddToRoleAsync(testAdmin,Constants.AdministratorRole);
            }
        }
    }
}
