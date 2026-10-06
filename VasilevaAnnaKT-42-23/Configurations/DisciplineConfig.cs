using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VasilevaAnnaKT_42_23.Database.Helpers;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database.Configuration
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "tb_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder
                .HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd()
                .HasColumnName("discipline_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(200);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);

            builder.HasMany(p => p.Grades)
                .WithOne(p => p.Discipline)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(TableName);
        }
    }
}