using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Users;

namespace Taxi.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName).IsRequired()
                .HasConversion(firstName => firstName.Value, value => new FirstName(value));
            builder.Property(user => user.LastName).IsRequired()
                .HasConversion(lastName => lastName.Value, value => new LastName(value));
            builder.Property(user => user.Email).IsRequired()
                .HasConversion(email => email.Value, value => new Email(value));
            builder.Property(user => user.Username).IsRequired()
                .HasConversion(username => username.Value, value => new Username(value));
            builder.Property(user => user.Password).IsRequired()
                .HasConversion(password => password.Value, value => new Password(value));
            builder.Property(user => user.Address).IsRequired()
               .HasConversion(address => address.Value, value => new Address(value));

            builder.Property(user => user.Birthday).IsRequired()
              .HasConversion(birthday => birthday.Value, value => new Birthday(value));
            builder.Property(user => user.Picture)
              .HasConversion(picture => picture.Value, value => new Picture(value));
            builder.Property(user => user.UserTypes).IsRequired();

            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasIndex(user => user.Username).IsUnique();
        }
    }
}
