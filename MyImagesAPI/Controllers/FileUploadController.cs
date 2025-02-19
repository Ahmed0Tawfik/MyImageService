using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyImageService.Interfaces;

namespace MyImagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController(IUploadFileService uploadFileService) : ControllerBase
    {
        [HttpPost]
        [Route("upload-file")]
        public IActionResult UploadImage(IFormFile file)
        {
            var url = uploadFileService.UploadFile(file, Request.Scheme, Request.Host.Value);
            return Ok(url);

        }

        [HttpDelete]
        [Route("delete-file")]
        public IActionResult DeleteImage(string fileUrl)
        {
            uploadFileService.DeleteImage(fileUrl);
            return Ok("File Deleted");
        }
    }
}
