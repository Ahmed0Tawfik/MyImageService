namespace MyImageService.Exception
{
    public class FileDoesNotExistException : System.Exception
    {
        public FileDoesNotExistException(string message) : base(message)
        {
        }
    }
}
