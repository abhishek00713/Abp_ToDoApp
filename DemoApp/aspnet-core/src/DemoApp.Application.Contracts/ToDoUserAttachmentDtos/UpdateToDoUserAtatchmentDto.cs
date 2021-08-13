using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.ToDoUserAttachmentDtos
{
    public class UpdateToDoUserAtatchmentDto
    {

        [Required]
        public Guid ToDOUserTaskId { get; set; }

        [Required]
        public string AttachmentCaption { get; set; }

        [Required]
        public string AttachmentFile { get; set; }

        [Required]
        public byte[] BinaryFile { get; set; }
    }
}
