using Azure.Storage.Blobs.Models;
using Innowise.Clinic.Documents.Services.BlobService.Interfaces;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;

namespace Innowise.Clinic.Documents.Services.BlobService.Implementations;

public class BlobService : IBlobService
{
    public async Task<BlobInfo> GetFileAsync(string container, Guid fileId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> SaveFileAsync(BlobUploadRequest uploadRequest)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateFileAsync(BlobUpdateRequest updateRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteFileAsync(BlobDeletionRequest deletionRequest)
    {
        throw new NotImplementedException();
    }
}