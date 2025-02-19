using Microsoft.Extensions.DependencyInjection;
using MyImageService.Interfaces;
using MyImageService.Service;

namespace MyImageService
{
    public static class DependencyInjection
    {
        public static void RegisterImageServices(this IServiceCollection services)
        {
            services.AddScoped<IUploadFileService, UploadFileService>();
        }
    }
}
