﻿using DemoApp.AppEntities;
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
        }
    }
}