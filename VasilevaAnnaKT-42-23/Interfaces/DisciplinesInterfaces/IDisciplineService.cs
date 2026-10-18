using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.DisciplinesFilters;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        public Task<Discipline[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly UniversityDbContext _dbContext;
        public DisciplineService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Discipline[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var disciplines = _dbContext.Set<Discipline>().AsQueryable();

            if (!string.IsNullOrEmpty(filter.DisciplineName))
                disciplines = disciplines.Where(d => d.Name.Contains(filter.DisciplineName));
            if (filter.IsDeleted.HasValue)
                disciplines = disciplines.Where(d => d.IsDeleted == filter.IsDeleted.Value);

            return await disciplines.ToArrayAsync(cancellationToken);
        }

    }
}
