using AuthAPI.Services.Interfaces;
using System.Net.Http.Headers;
using System.IO;
namespace AuthAPI.Services
{
    public class LocalFileUploadServices : IFileUploadServices
    {
        private readonly IWebHostEnvironment _enviroment;
        public LocalFileUploadServices(IWebHostEnvironment environment)
        {
            _enviroment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return string.Empty;
            var folderPath = Path.Combine(_enviroment.WebRootPath, "image", folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/images/{folderName}/{fileName}";
        }
    }
}