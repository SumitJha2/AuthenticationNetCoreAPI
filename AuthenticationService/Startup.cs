using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Authentication.DAL;
using Authentication.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AuthenticationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            #region DI Container
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<ITokenService, TokenService>();
            #endregion

            #region Getdata from json file
            var tokenParams = Configuration.GetSection("TokenSettings");
            services.Configure<TokenParametersModel>(tokenParams);
            var tokenmodl=tokenParams.Get<TokenParametersModel>();
            #endregion

            SetupJWTServices(services, tokenmodl);
            services.AddCors();
            services.AddMvc(options => options.Filters.Add(new GlobalExceptionLogger())); 
            services.AddControllers();
        }
        private void SetupJWTServices(IServiceCollection services, TokenParametersModel tokenParameters)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = tokenParameters.IssuerKey,
                  ValidAudience = tokenParameters.AudenceKey,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.SecretKey))
              };
              options.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = context =>
                  {
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          context.Response.Headers.Add("token-expired", "true");
                      }
                      return Task.CompletedTask;
                  }
              };
          });
        }       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
