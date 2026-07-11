using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using BookStore.Application.Services.Interfaces;
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

            string bigPath = Path.Combine(
            _environment.WebRootPath,
            "images",
            "books",
            "big",
            imageName);

            string smallPath = Path.Combine(
           _environment.WebRootPath,
           "images",
           "books",
           "small",
           imageName);

            string mediumPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "medium",
                imageName);

            string thumbPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "thumb",
                imageName);

            // اطمینان از وجود پوشه‌ها
            Directory.CreateDirectory(Path.GetDirectoryName(bigPath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(smallPath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(mediumPath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(thumbPath)!);


            // ذخیره نسخه بزرگ
            using (var bigImage =
                   await Image.LoadAsync(imageFile.OpenReadStream()))
            {
                bigImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(1200, 1200),
                    Mode = ResizeMode.Crop
                }));

                await bigImage.SaveAsJpegAsync(
                    bigPath,
                    new JpegEncoder
                    {
                        Quality = 90
                    });
            }

            // ذخیره نسخه کوچک
            using (var smallImage =
                   await Image.LoadAsync(imageFile.OpenReadStream()))
            {
                smallImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(128, 128),
                    Mode = ResizeMode.Crop
                }));

                await smallImage.SaveAsJpegAsync(
                    smallPath,
                    new JpegEncoder
                    {
                        Quality = 65
                    });
            }

            // ساخت نسخه Medium
            using (var mediumImage =
                   await Image.LoadAsync(imageFile.OpenReadStream()))
            {
                mediumImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(458, 458),
                    Mode = ResizeMode.Crop
                }));

                await mediumImage.SaveAsJpegAsync(
                    mediumPath,
                    new JpegEncoder
                    {
                        Quality = 75
                    });
            }

            // ساخت Thumbnail
            using (var thumbImage =
                   await Image.LoadAsync(imageFile.OpenReadStream()))
            {
                thumbImage.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(40, 40),
                    Mode = ResizeMode.Crop
                }));

                await thumbImage.SaveAsJpegAsync(
                    thumbPath,
                    new JpegEncoder
                    {
                        Quality = 65
                    });
            }

            return imageName;
        }

        public void DeleteImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return;

            string bigPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "big",
                imageName);

            string mediumPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "medium",
                imageName);

            string thumbPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "thumb",
                imageName);
            
            string smallPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "books",
                "small",
                imageName);


            if (File.Exists(bigPath))
                File.Delete(bigPath);

            if (File.Exists(mediumPath))
                File.Delete(mediumPath);

            if (File.Exists(thumbPath))
                File.Delete(thumbPath); 
            
            if (File.Exists(smallPath))
                File.Delete(smallPath);
        }

    }
}

