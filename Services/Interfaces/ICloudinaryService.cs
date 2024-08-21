namespace AppointmentAPI.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadPhotoAsync(IFormFile file, string fileName);
    }
}
