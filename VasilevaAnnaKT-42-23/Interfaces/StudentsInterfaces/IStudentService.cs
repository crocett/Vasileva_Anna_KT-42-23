using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.StudentFilters;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;
        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken)
        {
            var students = _dbContext.Set<Student>().Where(x => x.Group.Name == filter.GroupName).ToArrayAsync(cancellationToken);
            return students;
        }
    }
}
