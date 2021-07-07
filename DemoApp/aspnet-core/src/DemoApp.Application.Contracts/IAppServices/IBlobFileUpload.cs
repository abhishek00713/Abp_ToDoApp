using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IBlobFileUpload : IApplicationService
    {
        //Blob File Upload
        Task SaveBlobFileAsync(byte[] bytes, Guid fname);
        Task<byte[]> GetBlobFileAsync(Guid fname);

        Task<bool> DeleteBlobFileAsync(Guid fname);
    }
}
