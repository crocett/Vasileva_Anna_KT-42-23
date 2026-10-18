using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.GradeFilters;
using VasilevaAnnaKT_42_23.Models;
namespace VasilevaAnnaKT_42_23.Interfaces.GradesInterfaces
{
        public interface IGradeService
        {
        public Task<double?> GetAverageByGroupAndDisciplineAsync(GroupDisciplineFilter filter, CancellationToken cancellationToken);
        public Task<int?> GetStudentGradeAsync(StudentDisciplineFilter filter, CancellationToken cancellationToken);
        public Task<double?> GetAverageByYearAsync(YearFilter filter, CancellationToken cancellationToken);
        public Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken);
        public Task<Grade> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken);
    }

        public class GradeService : IGradeService
        {
            private readonly UniversityDbContext _dbContext;
            public GradeService(UniversityDbContext dbContext)
            {
                _dbContext = dbContext;
            }
        public async Task<double?> GetAverageByGroupAndDisciplineAsync(GroupDisciplineFilter filter, CancellationToken cancellationToken)
        {
            var grades = await _dbContext.Set<Grade>()
                .Where(g => g.Student.GroupId == filter.GroupId && g.DisciplineId == filter.DisciplineId)
                .Select(g => (double?)g.Value)
                .ToListAsync(cancellationToken);
            return grades.Any() ? grades.Average() : null;
        }

        public async Task<int?> GetStudentGradeAsync(StudentDisciplineFilter filter, CancellationToken cancellationToken)
        {
            var grade = await _dbContext.Set<Grade>()
                .FirstOrDefaultAsync(g => g.StudentId == filter.StudentId && g.DisciplineId == filter.DisciplineId, cancellationToken);

            return grade?.Value;
        }

        public async Task<double?> GetAverageByYearAsync(YearFilter filter, CancellationToken cancellationToken)
        {
            string yearStr = filter.Year.ToString();
            string yearSuffix = yearStr.Length == 4 ? yearStr.Substring(2) : yearStr;

            var grades = await _dbContext.Set<Grade>()
                .Where(g => g.Student.Group.Name.EndsWith("-" + yearSuffix))
                .Select(g => (double?)g.Value)
                .ToListAsync(cancellationToken);

            return grades.Any() ? grades.Average() : null;
        }

        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            _dbContext.Grades.Add(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

        public async Task<Grade> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            _dbContext.Grades.Update(grade);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }

    }
}
