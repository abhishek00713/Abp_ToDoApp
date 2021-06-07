using DemoApp.AppEntities;
using DemoApp.DefinitionAttachmentDtos;
using DemoApp.IAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace DemoApp.AppServices
{
    public class DefinitionAttachmentAppService : DemoAppAppService, IDefinitionAttachmentAppService
    {
        private readonly IRepository<DefinitionAttachment, Guid> _definitionattachmentRepository;

        public DefinitionAttachmentAppService(IRepository<DefinitionAttachment, Guid> definitionattachmentRepository)
        {
            _definitionattachmentRepository = definitionattachmentRepository;
        }

        public async Task<DefinitionAttachmentDto> CreateASync(CreateDefinitionAttachmentDto input)
        {
            DefinitionAttachment attachments =
                ObjectMapper.Map<CreateDefinitionAttachmentDto, DefinitionAttachment>(input);

            var attachment = await _definitionattachmentRepository.InsertAsync(attachments);

            return ObjectMapper.Map<DefinitionAttachment, DefinitionAttachmentDto>(attachment);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DefinitionAttachmentDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<DefinitionAttachmentDto>> GetListAsync(GetDefinitionAttachmentListDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateDefinitionAttachmentDto input)
        {
            throw new NotImplementedException();
        }
    }
}
