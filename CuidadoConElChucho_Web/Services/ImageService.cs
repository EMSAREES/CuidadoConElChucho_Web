namespace CuidadoConElChucho_Web.Services
{
    public class ImageService(IWebHostEnvironment _env) : IImageService
    {
        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            var uploadsPath = Path.Combine(_env.WebRootPath, folder);
            Directory.CreateDirectory(uploadsPath);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public void DeleteImage(string? fileName, string folder)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var filePath = Path.Combine(_env.WebRootPath, folder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
