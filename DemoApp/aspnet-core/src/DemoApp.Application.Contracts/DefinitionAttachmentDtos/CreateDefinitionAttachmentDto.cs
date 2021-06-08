using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class CreateDefinitionAttachmentDto
    {
        public Guid ToDoId { get; set; }
        [Required]
        public string AttachmentName { get; set; }
        [Required]
        public string AttachmentFileURL { get; set; }
        [Required]
        public byte[] BinaryFile { get; set; }
    }
}
