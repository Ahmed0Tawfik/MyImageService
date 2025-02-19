namespace MyImageService.Exception
{
    public class InvalidFileSizeException : System.Exception
    {
        public InvalidFileSizeException(string message) : base(message)
        {
        }
    }
}
