using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
using Microsoft.AspNetCore.Http;

namespace asistPatentCore.Service.IConstractor
{
    public interface IUploadImageService
    {
        string UploadImageSingle(IFormFile file, string path);
    }
}
