// --------------------------------------------------------------------------------------------------
// <copyright file="BuildExtension.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentValidation;
using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Application.Services;
using LocalFriendzApi.Application.Validations;
using LocalFriendzApi.Domain.IRepositories;
using LocalFriendzApi.Infrastructure.Data.Context;
using LocalFriendzApi.Infrastructure.ExternalServices.Interfaces;
using LocalFriendzApi.Infrastructure.Logging;
using LocalFriendzApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Refit;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace LocalFriendzApi.UI.Configuration
{
    public static class BuildExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddDbContext<AppDbContext>(
                    x =>
                    {
                        x.UseSqlServer(ApiConfiguration.ConnectionString);
                    });

        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddTransient<IContactServices, ContactServices>();

            builder
                .Services
                .AddTransient<IContactRepository, ContactRepository>();
        }

        public static void AddServicesOpenTelemetry(this WebApplicationBuilder builder)
        {
            builder.Services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder.AddPrometheusExporter();
                builder.AddMeter("Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel");
                builder.AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                              0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
                    });


                // Configura as views de histogramas para latência, CPU e memória
                builder.AddView("process.cpu.usage", new ExplicitBucketHistogramConfiguration
                {
                    Boundaries = new double[] { 0, 0.25, 0.5, 0.75, 1 }
                });

                builder.AddView("process.memory.usage", new ExplicitBucketHistogramConfiguration
                {
                    Boundaries = new double[] { 0, 100 * 1024 * 1024, 500 * 1024 * 1024, 1 * 1024 * 1024 * 1024 } // limites de 100MB, 500MB, 1GB
                });
            });
        }


        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            // inserir implementação do cross
        }

        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information,
            }));
        }

        public static void ExternalServices(this WebApplicationBuilder builder)
        {
            builder
            .Services
            .AddRefitClient<IAreaCodeExternalService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://brasilapi.com.br/api"));
        }

        public static void AddFluentValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<CreateContactRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateContactRequestValidator>();
        }
    }
}