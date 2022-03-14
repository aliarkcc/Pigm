using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", @"dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasColumnName("Password").HasMaxLength(30).IsRequired();
            builder.Property(x => x.Gender).HasColumnName("Gender").IsRequired();
            builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);

            builder.HasData(new User
            {
                Id = 1,
                UserName = "mali",
                FirstName = "Mehmet Ali",
                LastName = "Arkaç",
                Password = "1",
                Gender = true,
                DateOfBirth = Convert.ToDateTime("01-04-1999"),
                Address = "İstanbul",
                Email = "alimehmet1@windowslive.com",
                CreatedUserId = 1,
                CreatedDate = DateTime.Now
            });

        }
    }
}
