using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);

            modelBuilder.Entity<UserProperty>().ToTable("UserProperties").HasKey(u => new { u.Key,u.Value,u.UserId});
            modelBuilder.Entity<UserProperty>().Property(u => u.Key).HasMaxLength(100);
            modelBuilder.Entity<UserProperty>().Property(u => u.Value).HasMaxLength(100);

            modelBuilder.Entity<UserTag>().Property(u => u.Tag).HasMaxLength(100);
            modelBuilder.Entity<UserTag>().ToTable("UserTags").HasKey(u => new { u.Tag, u.UserId });

            modelBuilder.Entity<BPFile>().ToTable("BPFiles").HasKey(u => u.id);
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }
        }
        public class BoolToIntConverter : ValueConverter<bool, int>
        {
            public BoolToIntConverter(ConverterMappingHints mappingHints = null)
                : base(
                      v => Convert.ToInt32(v),
                      v => Convert.ToBoolean(v),
                      mappingHints)
            {
            }

            public static ValueConverterInfo DefaultInfo { get; }
                = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserProperty> UserProperties { get; set; }
    }
}
