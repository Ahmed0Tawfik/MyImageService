namespace MyImageService.Exception
{
    public class InvalidFileExtensionException : System.Exception
    {
        public InvalidFileExtensionException(string message) : base(message)
        {
        }
    }
}
