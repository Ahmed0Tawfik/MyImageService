using Microsoft.AspNetCore.Http;
using MyImageService.Exception;
using MyImageService.Interfaces;

namespace MyImageService.Service
{
    public class UploadFileService : IUploadFileService
    {
        public void DeleteImage(string url)
        {
            var filename = Path.GetFileName(url);
            string imageFolderPath = Path.Combine("Images", filename);

            if (File.Exists(imageFolderPath))
            {
                // Delete the file
                File.Delete(imageFolderPath);

            }
            else
            {
                throw new FileDoesNotExistException("File Does Not Exist");
            }

        }
        public string GetUrl(string Scheme, string Host, string fileName)
        {
            return $"{Scheme}://{Host}/Images/{fileName}";
        }

        public string UploadFile(IFormFile file, string requestScheme, string requestHost)
        {
            // Check if the file is valid (extension)
            var filename = file.FileName;
            var extension = Path.GetExtension(filename);

            var allowedExtensions = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".svg"
            };

            bool isValid = allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);
            if (!isValid)
            {
                throw new InvalidFileExtensionException("Invalid file type");
            }

            // CheckingSize

            bool isFileSizeValid = file.Length is > 0 and < 2_000_000;
            if (!isFileSizeValid)
            {
                throw new InvalidFileSizeException("Invalid file size");
            }

            // Save the file
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagePath = Path.Combine(Environment.CurrentDirectory, "Images");
            var fullPath = Path.Combine(imagePath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var url = GetUrl(requestScheme, requestHost, newFileName);
            return url;            
        }
    }
}
