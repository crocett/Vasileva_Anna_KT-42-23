using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Database;

public class UniversityDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }
}