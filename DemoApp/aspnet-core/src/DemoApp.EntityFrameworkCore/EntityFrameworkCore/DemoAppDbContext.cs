using Microsoft.EntityFrameworkCore;
using DemoApp.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using DemoApp.AppEntities;

namespace DemoApp.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See DemoAppMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class DemoAppDbContext : AbpDbContext<DemoAppDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        //public DbSet<Task1> Task1s { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<DefinitionAttachment> DefinitionAttachments { get; set; }

        public DbSet<AssignedToUser> AssignedToUsers { get; set; }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<ToDoUserAttachment> ToDoUserAttachments { get; set; }
        public DbSet<ToDoUserTask> ToDoUserTasks { get; set; }







        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside DemoAppDbContextModelCreatingExtensions.ConfigureDemoApp
         */

        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();


                /* Configure mappings for your additional properties
                 * Also see the DemoAppEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureDemoApp method */

            builder.ConfigureDemoApp();
        }
    }
}
