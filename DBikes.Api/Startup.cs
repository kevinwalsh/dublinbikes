using DBikes.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SKELETONPROJECT.CoreApi.SettingsOptions;

namespace SKELETONPROJECT.CoreApi
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
                options.AddPolicy("DBikesPolicy", builder =>
                {
                    builder
                        //.AllowAnyOrigin()
                        .WithOrigins("https://localhost:44311")
                        .WithHeaders("Authorization")
                    ;
                });
            });

            services.AddControllersWithViews();
            //KW 
            services.AddSwaggerGen();
            services.AddSingleton<DBikesMemoryCache>();
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

            app.UseCors("DBikesPolicy");            // Ordering is important!   (a) Routing (b) Cors (c) Authorization (d) Endpoints
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DublinBikesStatic}/{action=GetCities}/{id?}")
         //       .RequireCors("DBikesPolicy)
                ;
            });

            //KW
            var swagger_openapiversion = Configuration.GetValue<int>("mysettings:SwaggerOpenApiVersion");      // appsettings, Option 1
            var mysettings = new MySettingsOptions();                           // appsettings, Option 2
            Configuration.GetSection(StaticData.MySettings).Bind(mysettings);
            app.UseSwagger();

            if (mysettings.SwaggerOpenApiVersion == 2)              // e.g configuring startup settings based on appsettings
            {
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;      // default v3 openAPI, but 2.0 may be needed for Powerapps/ Flow
                });
            }
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    mysettings.MyApiVersion != null ? mysettings.MyApiVersion : "my API v1"
                    );
            });

        }
    }
}
