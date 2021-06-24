using DemoApp.ToDoUserAttachmentDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IToDoUserAttachmentAppService : IApplicationService
    {
        Task<ToDoUserAttachmentDto> GetAsync(Guid id);
        Task<PagedResultDto<ToDoUserAttachmentDto>> GetListAsync(GetToDoUserAttachmentListDto input);
        Task<ToDoUserAttachmentDto> CreateASync(CreateToDoUserAttachmentDto input);

        Task<ToDoUserAttachmentDto> UpdateAsync(Guid id, UpdateToDoUserAtatchmentDto input);
        Task<bool> DeleteAsync(Guid id);

        //Blob File Upload
        Task SaveBlobAsync(byte[] bytes, string fname);
        Task<byte[]> GetBlobAsync(string fname);
    }
}
