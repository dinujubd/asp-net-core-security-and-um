// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.MongoDB.Interfaces;
using IdentityServer4.MongoDB.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using IdentityServer4;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        private readonly IConfiguration configuration;

        public Startup(IConfiguration config)
        {
            configuration = config;
        }
        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you wan to add an MVC-based UI
            services.AddMvc();

            //var builder = services.AddIdentityServer()
            //    .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //    .AddInMemoryApiResources(Config.GetApis())
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddTestUsers(Config.GetUsers());

            //var builder = services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseSuccessEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseErrorEvents = true;
            //})
            //.AddConfigurationStore(configuration.GetSection("MongoDB"))
            //.AddOperationalStore(configuration.GetSection("MongoDB"))
            //.AddDeveloperSigningCredential()
            //.AddJwtBearerClientAuthentication()
            //.AddAppAuthRedirectUriValidator()
            //.AddTestUsers(Config.GetUsers());



            var builder = services.AddIdentityServer()
            .AddTestUsers(Config.GetUsers())
            // this adds the config data from DB (clients, resources)
            .AddConfigurationStore(configuration.GetSection("MongoDB"))
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(configuration.GetSection("MongoDB"));

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // uncomment if you want to support static files
            app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you wan to add an MVC-based UI
            app.UseMvcWithDefaultRoute();
        }
    }
}