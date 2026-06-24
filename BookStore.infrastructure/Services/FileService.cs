using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
namespace BookStore.infrastructure.Services
{
    public class FileService:IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // کنترل حجم
            if (imageFile.Length > 10 * 1024 * 1024)
                throw new Exception("حجم فایل نباید بیشتر از 10 مگابایت باشد.");

            // کنترل فرمت
            string[] allowedExtensions =
            {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

            string extension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new Exception("فرمت فایل معتبر نیست.");

            string imageName = Guid.NewGuid() + ".jpg";

            string path = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                imageName);

            using var image =
                await Image.LoadAsync(imageFile.OpenReadStream());

            // اگر عکس بزرگ بود کوچک کن
            if (image.Width > 800 || image.Height > 800)
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 800),
                    Mode = ResizeMode.Max
                }));
            }

            await image.SaveAsJpegAsync(path,
                new JpegEncoder
                {
                    Quality = 80
                });

            return imageName;
        }

        public void DeleteImage(string imageName)
        {
            string path = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                imageName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}

