using DemoApp.AppEntities;
using DemoApp.BlobFileSystem;
using DemoApp.DefinitionAttachmentDtos;
using DemoApp.IAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace DemoApp.AppServices
{
    public class DefinitionAttachmentAppService : DemoAppAppService, IDefinitionAttachmentAppService
    {
        private readonly IRepository<DefinitionAttachment, Guid> _definitionattachmentRepository;
        private readonly IBlobContainer<ToDoDefinitionFileContainer> _fileContainer;

        public DefinitionAttachmentAppService(IRepository<DefinitionAttachment, Guid> definitionattachmentRepository, IBlobContainer<ToDoDefinitionFileContainer> fileContainer)
        {
            _definitionattachmentRepository = definitionattachmentRepository;
            _fileContainer = fileContainer;
        }

        public async Task<DefinitionAttachmentDto> CreateASync(CreateDefinitionAttachmentDto input)
        {
            DefinitionAttachment attachments =
                ObjectMapper.Map<CreateDefinitionAttachmentDto, DefinitionAttachment>(input);

            //Upload file first
            await SaveBlobAsync(input.BinaryFile, input.AttachmentFileURL);

            //Push to DB
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


        //Upload Definition File
        public async Task SaveBlobAsync(byte[] bytes, string fname)
        {            
            await _fileContainer.SaveAsync(fname, bytes);
        }

        //Get Definition File
        public async Task<byte[]> GetBlobAsync(string fname)
        {           
            return await _fileContainer.GetAllBytesOrNullAsync(fname);
        }
    }
}
