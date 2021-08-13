using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.DefinitionAttachmentDtos
{
    public class CreateDefinitionAttachmentDto
    {
        [Required]
        public Guid ToDoId { get; set; }
        [Required]
        public string Caption { get; set; }
        public Guid FileName { get; set; }
        [Required]
        public string BinaryFile { get; set; }
    }
}
