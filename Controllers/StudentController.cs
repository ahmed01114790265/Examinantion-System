using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly IServiceStudent serviceStudent;
        public StudentController(IServiceStudent serviceStudent)
        {
            this.serviceStudent = serviceStudent;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] ViewmodelStudentForAdding model)
        {
            var dto = new DTOStudentFor_Adding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceStudent.AddStudentAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] ViewmodelStudentForUpdating model)
        {
            if(model.Id==Guid.Empty)
                return BadRequest("Id is required");

            var dto = new DTOStudentFor_Updating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await serviceStudent.UpdateStudentAsync(dto);

            if (result.IsSuccess)
                return Ok(result);

            else
                return BadRequest(result);
        }
        [HttpDelete("DeleteStudent/{Id}")]
        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
            var result = await serviceStudent.DeleteStudentAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllStudents")]

        public async Task<IActionResult> GetAllStudents([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceStudent.GetAllStudentsAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);



            else
                return NotFound(result);
        }


        [HttpGet("GetStudentById/{Id}")]
        public async Task<IActionResult> GetStudentById(Guid Id)
        {
            var result = await serviceStudent.GetStudentByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}
