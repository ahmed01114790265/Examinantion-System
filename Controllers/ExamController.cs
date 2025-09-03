using Application.Examinantion_System.DTOS.Exam;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ViewModels.Exam;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        readonly IServiceExam serviceExam;
        public ExamController(IServiceExam serviceExam)
        {
            this.serviceExam = serviceExam;
        }

        [HttpPost("AddExam")]
        public async Task<IActionResult> AddExam([FromBody] ViewModelExamForAdding model)
        {
            var dto = new DTOExamForAdding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceExam.AddExamAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpPut("UpdateExam")]
        public async Task<IActionResult> UpdateExam([FromBody] ViewModelExamForUpdating model)
        {
            if (model.Id == Guid.Empty)
                return BadRequest("Id is required");

            var dto = new DTOExamForUpdating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await serviceExam.UpdateExamAsync(dto);

            if (result.IsSuccess)
                return Ok(result);

            else
                return BadRequest(result);
        }
        [HttpDelete("DeleteExam/{Id}")]
        public async Task<IActionResult> DeleteExam(Guid Id)
        {
            var result = await serviceExam.DeleteExamAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllExam")]

        public async Task<IActionResult> GetAllExam([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceExam.GetAllExamAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);



            else
                return NotFound(result);
        }


        [HttpGet("GetExamById/{Id}")]
        public async Task<IActionResult> GetExamById(Guid Id)
        {
            var result = await serviceExam.GetExamByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}
