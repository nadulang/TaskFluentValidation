using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using TaskMediatrFluentValidation.Entity;
using TaskMediatrFluentValidation.Validator;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace TaskMediatrFluentValidation
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
            services.AddDbContext<TheWholeContext>(options => options.UseNpgsql("Host=127.0.0.1;Database=onlineshopdb;Username=postgres;Password=docker"));
            services.AddControllers();
            services.AddMvc()
                    .AddFluentValidation(); 

            services.AddTransient<IValidator<Customer>, CustomerValidator>()
                    .AddTransient<IValidator<CustomerPaymentCards>, CustomerPaymentValidator>()
                    .AddTransient<IValidator<Merchant>, MerchantValidator>()
                    .AddTransient<IValidator<Products>, ProductsValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidator<,>));
            services.AddAuthentication( options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.RequireHttpsMetadata = false;
                option.SaveToken = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secretnya kurang panjaaaaaangggggg banget")),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
