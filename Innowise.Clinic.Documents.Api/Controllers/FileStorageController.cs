using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Shared.ControllersAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Innowise.Clinic.Documents.Api.Controllers;

public class FileStorageController : ApiControllerBase
{
    private readonly IBlobService _blobService;

    public FileStorageController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpGet("{container:required}/{fileId:guid}")]
    public async Task<FileStreamResult> GetFile([FromRoute] string container, [FromRoute] Guid fileId)
    {
        return await _blobService.GetFileAsync(container, fileId);
    }
}
