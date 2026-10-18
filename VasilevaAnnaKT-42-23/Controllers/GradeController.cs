using Microsoft.AspNetCore.Mvc;
using VasilevaAnnaKT_42_23.Filters.GradeFilters;
using VasilevaAnnaKT_42_23.Interfaces.GradesInterfaces;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IGradeService _gradeService;

        public GradeController(ILogger<GradeController> logger, IGradeService gradeService)
        {
            _logger = logger;
            this._gradeService = gradeService;
        }
        [HttpPost("average-by-group")]
        public async Task<IActionResult> GetAverageByGroupAsync(GroupDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await _gradeService.GetAverageByGroupAndDisciplineAsync(filter, cancellationToken);

            if (result == null)
                return NotFound("Оценок по данному предмету в этой группе нет.");

            return Ok(new { AverageGrade = Math.Round(result.Value, 2) });
        }

        [HttpPost("student-grade")]
        public async Task<IActionResult> GetStudentGradeAsync(StudentDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await _gradeService.GetStudentGradeAsync(filter, cancellationToken);

            if (result == null)
                return NotFound("У студента нет оценки по данному предмету.");

            return Ok(new { Grade = result.Value });
        }

        [HttpPost("average-by-year")]
        public async Task<IActionResult> GetAverageByYearAsync(YearFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await _gradeService.GetAverageByYearAsync(filter, cancellationToken);

            if (result == null)
                return NotFound("Оценок для студентов этого года поступления нет.");

            return Ok(new { AverageGrade = Math.Round(result.Value, 2) });
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.AddGradeAsync(grade, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken)
        {
            return Ok(await _gradeService.UpdateGradeAsync(grade, cancellationToken));
        }
    }
}