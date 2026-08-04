using AuthAPI.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace AuthAPI.Services
{
    public class CloudinaryFileUploadServices : IFileUploadServices
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryFileUploadServices()
        {
            var account = new Account(
                "hpxmrib3",
                "342485654421384",
                "Qsc68kr9SGNzdJFWEV92A6SaIpw"
            );
            _cloudinary = new Cloudinary( account );
        }
        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if(file == null || file.Length == 0) return string.Empty;
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = $"Game/{folderName}",
                UseFilename = true,
                UniqueFilename = true,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}
