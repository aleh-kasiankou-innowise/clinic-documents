using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;
using MassTransit;

namespace Innowise.Clinic.Documents.Services.MassTransitService.Consumers;

public class FileUpdateRequestConsumer : IConsumer<BlobUpdateRequest>
{
    private readonly IBlobService _blobService;
    private static readonly string DefaultErrorMessage =
        "The internal error occured while updating the file. Please try again later.";

    public FileUpdateRequestConsumer(IBlobService blobService)
    {
        _blobService = blobService;
    }

    public async Task Consume(ConsumeContext<BlobUpdateRequest> context)
    {
        try
        {
            await _blobService.UpdateFileAsync(context.Message);
            await context.RespondAsync(new BlobUpdateResponse(true, null));
        }
        catch (ApplicationException e)
        {
            await context.RespondAsync(new BlobUpdateResponse(false, e.Message));
        }
        catch (Exception e)
        {
            await context.RespondAsync(new BlobUpdateResponse(false, DefaultErrorMessage));

        }
    }
}