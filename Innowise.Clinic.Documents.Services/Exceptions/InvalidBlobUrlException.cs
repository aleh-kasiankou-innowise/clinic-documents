namespace Innowise.Clinic.Documents.Services.Exceptions;

public class InvalidBlobUrlException : ApplicationException
{
    private static readonly string DefaultMessage = "The URI of the requested file is invalid or file does not exist.";
    public InvalidBlobUrlException() : base(DefaultMessage)
    {
    }
}