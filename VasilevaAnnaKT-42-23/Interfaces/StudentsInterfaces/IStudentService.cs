using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.StudentFilters;
using VasilevaAnnaKT_42_23.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VasilevaAnnaKT_42_23.Interfaces.StudentsInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByFilterAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }

    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _dbContext;
        public StudentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student[]> GetStudentsByFilterAsync(StudentGroupFilter filter, CancellationToken cancellationToken)
        {
            var students = _dbContext.Set<Student>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.GroupName))
                {
                students = students.Where(x => x.Group != null && x.Group.Name == filter.GroupName);
                }
            if (!string.IsNullOrWhiteSpace(filter.Fio))
                {
                var text = filter.Fio.Trim().ToLower();
                students = students.Where(x =>
                    x.FirstName.ToLower().Contains(text) ||
                    x.LastName.ToLower().Contains(text));
            }
            bool isDeleted = filter.IsDeleted ?? false;
            students = students.Where(x => x.IsDeleted == isDeleted);

            return await students.ToArrayAsync(cancellationToken);
        }
           
    }
}
