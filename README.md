# MyImageService

## Overview
MyImageService is a simple .NET Core service for handling image uploads and deletions. It includes file validation, size checks, and URL generation for stored images.

## Features
- Upload images with validation for file type and size.
- Delete images by filename.
- Generate accessible URLs for stored images.

## Installation
### Using NuGet Package
You can install the package via NuGet Package Manager:

```sh
Install-Package Ahmed0Tawfik.Services.Images
```

Or using .NET CLI:

```sh
dotnet add package Ahmed0Tawfik.Services.Images
```

### Manual Installation
1. Install the package (if applicable) or add the service to your project.
2. Register the service in your `Startup.cs` or `Program.cs` file.

## Usage

### 1. Register the Service
In your `.NET` application, register the image service by modifying `Program.cs`:

```csharp
using MyImageService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.RegisterImageServices();

var app = builder.Build();
app.Run();
```

### 2. Inject and Use the Service
You can inject `IUploadFileService` into any controller or service where you need file upload functionality.

```csharp
using Microsoft.AspNetCore.Mvc;
using MyImageService.Interfaces;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly IUploadFileService _uploadFileService;

    public ImageController(IUploadFileService uploadFileService)
    {
        _uploadFileService = uploadFileService;
    }

    [HttpPost("upload")]
    public IActionResult UploadImage([FromForm] IFormFile file)
    {
        try
        {
            var url = _uploadFileService.UploadFile(file, Request.Scheme, Request.Host.Value);
            return Ok(new { ImageUrl = url });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("delete")]
    public IActionResult DeleteImage([FromQuery] string fileName)
    {
        try
        {
            _uploadFileService.DeleteImage(fileName);
            return Ok(new { Message = "Image deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
```

## Service Logic Explained
### UploadFile Method
1. **File Type Validation**: Ensures only `.jpg`, `.jpeg`, `.png`, and `.svg` files are uploaded.
2. **File Size Validation**: Ensures file size is between `0 and 2MB`.
3. **File Storage**: Saves the file under the `Images` directory with a unique GUID filename.
4. **URL Generation**: Returns a publicly accessible URL for the uploaded image.

### DeleteImage Method
1. Extracts the filename from the given URL.
2. Checks if the file exists in the `Images` directory.
3. Deletes the file if it exists; otherwise, throws an exception.

## Exception Handling
| Exception | Reason |
|-----------|--------|
| `InvalidFileExtensionException` | If the uploaded file has an invalid extension |
| `InvalidFileSizeException` | If the file size is not within the allowed limit |
| `FileDoesNotExistException` | If the file to delete does not exist |

## License
This project is open-source and available under the MIT License.

