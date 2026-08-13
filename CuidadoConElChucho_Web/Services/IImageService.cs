namespace CuidadoConElChucho_Web.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, string folder);
        void DeleteImage(string? fileName, string folder);
    }
}
