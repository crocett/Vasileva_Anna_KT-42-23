using Microsoft.AspNetCore.Mvc;
using System.Threading;
using VasilevaAnnaKT_42_23.Filters.DisciplinesFilters;
using VasilevaAnnaKT_42_23.Interfaces.DisciplinesInterfaces;
using VasilevaAnnaKT_42_23.Models;

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
        [HttpPost("add")]
        public async Task<IActionResult> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            return Ok(await _disciplineService.AddDisciplineAsync(discipline, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken)
        {
            return Ok(await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteDisciplineAsync(int id, CancellationToken cancellationToken)
        {
            await _disciplineService.DeleteDisciplineAsync(id, cancellationToken);
            return Ok();
        }

    }
}
