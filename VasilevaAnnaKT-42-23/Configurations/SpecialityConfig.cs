using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VasilevaAnnaKT_42_23.Database.Helpers;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database.Configuration
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        private const string TableName = "cd_speciality";

        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder
                .HasKey(p => p.SpecialityId)
                .HasName($"pk_{TableName}_speciality_id");

            builder.Property(p => p.SpecialityId)
                .ValueGeneratedOnAdd()
                .HasColumnName("speciality_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(200);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnName("code")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(20);

            builder.ToTable(TableName);
        }
    }
}