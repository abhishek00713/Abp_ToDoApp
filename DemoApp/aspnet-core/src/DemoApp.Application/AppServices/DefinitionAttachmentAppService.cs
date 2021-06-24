using DemoApp.AppEntities;
using DemoApp.BlobFileSystem;
using DemoApp.DefinitionAttachmentDtos;
using DemoApp.IAppServices;
using DemoApp.Permissions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(DemoAppPermissions.DemoApp.Default_Define_ToDo)]
    public class DefinitionAttachmentAppService : DemoAppAppService, IDefinitionAttachmentAppService, IBlobFileUpload
    {
        private readonly IRepository<DefinitionAttachment, Guid> _definitionattachmentRepository;
        private readonly IBlobContainer<ToDoDefinitionFileContainer> _fileContainer;

        public DefinitionAttachmentAppService(IRepository<DefinitionAttachment, Guid> definitionattachmentRepository, IBlobContainer<ToDoDefinitionFileContainer> fileContainer)
        {
            _definitionattachmentRepository = definitionattachmentRepository;
            _fileContainer = fileContainer;
        }



        #region CRUD APIs

        [Authorize(DemoAppPermissions.DemoApp.Create_Define_ToDo)]
        public async Task<DefinitionAttachmentDto> CreateASync(CreateDefinitionAttachmentDto input)
        {
            //create new guid for filename
            var filename = Guid.NewGuid();
            input.FileName = filename;

            DefinitionAttachment attachments =
                ObjectMapper.Map<CreateDefinitionAttachmentDto, DefinitionAttachment>(input);
            //Upload file first
            await SaveBlobFileAsync(input.BinaryFile, input.FileName);

            //Push to DB
            var createdAttachment = await _definitionattachmentRepository.InsertAsync(attachments);

            return ObjectMapper.Map<DefinitionAttachment, DefinitionAttachmentDto>(createdAttachment);
        }

        [Authorize(DemoAppPermissions.DemoApp.Delete_Define_ToDo)]
        public async Task<bool> DeleteAsync(Guid id)
        {
            var definitionAttachment = await _definitionattachmentRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (definitionAttachment != null)
            {
                await _definitionattachmentRepository.DeleteAsync(definitionAttachment);
                return true;
            }
            else
                return false;
        }

        public async Task<DefinitionAttachmentDto> GetAsync(Guid id)
        {
            var definitionAttachment = await _definitionattachmentRepository.GetAsync(id);
            return ObjectMapper.Map<DefinitionAttachment, DefinitionAttachmentDto>(definitionAttachment);
        }

        public async Task<PagedResultDto<DefinitionAttachmentDto>> GetListAsync(GetDefinitionAttachmentListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(DefinitionAttachment.Caption);
            }

            List<DefinitionAttachment> definitionAttachments = await _definitionattachmentRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting

                );



            var totalcount = await AsyncExecuter.CountAsync(
                _definitionattachmentRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    t => t.Caption.Contains(input.Filter)
                    )
                );



            List<DefinitionAttachmentDto> definitionAttachmentDtos =
                ObjectMapper.Map<List<DefinitionAttachment>, List<DefinitionAttachmentDto>>(definitionAttachments);

            PagedResultDto<DefinitionAttachmentDto> result = new PagedResultDto<DefinitionAttachmentDto>(
                    totalcount, definitionAttachmentDtos
                );



            return result;
        }

        [Authorize(DemoAppPermissions.DemoApp.Update_Define_ToDo)]
        public async Task<DefinitionAttachmentDto> UpdateAsync(Guid id, UpdateDefinitionAttachmentDto input)
        {
            var attachmentDetail = ObjectMapper.Map<UpdateDefinitionAttachmentDto, DefinitionAttachment>(input);

            attachmentDetail = await _definitionattachmentRepository.GetAsync(id);
            attachmentDetail.Caption = input.Caption;

            //upload new attachment
            if (input.BinaryFile.Length > 0)
            {
                //delete the existing file
                if (await DeleteBlobFileAsync(attachmentDetail.FileName))
                {
                    //if deleted, upload new file   
                    await SaveBlobFileAsync(input.BinaryFile, attachmentDetail.FileName);
                }
            }

            var updatedDetail = await _definitionattachmentRepository.UpdateAsync(attachmentDetail);
            return ObjectMapper.Map<DefinitionAttachment, DefinitionAttachmentDto>(updatedDetail);
        }

        #endregion


        #region File Attachment Methods
        //Upload Definition File        
        public async Task SaveBlobFileAsync(byte[] bytes, Guid fname)
        {
            await _fileContainer.SaveAsync(fname.ToString(), bytes);
        }

        public async Task<byte[]> GetBlobFileAsync(Guid fname)
        {
            return await _fileContainer.GetAllBytesOrNullAsync(fname.ToString());
        }

        public async Task<bool> DeleteBlobFileAsync(Guid fname)
        {
            return await _fileContainer.DeleteAsync(fname.ToString());
        }
        #endregion

    }
}
