using Microsoft.AspNetCore.Mvc;
using VasilevaAnnaKT_42_23.Filters.GroupFilters;
using VasilevaAnnaKT_42_23.Interfaces.GroupsInterfaces;

namespace VasilevaAnnaKT_42_23.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class GroupController : ControllerBase
        {
            private readonly ILogger<GroupController> _logger;
            private readonly IGroupService _groupService;

            public GroupController(ILogger<GroupController> logger, IGroupService groupService)
            {
                _logger = logger;
                this._groupService = groupService;
            }

            [HttpPost(Name = "GetGroupsByFilter")]
            public async Task<IActionResult> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken = default)
            {
                if (filter.IsDeleted == null)
                {
                    filter.IsDeleted = false;
                }
                var groups = await _groupService.GetGroupsByFilterAsync(filter, cancellationToken);
                return Ok(groups);
            }

        }
    }
