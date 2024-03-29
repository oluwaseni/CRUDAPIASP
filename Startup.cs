﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using OnMyOwn.Model;

namespace OnMyOwn
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)

            //The following is to make sure that VS doesn't change the first letter of the column name to small letter
            .AddJsonOptions(options =>
            {
            var resolver = options.SerializerSettings.ContractResolver;
            if (resolver != null)
            {
                    (resolver as DefaultContractResolver).NamingStrategy = null; 
                }
            });

            services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("EmployeePayment")));

            //services.AddDbContextPool<AppDbContext>(options => 
            //options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
