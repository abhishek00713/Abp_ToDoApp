using DemoApp.AppEntities;
using Microsoft.EntityFrameworkCore;
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

            builder.Entity<Task1>(b =>
            {
                b.ToTable("Task1s");
                b.ConfigureByConvention();
                b.Property(i => i.TaskName).IsRequired().HasMaxLength(100);



            });

            builder.Entity<ToDo>(b =>
            {
                b.ToTable("ToDos");
                b.ConfigureByConvention();
                b.Property(i => i.Date).IsRequired();
                b.Property(i => i.AssignedBy).IsRequired();
                b.Property(i => i.Remarks);

                //b.HasMany<Category>(g => g.Categories)
                //.WithOne(s => s.ToDo)
                //.HasForeignKey(x => x.Id).IsRequired();


                //b.HasMany<Status>(g => g.Statuses)
                //.WithOne(s => s.ToDo)
                //.HasForeignKey(x => x.Id).IsRequired();


                //b.HasMany<Priority>(g => g.Priorities )
                //.WithOne(s => s.ToDo)
                //.HasForeignKey(x => x.Id).IsRequired();


                //  b.HasOne<Task1>(g => g.todotask)
                //  .WithOne(td => td.ToDo)
                //  .HasForeignKey<Task1>(td => td.Id);



                b.HasOne<Category>().WithMany().HasForeignKey(i => i.CategoryId).IsRequired();
                b.HasOne<Status>().WithMany().HasForeignKey(i => i.StatusId).IsRequired();
                b.HasOne<Priority>().WithMany().HasForeignKey(i => i.PriorityId).IsRequired();
                b.HasOne<Task1>().WithMany().HasForeignKey(i => i.TaskId).IsRequired();
                //b.HasOne<Task1>().WithOne().IsRequired();



            });

            builder.Entity<ToDoAssignedTo>(b =>
            {
                b.ToTable("toDoAssignedTos");
                b.ConfigureByConvention();
                b.HasOne<ToDo>().WithMany().HasForeignKey(i => i.ToDoId).IsRequired();
                b.Property(i => i.AssignedBy).IsRequired();
                b.Property(i => i.AssignedTo).IsRequired();




            });


        }
    }
}