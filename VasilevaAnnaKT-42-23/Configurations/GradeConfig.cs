using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VasilevaAnnaKT_42_23.Database.Helpers;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database.Configuration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "tb_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("grade_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("value")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("student_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("discipline_id")
                .HasColumnType(ColumnType.Int);

            builder.HasOne(p => p.Student)
                .WithMany(p => p.Grades)
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Discipline)
                .WithMany(p => p.Grades)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");

            builder.ToTable(TableName)
                .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            builder.Navigation(p => p.Student).AutoInclude();
            builder.Navigation(p => p.Discipline).AutoInclude();
        }
    }
}