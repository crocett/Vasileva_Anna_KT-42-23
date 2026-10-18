using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.DisciplinesFilters;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Interfaces.DisciplinesInterfaces
{
    public interface IDisciplineService
    {
        public Task<Discipline[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken);
        public Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        public Task<Discipline> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken);
        public Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken);
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
        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Discipline> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            _dbContext.Disciplines.Update(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Disciplines.FindAsync(new object[] { disciplineId }, cancellationToken);
            if (discipline != null)
            {
                discipline.IsDeleted = true; 
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
