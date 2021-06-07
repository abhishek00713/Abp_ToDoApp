using DemoApp.DefinitionAttachmentDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DemoApp.IAppServices
{
    public interface IDefinitionAttachmentAppService : IApplicationService
    {
        Task<DefinitionAttachmentDto> GetAsync(Guid id);
        Task<PagedResultDto<DefinitionAttachmentDto>> GetListAsync(GetDefinitionAttachmentListDto input);
        Task<DefinitionAttachmentDto> CreateASync(CreateDefinitionAttachmentDto input);
        Task UpdateAsync(Guid id, UpdateDefinitionAttachmentDto input);
        Task DeleteAsync(Guid id);
    }
}
