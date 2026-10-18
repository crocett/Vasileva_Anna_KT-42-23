using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using System.Threading;
using VasilevaAnnaKT_42_23.Filters.StudentFilters;
using VasilevaAnnaKT_42_23.Interfaces.StudentsInterfaces;
using VasilevaAnnaKT_42_23.Models;

namespace VasilevaAnnaKT_42_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            this._studentService = studentService;
        }

        [HttpPost(Name = "GetStudentsByFilter")]
        public async Task<IActionResult> GetStudentsByFilterAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            if (filter.IsDeleted == null)
            {
                filter.IsDeleted = false;
            }
            var students = await _studentService.GetStudentsByFilterAsync(filter, cancellationToken);
            return Ok(students);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            return Ok(await _studentService.AddStudentAsync(student, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            return Ok(await _studentService.UpdateStudentAsync(student, cancellationToken));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteStudentAsync(int id, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(id, cancellationToken);
            return Ok();
        }

    }
}
