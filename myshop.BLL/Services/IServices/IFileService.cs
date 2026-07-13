using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services.IServices
{
    public interface IFileService
    {
        public string? uploadFile(IFormFile file,string path);
        public void DeleteFIle(string path);
    }
}
