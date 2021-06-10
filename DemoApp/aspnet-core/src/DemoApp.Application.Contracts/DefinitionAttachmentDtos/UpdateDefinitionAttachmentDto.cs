using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class UpdateDefinitionAttachmentDto
    {
        [Required]
        public string Caption { get; set; }
        public Guid FileName { get; set; }
        public byte[] BinaryFile { get; set; }
    }
}
