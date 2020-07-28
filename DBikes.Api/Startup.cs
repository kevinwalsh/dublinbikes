using DBikes.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DBikes.CoreApi.SettingsOptions;

namespace DBikes.CoreApi
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
            var dbikesSettings = new MySettingsOptions();
            Configuration.GetSection(StaticData.DBikesSettings).Bind(dbikesSettings);
            services.AddCors(options =>
            {
                options.AddPolicy("DBikesPolicy", builder =>
                {
                    builder
                       .WithOrigins(dbikesSettings.AllowedUrls)
                        .WithHeaders("Authorization")
                    ;
                });
            });

            services.AddControllersWithViews();
            //KW 
            services.AddSwaggerGen();
            services.AddSingleton(new DBikesMemoryCache(dbikesSettings.DefaultCacheLifetime));
            services.AddSingleton(dbikesSettings);
            services.AddSingleton(new DBikes.Api.Helpers.HTTPClient.DublinBikesHTTPClientHelper(dbikesSettings.DefaultCity));
           
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
            var mysettings = new MySettingsOptions();                           // appsettings, Option 2
            Configuration.GetSection(StaticData.DBikesSettings).Bind(mysettings);
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
