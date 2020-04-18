using System.Net;
using System.Text;
using AutoMapper;
using DOT_NET_CORE_WEBAPI_SQLITE.Data;
using DOT_NET_CORE_WEBAPI_SQLITE.Helpers;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.auth;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.product;
using DOT_NET_CORE_WEBAPI_SQLITE.Repository.user;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DOT_NET_CORE_WEBAPI_SQLITE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This is dependency injection container
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding ConnectionString
            services.AddDbContext<AppDataContext>(conn => conn.UseSqlite(Configuration.GetConnectionString("DevConnection")));
            services.AddControllers().AddNewtonsoftJson(option => { // Serialize output and return json response using AddNewtonSoft Json
                // This will prevent looping error, which we might get while referencing data to other table.
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }); 

            // Add CORS
            services.AddCors();

            // Register Cloudinary Service for Uploading Photos
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            //Register service for Auto Mapper
            services.AddAutoMapper(typeof(UserRepository).Assembly);

            // Register New Controller.
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Add JWT Authentication Service
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option => {
                    option.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration.GetSection("Appsettings:TokenSecret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false                        
                    };
                });
        }

        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else {
                // Use global response handler
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => { // this is application response context
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if(error != null){
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            // This is use to redirect any http request to https
            // app.UseHttpsRedirection();

            app.UseRouting();

            // Must use authentication before authorization. A request must be authenticated
            // before it can be authorized.
            app.UseAuthentication();
            app.UseAuthorization();            

            // Setting up application request here.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
