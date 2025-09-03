using Application.Examinantion_System.DTOS.Course;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ViewModels.Course;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        readonly IServiceCourse serviceCourse;
        public CourseController(IServiceCourse serviceCourse)
        {
            this.serviceCourse = serviceCourse;
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] ViewModelCourseForAdding model)
        {
            var dto = new DTOCourseForAdding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceCourse.AddCourseAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpPut("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] ViewModelCourseForUpdating model)
        {
            if (model.Id == Guid.Empty)
                return BadRequest("Id is required");

            var dto = new DTOCourseForUpdating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await serviceCourse.UpdateCourseAsync(dto);

            if (result.IsSuccess)
                return Ok(result);

            else
                return BadRequest(result);
        }
        [HttpDelete("DeleteCourse/{Id}")]
        public async Task<IActionResult> DeleteCourse(Guid Id)
        {
            var result = await serviceCourse.DeleteCourseAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllCourse")]

        public async Task<IActionResult> GetAllCourse([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceCourse.GetAllCourseAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);



            else
                return NotFound(result);
        }


        [HttpGet("GetCourseById/{Id}")]
        public async Task<IActionResult> GetCourseById(Guid Id)
        {
            var result = await serviceCourse.GetCourseByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}

