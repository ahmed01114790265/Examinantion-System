using Application.Examinantion_System.DTOS.Choice;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Examinantion_System.ResponsViewModel;
using Examinantion_System.ViewModels.Choice;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        readonly IServiceChoice serviceChoice;
        public ChoiceController(IServiceChoice serviceChoicet)
        {
            this.serviceChoice = serviceChoice;
        }

        [HttpPost("AddChoice")]
        public async Task<ResponsViewModel<ViewModelChoiceForAdding>> AddChoice([FromBody] ViewModelChoiceForAdding model)
        {
            var dto = new DTOChoiceForAdding()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow
            };
            var result = await serviceChoice.AddChoiceAsync(dto);

           
                return new ResponsViewModel<ViewModelChoiceForAdding>()
                {
                    Data = result.IsSuccess ? model : null,
                    IsSuccess = result.IsSuccess,
                    Massage = result.Message,
                    Errors = result.Errors,
                    Status = result.Status,
                };
         
        }
        [HttpPut("UpdateChoice")]
        public async Task<ResponsViewModel<ViewModelChoiceForUpdating>> UpdateChoice([FromBody] ViewModelChoiceForUpdating model)
        {
       
            var dto = new DTOChoiceForUpdating()
            {
                Name = model.Name,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await serviceChoice.UpdateChoiceAsync(dto);

            return new ResponsViewModel<ViewModelChoiceForUpdating>()
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteChoice/{Id}")]
        public async Task<IActionResult> DeleteChoice(Guid Id)
        {
            var result = await serviceChoice.DeleteChoiceAsync(Id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetAllChoices")]

        public async Task<IActionResult> GetAllChoices([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceChoice.GetAllChoiceAsync(PageNumber, PageSize);

            if (result.IsSuccess)
                return Ok(result);

            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);

            else
                return NotFound(result);
        }


        [HttpGet("GetChoiceById/{Id}")]
        public async Task<IActionResult> GetChoiceById(Guid Id)
        {
            var result = await serviceChoice.GetChoiceByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            else if (result.Errors != null && result.Errors.Any())
                return BadRequest(result);
            else
                return NotFound(result);


        }
    }
}
