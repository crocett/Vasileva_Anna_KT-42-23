using Microsoft.AspNetCore.Mvc;
using VasilevaAnnaKT_42_23.Filters.GroupFilters;
using VasilevaAnnaKT_42_23.Interfaces.GroupsInterfaces;
using VasilevaAnnaKT_42_23.Models;

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

        [HttpPost("add")]
        public async Task<IActionResult> AddGroupAsync(Group group, CancellationToken cancellationToken)
        {
            return Ok(await _groupService.AddGroupAsync(group, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateGroupAsync(Group group, CancellationToken cancellationToken)
        {
            return Ok(await _groupService.UpdateGroupAsync(group, cancellationToken));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteGroupAsync(int id, CancellationToken cancellationToken)
        {
            await _groupService.DeleteGroupAsync(id, cancellationToken);
            return Ok();
        }

    }
    }
