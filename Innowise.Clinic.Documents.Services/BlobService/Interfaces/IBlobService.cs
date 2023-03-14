using Azure.Storage.Blobs.Models;
using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;

namespace Innowise.Clinic.Documents.Services.BlobService.Interfaces;

public interface IBlobService
{
    Task<BlobInfo> GetFileAsync(string container, Guid fileId);
    Task<string> SaveFileAsync(BlobUploadRequest uploadRequest);
    Task UpdateFileAsync(BlobUpdateRequest updateRequest);
    Task DeleteFileAsync(BlobDeletionRequest deletionRequest);
}