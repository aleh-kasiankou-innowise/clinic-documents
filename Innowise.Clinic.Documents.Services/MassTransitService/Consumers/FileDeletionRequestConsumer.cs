using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;
using MassTransit;

namespace Innowise.Clinic.Documents.Services.MassTransitService.Consumers;

public class FileDeletionRequestConsumer : IConsumer<BlobDeletionRequest>
{
    private readonly IBlobService _blobService;

    private static readonly string DefaultErrorMessage =
        "The internal error occured while deleting the file. Please try again later.";

    public FileDeletionRequestConsumer(IBlobService blobService)
    {
        _blobService = blobService;
    }

    public async Task Consume(ConsumeContext<BlobDeletionRequest> context)
    {
        try
        {
            await _blobService.DeleteFileAsync(context.Message);
            await context.RespondAsync(new BlobDeletionResponse(true, null));
        }
        catch (ApplicationException e)
        {
            await context.RespondAsync(new BlobDeletionResponse(false, e.Message));
        }
        catch (Exception e)
        {
            await context.RespondAsync(new BlobDeletionResponse(false, DefaultErrorMessage));

        }
    }
}