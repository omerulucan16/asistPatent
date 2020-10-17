using System;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using Microsoft.AspNetCore.Http;
using asistPatentCore.ViewModel;
using System.Text;
using AutoMapper;
using System.Collections.Generic;
using asistPatentCore.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace asistPatentCore.Service
{
    public class UploadImageService : IUploadImageService
    {
        
        MainContext _mainContext = new MainContext();
        private IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        public UploadImageService(IMapper mapper,IHostingEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public string UploadImageSingle(IFormFile file,string path)
        {
            string root = _hostingEnvironment.WebRootPath;
            if (file == null )
            {
                return "";
            }
            string filename = Guid.NewGuid().ToString();
            string pathExtension = Path.GetExtension(file.FileName);
            if (pathExtension == null )
            {
                return "";
            }
            filename = filename + pathExtension;
            try
            {
                var filePath = Path.Combine(Path.Combine(root, path), filename);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    //formFile.CopyToAsync(stream);
                }
                return filename;
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        
       
    }
}
