using Application.Examinantion_System.DTOS.Instructor;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ViewModels.Instructor;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        readonly IServiceInstructor serviceInstructor;
        public InstructorController(IServiceInstructor serviceInstructor)
        {
            this.serviceInstructor = serviceInstructor;
        }

        [HttpPost("AddInstructor")]
        public async Task<IActionResult> AddInstructort([FromBody] ViewModelInstructorForAdding model)
        {
            var dto = new DTOInstructorForAdding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceInstructor.AddInstructorAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpPut("UpdateInstructor")]
        public async Task<IActionResult> UpdateInstructor([FromBody] ViewModelInstructorForUpdating model)
        {
            if (model.Id == Guid.Empty)
                return BadRequest("Id is required");

            var dto = new DTOInstructorForUpdating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await serviceInstructor.UpdateInstructorAsync(dto);

            if (result.IsSuccess)
                return Ok(result);

            else
                return BadRequest(result);
        }
        [HttpDelete("DeleteInstructor/{Id}")]
        public async Task<IActionResult> DeleteInstructor(Guid Id)
        {
            var result = await serviceInstructor.DeleteInstructorAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllInstructors")]

        public async Task<IActionResult> GetAllInstructor([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceInstructor.GetAllInstructorAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);



            else
                return NotFound(result);
        }


        [HttpGet("GetInstructorById/{Id}")]
        public async Task<IActionResult> GetInstructorById(Guid Id)
        {
            var result = await serviceInstructor.GetInstructorByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}

