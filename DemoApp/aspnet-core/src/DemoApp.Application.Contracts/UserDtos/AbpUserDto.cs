using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DemoApp.UserDtos
{
    public class AbpUserDto : EntityDto<Guid>
    {
        public virtual Guid? TenantId { get; private set; }

        public virtual string UserName { get; private set; }

        public virtual string Name { get; private set; }

        public virtual string Surname { get; private set; }

        public virtual string Email { get; private set; }

        public virtual bool EmailConfirmed { get; private set; }

        public virtual string PhoneNumber { get; private set; }

        public virtual bool PhoneNumberConfirmed { get; private set; }
    }
}
