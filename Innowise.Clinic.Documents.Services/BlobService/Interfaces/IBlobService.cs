using Innowise.Clinic.Shared.MassTransit.MessageTypes.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Innowise.Clinic.Documents.Services.BlobService.Interfaces;

public interface IBlobService
{
    Task<FileStreamResult> GetFileAsync(string container, Guid fileId);
    Task<string> SaveFileAsync(BlobUploadRequest uploadRequest, string? customName = null);
    Task UpdateFileAsync(BlobUpdateRequest updateRequest);
    Task DeleteFileAsync(BlobDeletionRequest deletionRequest);
}