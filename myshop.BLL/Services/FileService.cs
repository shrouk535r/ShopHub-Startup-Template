using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using myshop.BLL.Services.IServices;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    internal class FileService:IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string? uploadFile(IFormFile file, string path)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, path);
                var ext = Path.GetExtension(file.FileName);

                using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                return path + filename + ext;
            }
            return null;
        }
        public void DeleteFIle(string path)
        {
            string RootPath = _webHostEnvironment.WebRootPath;
            var oldimg = Path.Combine(RootPath, path.TrimStart('\\'));

            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }

        }

    }
}
