using DemoApp.AppEntities;
using DemoApp.BlobFileSystem;
using DemoApp.IAppServices;
using DemoApp.ToDoUserAttachmentDtos;
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
    public class ToDoUserAttachmentAppService : DemoAppAppService, IToDoUserAttachmentAppService
    {

        private readonly IRepository<ToDoUserAttachment, Guid> _ToDoUserAttachmentRepository;
        private readonly IBlobContainer<ToDoUserFileContainer> _fileContainer;


        public ToDoUserAttachmentAppService(IRepository<ToDoUserAttachment, Guid> ToDoUserAttachmentRepository, IBlobContainer<ToDoUserFileContainer> fileContainer)
        {
            _ToDoUserAttachmentRepository = ToDoUserAttachmentRepository;
            _fileContainer = fileContainer;
        }
        public async Task<ToDoUserAttachmentDto> CreateASync(CreateToDoUserAttachmentDto input)
        {
            ToDoUserAttachment attachments =
                ObjectMapper.Map<CreateToDoUserAttachmentDto, ToDoUserAttachment>(input);

            //Upload file first
            await SaveBlobAsync(input.BinaryFile, input.AttachmentFile);

            //Push to DB
            var attachment = await _ToDoUserAttachmentRepository.InsertAsync(attachments);

            return ObjectMapper.Map<ToDoUserAttachment, ToDoUserAttachmentDto>(attachment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var definitionAttachment = await _ToDoUserAttachmentRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (definitionAttachment != null)
            {
                await _ToDoUserAttachmentRepository.DeleteAsync(definitionAttachment);
                return true;
            }
            else
                return false;
        }

        public async Task<ToDoUserAttachmentDto> GetAsync(Guid id)
        {
            var Attachment = await _ToDoUserAttachmentRepository.GetAsync(id);
            return ObjectMapper.Map<ToDoUserAttachment, ToDoUserAttachmentDto>(Attachment);
        }

        public async Task<byte[]> GetBlobAsync(string fname)
        {
            return await _fileContainer.GetAllBytesOrNullAsync(fname);
        }

        public async Task<PagedResultDto<ToDoUserAttachmentDto>> GetListAsync(GetToDoUserAttachmentListDto input)
        {

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ToDoUserAttachment.AttachmentCaption);
            }

            List<ToDoUserAttachment> Attachments = await _ToDoUserAttachmentRepository.GetPagedListAsync(

                input.SkipCount,
                input.MaxResultCount,
                input.Sorting

                );



            var totalcount = await AsyncExecuter.CountAsync(
                _ToDoUserAttachmentRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    t => t.AttachmentCaption.Contains(input.Filter)
                    )
                );



            List<ToDoUserAttachmentDto> AttachmentDtos =
                ObjectMapper.Map<List<ToDoUserAttachment>, List<ToDoUserAttachmentDto>>(Attachments);

            PagedResultDto<ToDoUserAttachmentDto> result = new PagedResultDto<ToDoUserAttachmentDto>(
                    totalcount, AttachmentDtos
                );



            return result;
        }

        public async Task SaveBlobAsync(byte[] bytes, string fname)
        {
            await _fileContainer.SaveAsync(fname, bytes);
        }

        public async Task<bool> DeleteBlobFileAsync(Guid fname)
        {
            return await _fileContainer.DeleteAsync(fname.ToString());
        }
        public async Task<ToDoUserAttachmentDto> UpdateAsync(Guid id, UpdateToDoUserAtatchmentDto input)
        {
            var attachmentDetail = ObjectMapper.Map<UpdateToDoUserAtatchmentDto, ToDoUserAttachment>(input);

            attachmentDetail = await _ToDoUserAttachmentRepository.GetAsync(id);
            attachmentDetail.AttachmentCaption = input.AttachmentCaption;

            //upload new attachment
            if (input.BinaryFile.Length > 0)
            {
                //delete the existing file
                if (await DeleteBlobFileAsync(attachmentDetail.Id))
                {
                    //if deleted, upload new file   
                    await SaveBlobAsync(input.BinaryFile, attachmentDetail.AttachmentCaption);
                }
            }

            var updatedDetail = await _ToDoUserAttachmentRepository.UpdateAsync(attachmentDetail);
            return ObjectMapper.Map<ToDoUserAttachment, ToDoUserAttachmentDto>(updatedDetail);
        }
    }
}
