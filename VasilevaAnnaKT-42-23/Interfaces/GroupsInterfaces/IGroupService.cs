using Microsoft.EntityFrameworkCore;
using VasilevaAnnaKT_42_23.Database;
using VasilevaAnnaKT_42_23.Filters.GroupFilters;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Interfaces.GroupsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken);
        public Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken);
        public Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken);
        public Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly UniversityDbContext _dbContext;
        public GroupService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Group[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken)
        {
            var groups = _dbContext.Set<Group>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.Speciality))
            {
                var text = filter.Speciality.Trim().ToLower();
                groups = groups.Where(x => x.Speciality != null && x.Speciality.Title.ToLower().Contains(text));
            }
            if (filter.Year.HasValue)
            {
                //groups = groups.Where(x => x.Course == filter.Year.Value);
                string year = filter.Year.Value.ToString().Substring(2);
                groups = groups.Where(x => x.Name.EndsWith("-" + year));
            }
            bool isDeleted = filter.IsDeleted ?? false;
            groups = groups.Where(x => x.IsDeleted == isDeleted);

            return await groups.ToArrayAsync(cancellationToken);
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken)
        {
            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellationToken)
        {
            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(new object[] { groupId }, cancellationToken);
            if (group != null)
            {
                group.IsDeleted = true;

                var studentsToDelete = await _dbContext.Students
                    .Where(x => x.GroupId == groupId)
                    .ToListAsync(cancellationToken);
                foreach (var student in studentsToDelete)
                {
                    student.IsDeleted = true;
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
