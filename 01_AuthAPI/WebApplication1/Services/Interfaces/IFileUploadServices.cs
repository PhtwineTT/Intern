namespace AuthAPI.Services.Interfaces
{
    public interface IFileUploadServices
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}
