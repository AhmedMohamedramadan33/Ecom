using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class ImageProfileManagement : IImageProfileManagement
    {
        private readonly IFileProvider _fileProvider;
        public ImageProfileManagement(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var ImageName = file.FileName;
                    var ImageSrc = $"/Images/{src}/{ImageName}";
                    var RootPath = Path.Combine(ImageDirectory, ImageName);
                    using (var stream = new FileStream(RootPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);

                }
            }
            return SaveImageSrc;

        }

        public void DeleteImageAsync(string src)
        {
            var ImageInfo = _fileProvider.GetFileInfo(src);
            var rootpath = ImageInfo.PhysicalPath;
            if ((File.Exists(rootpath)))
            {
                File.Delete(rootpath);
            }

        }
    }
}
