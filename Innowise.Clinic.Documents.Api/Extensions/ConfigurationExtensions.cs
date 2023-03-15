using Azure.Storage.Blobs;
using Innowise.Clinic.Documents.Services.BlobService.Implementations;
using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Documents.Services.MassTransitService.Consumers;
using MassTransit;

namespace Innowise.Clinic.Documents.Api.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureCrossServiceCommunication(this IServiceCollection services, IConfiguration rabbitMqConfig)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<FileUploadRequestConsumer>();
            x.AddConsumer<FileUpdateRequestConsumer>();
            x.AddConsumer<FileDeletionRequestConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfig["HostName"], h =>
                {
                    h.Username(rabbitMqConfig["UserName"]);
                    h.Password(rabbitMqConfig["Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }

    public static IServiceCollection ConfigureBlobStorage(this IServiceCollection services, 
        string connectionString)
    {
        services.AddSingleton(x => new BlobServiceClient(connectionString));
        services.AddSingleton<IBlobService, BlobService>();
        return services;
    }
}