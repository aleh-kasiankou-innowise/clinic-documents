using Innowise.Clinic.Documents.Services.Exceptions;

namespace Innowise.Clinic.Documents.Services.BlobService.Helpers;

public static class BlobUrlParser
{
    public static (string containerName, string fileName) ParseBlobUrl(string url)
    {
        var parsedUrl = url.Split('/');
        if (parsedUrl.Length < 3)
        {
            throw new InvalidBlobUrlException();
        }

        var containerName = parsedUrl[^2];
        var fileName = parsedUrl[^1];
        return (containerName, fileName);
    }

    public static string BuildBlobUri(string containerName, string fileName)
    {
        var gatewayAndServiceUrl = Environment.GetEnvironmentVariable("GATEWAY_DOCUMENTS_API_URL");
        return gatewayAndServiceUrl + "/" + "FileStorage" + "/" + containerName + "/" + fileName;
        // todo remove hardcoded values
    }
}