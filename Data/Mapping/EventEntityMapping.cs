using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using FICR.Cooperation.Humanism.Entities;

namespace FICR.Cooperation.Humanism.Data.Mapping
{
    public class EventEntityMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            ;
            builder.ToTable("Eventos");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.description).HasMaxLength(100).IsRequired();
            builder.Property(u => u.title).HasMaxLength(100).IsRequired();
            builder.Property(u => u.imagePath);
            builder.Property(u => u.scheduling).IsRequired();

        }
    }
}