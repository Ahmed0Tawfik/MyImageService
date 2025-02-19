using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace MyImageService.Interfaces
{
    public interface IUploadFileService
    {
        public string UploadFile(IFormFile file, string requestScheme, string requestHost);
        public void DeleteImage(string fileName);
        protected string GetUrl(string Scheme, string Host, string fileName);
    }
}
