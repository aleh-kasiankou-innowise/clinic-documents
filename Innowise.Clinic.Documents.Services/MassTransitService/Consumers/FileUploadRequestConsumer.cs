using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;
using MassTransit;

namespace Innowise.Clinic.Documents.Services.MassTransitService.Consumers;

public class FileUploadRequestConsumer : IConsumer<BlobUploadRequest>
{
    private readonly IBlobService _blobService;
    private static readonly string DefaultErrorMessage =
        "The internal error occured while saving the file. Please try again later.";

    public FileUploadRequestConsumer(IBlobService blobService)
    {
        _blobService = blobService;
    }

    public async Task Consume(ConsumeContext<BlobUploadRequest> context)
    {
        try
        {
            var fileUrl = await _blobService.SaveFileAsync(context.Message);
            await context.RespondAsync(new BlobUploadResponse(true, fileUrl, null));
        }
        catch (ApplicationException e)
        {
            await context.RespondAsync(new BlobUploadResponse(false, null, e.Message));
        }
        catch (Exception e)
        {
            await context.RespondAsync(new BlobUploadResponse(false, null, DefaultErrorMessage));

        }
    }
}