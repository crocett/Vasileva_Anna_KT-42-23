using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VasilevaAnnaKT_42_23.Database.Helpers;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "cd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(p => p.StudentId)
                .HasName($"pk_{TableName}_student_id");

            builder.Property(p => p.StudentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("student_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("first_name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("last_name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100);

            builder.Property(p => p.GroupId)
                .IsRequired()
                .HasColumnName("group_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Group)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

            builder.Navigation(p => p.Group).AutoInclude();
        }
    }
}