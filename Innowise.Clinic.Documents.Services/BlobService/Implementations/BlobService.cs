using Azure.Storage.Blobs;
using Innowise.Clinic.Documents.Services.BlobService.Helpers;
using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Documents.Services.Exceptions;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;
using Microsoft.AspNetCore.Mvc;
using BlobHttpHeaders = Azure.Storage.Blobs.Models.BlobHttpHeaders;

namespace Innowise.Clinic.Documents.Services.BlobService.Implementations;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobStorageClient;

    public BlobService(BlobServiceClient blobClient)
    {
        _blobStorageClient = blobClient;
    }

    public async Task<FileStreamResult> GetFileAsync(string container, Guid fileId)
    {
        var containerClient = _blobStorageClient.GetBlobContainerClient(container) ??
                              throw new InvalidBlobUrlException();

        var blobClient = containerClient.GetBlobClient(fileId.ToString());

        var blobDownloadInfo = await blobClient.DownloadAsync();
        return new FileStreamResult(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
    }

    public async Task<string> SaveFileAsync(BlobUploadRequest uploadRequest, string? customName = null)
    {
        var containerName = uploadRequest.FileCategory;
        var containerClient = _blobStorageClient.GetBlobContainerClient(containerName) ??
                              throw new InvalidBlobUrlException();
        await containerClient.CreateIfNotExistsAsync();
        var newBlobName = customName ?? Guid.NewGuid().ToString();
        var blobClient = containerClient.GetBlobClient(newBlobName);

        await using var contentStream = new MemoryStream(uploadRequest.FileContent);
        await blobClient.UploadAsync(contentStream,
            new BlobHttpHeaders
            {
                ContentType = uploadRequest.FileType,
            });

        return BlobUrlParser.BuildBlobUri(containerName, newBlobName);
    }

    public async Task UpdateFileAsync(BlobUpdateRequest updateRequest)
    {
        var blobData = BlobUrlParser.ParseBlobUrl(updateRequest.FileUrl);
        await SaveFileAsync(new BlobUploadRequest(updateRequest.FileContent, updateRequest.FileType,
            blobData.containerName), blobData.fileName);
    }

    public async Task DeleteFileAsync(BlobDeletionRequest deletionRequest)
    {
        var blobData = BlobUrlParser.ParseBlobUrl(deletionRequest.FileUrl);
        var containerClient = _blobStorageClient.GetBlobContainerClient(blobData.containerName) ??
                              throw new InvalidBlobUrlException();

        var blobClient = containerClient.GetBlobClient(blobData.fileName);
        await blobClient.DeleteAsync();
    }
}