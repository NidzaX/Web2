using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Infrastructure.Configurations
{
    internal sealed class RideConfiguration : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {

            builder.ToTable("rides");

            builder.HasKey(ride => ride.Id);

            builder.Property(ride => ride.Price).IsRequired()
                .HasConversion(price => price.Value , value => new Price(value));
            builder.Property(ride => ride.PredictedTime).IsRequired()
                 .HasConversion(predictedTime => predictedTime.Value, value => new PredictedTime(value));
            builder.Property(ride => ride.StartAddress).IsRequired()
                .HasConversion(startAddress => startAddress.Value , value => new StartAddress(value));
            builder.Property(ride => ride.EndAddress).IsRequired()
               .HasConversion(endAddress => endAddress.Value, value => new EndAddress(value));

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(user => user.Id);
        }
    }
}
