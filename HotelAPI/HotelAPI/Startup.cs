using HotelAPI.Filters;
using HotelAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Services;

namespace HotelAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("HotelApiCorsPolicy", policy =>
                    policy.AllowAnyOrigin()); //Reconfigure this for prod. #todo
            });

            services.Configure<HotelInfo>(
                Configuration.GetSection("HotelInfo"));

            services.AddScoped<IRoomService, DefaultRoomsService>();
            
            services.AddDbContext<HotelApiDBContext>(options => {
                //Dev Config for dbcontext
                options.UseInMemoryDatabase("hotelDb");
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
            });
            services.AddControllers();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });
            app.UseRouting();
            app.UseCors("HotelApiCorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
