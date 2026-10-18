using Microsoft.AspNetCore.Mvc;
using VasilevaAnnaKT_42_23.Filters.DisciplinesFilters;
using VasilevaAnnaKT_42_23.Interfaces.DisciplinesInterfaces;

namespace VasilevaAnnaKT_42_23.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DisciplineController : ControllerBase
    {
        private readonly ILogger<DisciplineController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplineController(ILogger<DisciplineController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            this._disciplineService = disciplineService;
        }

        [HttpPost(Name = "GetDisciplinesByFilter")]
        public async Task<IActionResult> GetGroupsByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            if (filter.IsDeleted == null)
            {
                filter.IsDeleted = false;
            }
            var discipline = await _disciplineService.GetDisciplinesByFilterAsync(filter, cancellationToken);
            return Ok(discipline);
        }

    }
}
