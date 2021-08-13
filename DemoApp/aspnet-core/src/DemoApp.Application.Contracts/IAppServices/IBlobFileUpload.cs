using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace DemoApp.IAppServices
{
    public interface IBlobFileUpload : IApplicationService
    {
        //Blob File Upload
        Task SaveBlobFileAsync(IBlobContainer container, byte[] bytes, Guid fname);
        Task<byte[]> GetBlobFileAsync(Guid fname);

        Task<bool> DeleteBlobFileAsync(Guid fname);
    }
}
