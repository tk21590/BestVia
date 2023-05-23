using Microsoft.AspNetCore.Http;
using System.Text;

namespace BestVia.Client.Services
{
    public interface IFileServicesClient
    {
        Task ExportFile(string path, byte[] bytes);
    }

    public class FileServicesClient : IFileServicesClient
    {
        //public async Task UploadFile(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var imagePath = @"\Data\TempFiles";
        //        var uploadPath = _env.WebRootPath + imagePath;
        //        if (!Directory.Exists(uploadPath))
        //        {
        //            Directory.CreateDirectory(uploadPath);
        //        }
        //        var fullPath = Path.Combine(uploadPath, file.FileName);
        //        string fullUploadPath = fullPath;
        //        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        //        {
        //            await file.CopyToAsync(fileStream);
        //        }
        //    }
        //}

        public async Task ExportFile(string path, byte[] bytes)
        {
            await File.WriteAllBytesAsync(path, bytes);

        }
    }
}
