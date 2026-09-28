using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VasilevaAnnaKT_42_23.Database.Helpers;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd()
                .HasColumnName("group_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);

            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("course")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.SpecialityId)
                .IsRequired()
                .HasColumnName("speciality_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Speciality)
                .WithMany(p => p.Groups)
                .HasForeignKey(p => p.SpecialityId)
                .HasConstraintName("fk_f_speciality_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(TableName)
                .HasIndex(p => p.SpecialityId, $"idx_{TableName}_fk_f_speciality_id");

            builder.Navigation(p => p.Speciality).AutoInclude();
        }
    }
}