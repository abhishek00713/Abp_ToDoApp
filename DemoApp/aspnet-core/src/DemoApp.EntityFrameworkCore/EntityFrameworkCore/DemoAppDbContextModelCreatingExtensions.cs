using DemoApp.AppEntities;
using DemoApp.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DemoApp.EntityFrameworkCore
{
    public static class DemoAppDbContextModelCreatingExtensions
    {
        public static void ConfigureDemoApp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(DemoAppConsts.DbTablePrefix + "YourEntities", DemoAppConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});


            builder.Entity<Product>(b =>
            {
                b.ToTable("Products");
                b.ConfigureByConvention();
                b.Property(i => i.Name).IsRequired().HasMaxLength(100);
                b.Property(i => i.Price).IsRequired();
                b.Property(i => i.Quantity).IsRequired();
                b.Property(i => i.ProductType).IsRequired().HasMaxLength(100);
            });


            builder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.ConfigureByConvention();
                b.Property(i => i.CategoryName).IsRequired().HasMaxLength(100);



            });

            builder.Entity<Status>(b =>
            {
                b.ToTable("Statuses");
                b.ConfigureByConvention();
                b.Property(i => i.StatusName).IsRequired().HasMaxLength(100);



            });

            builder.Entity<Priority>(b =>
            {
                b.ToTable("Priorities");
                b.ConfigureByConvention();
                b.Property(i => i.PriorityName).IsRequired().HasMaxLength(100);



            });

            builder.Entity<ToDoTask>(b =>
            {
                b.ToTable("ToDoTask");
                b.ConfigureByConvention();
                b.Property(i => i.TaskName).IsRequired().HasMaxLength(100);
            });


            builder.Entity<DefinitionAttachment>(b =>
            {
                b.ToTable("DefinitionAttachments");
                b.ConfigureByConvention();
                b.Property(i => i.Caption).IsRequired().HasMaxLength(100);
                b.Property(i => i.FileName).IsRequired().HasMaxLength(100);

                //relationship with ToDo Schema Table
                b.HasOne(i => i.ToDos).WithMany(j=>j.DefinitionAttachments).HasForeignKey(t => t.ToDoId);
            });

            builder.Entity<AssignedToUser>(b =>
            {
                b.ToTable("AssignedToUsers");
                b.ConfigureByConvention();
                b.Property(i => i.IsActive).IsRequired();

                //relationship with ToDo Schema Table
                b.HasOne(i => i.ToDos).WithMany().HasForeignKey(t => t.ToDoId);
                //relationship with AppUser Schema Table
                b.HasOne(i => i.AbpUser).WithMany().HasForeignKey(t => t.UserId);
            }
            );

            builder.Entity<ToDo>(b =>
            {
                b.ToTable("ToDos");
                b.ConfigureByConvention();
                b.Property(i => i.Date).IsRequired();
                b.Property(i => i.AssignedBy).IsRequired();
                b.Property(i => i.Remarks);

                b.HasOne<Category>(td => td.Category).WithMany(c => c.ToDos).HasForeignKey(i => i.CategoryId).IsRequired();
                b.HasOne<Priority>(td => td.Priority).WithMany(p => p.ToDos).HasForeignKey(i => i.PriorityId).IsRequired();
                b.HasOne<ToDoTask>(td => td.Tasks).WithMany(t => t.ToDos).HasForeignKey(i => i.TaskId).IsRequired();
                b.HasOne<Status>(td => td.Status).WithMany(s => s.ToDos).HasForeignKey(i => i.StatusId).IsRequired();
            });


            builder.Entity<ToDoUserTask>(b =>
            {
                b.ToTable("ToDoUserTasks");
                b.ConfigureByConvention();
                b.HasOne<ToDo>().WithMany().HasForeignKey(i => i.ToDoId).OnDelete(DeleteBehavior.Restrict).IsRequired(); 
                b.HasOne<Status>().WithMany().HasForeignKey(i => i.StatusId).OnDelete(DeleteBehavior.Restrict).IsRequired(); 
            });

            builder.Entity<ToDoUserAttachment>(b =>
            {
                b.ToTable("ToDoUserAttachments");
                b.ConfigureByConvention();
                b.HasOne<ToDoUserTask>().WithMany().HasForeignKey(i => i.ToDOUserTaskId).IsRequired();
                b.Property(i => i.AttachmentCaption).IsRequired().HasMaxLength(100);
                b.Property(i => i.AttachmentFile).IsRequired().HasMaxLength(100);
            });


            var cascadeFks = builder.Model.GetEntityTypes().
               SelectMany(t => t.GetForeignKeys())
               .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFks)
                fk.DeleteBehavior = DeleteBehavior.Cascade;

            ;
        }
    }
}