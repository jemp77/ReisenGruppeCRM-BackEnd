using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartCardCRM.Data.Entities;
using System;
using System.IO;

namespace SmartCardCRM.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            AddSwagger(services);
            //services.AddMvc().AddJsonOptions(options => 
            //{ 
            //    options.JsonSerializerOptions.IgnoreNullValues = true; 
            //});
            services.AddDbContext<SmartCardCRMContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SmartCardCRM"), b => b.MigrationsAssembly("SmartCardCRM.Service")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            //appLifetime.ApplicationStarted.Register(() => { });
            //appLifetime.ApplicationStopping.Register(() => { });
            //appLifetime.ApplicationStopped.Register(() => { });

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                configurationBuilder = configurationBuilder.AddUserSecrets<Program>();
            }

            if (env.IsDevelopment() || env.IsEnvironment("LocalDev"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "SmartCardCRM API V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Documents", "QuoterAssets", "Images")),
                RequestPath = "/Images"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Documents", "QuoterAssets", "Videos")),
                RequestPath = "/Videos"
            });

            Configuration = configurationBuilder.Build();

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"SmartCardCRM API {groupName}",
                    Version = groupName,
                    Description = "SmartCardCRM API",
                    Contact = new OpenApiContact
                    {
                        Name = "SmartCardCRM",
                        Email = string.Empty,
                        //Url = new Uri("http://services.smartcardcrm.com/")
                        Url = new Uri("https://localhost:44308/")
                    }
                });
            });
        }
    }
}
