using Application.Examinantion_System.DTOS.Question;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ViewModels.Question;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        readonly IServiceQuestion serviceQuestion;
        public QuestionController(IServiceQuestion serviceQuestion)
        {
            this.serviceQuestion = serviceQuestion;
        }

        [HttpPost("AddQuestion")]
        public async Task<IActionResult> AddQuestion([FromBody] ViewModelQuestionForAdding model)
        {
            var dto = new DTOQuestionForAdding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceQuestion.AddQuestionAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
        [HttpPut("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion([FromBody] ViewModelQuestionForUpdating model)
        {
            if (model.Id == Guid.Empty)
                return BadRequest("Id is required");

            var dto = new DTOQuestionForUpdating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };
            var result = await serviceQuestion.UpdateQuestionAsync(dto);

            if (result.IsSuccess)
                return Ok(result);

            else
                return BadRequest(result);
        }
        [HttpDelete("DeleteQuestion/{Id}")]
        public async Task<IActionResult> DeleteQuestion(Guid Id)
        {
            var result = await serviceQuestion.DeleteQuestionAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllQuestion")]

        public async Task<IActionResult> GetAllQuestion([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceQuestion.GetAllQuestionAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);



            else
                return NotFound(result);
        }


        [HttpGet("GetQuestionById/{Id}")]
        public async Task<IActionResult> GetQuestionById(Guid Id)
        {
            var result = await serviceQuestion.GetQuestionByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}

