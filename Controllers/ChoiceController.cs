using Application.Examinantion_System.DTOS.Choice;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
using Examinantion_System.ViewModels.choic;
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
        readonly IMapper mapper;
        public ChoiceController(IServiceChoice serviceChoicet , IMapper mapper)
        {
            this.serviceChoice = serviceChoice;
            this.mapper = mapper;
        }

        [HttpPost("AddChoice")]
        public async Task<ResponsViewModel<ViewModelChoiceForAdding>> AddChoice([FromBody] ViewModelChoiceForAdding model)
        {
            var dto = mapper.Map<DTOChoiceForAdding>(model);
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

            var dto = mapper.Map<DTOChoiceForUpdating>(model);
            
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
        public async Task<ResponsViewModel<Guid>> DeleteChoice(Guid Id)
        {
            var result = await serviceChoice.DeleteChoiceAsync(Id);
            return new ResponsViewModel<Guid>()
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllChoices")]

        public async Task<ResponsViewModel<PaginationModel< ViewModelChoice>>> GetAllChoices([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceChoice.GetAllChoiceAsync(PageNumber, PageSize);
            return new ResponsViewModel<PaginationModel<ViewModelChoice>>()
            {
                Data = result.IsSuccess ? new PaginationModel<ViewModelChoice>(
               
                    result.Data.Data.Select(c => new ViewModelChoice
                    {
                            Name = c.Name,
                            Text = c.Text,

                    }),
                    result.Data.PageNumber,
                    result.Data.PageSize,
                    result.Data.TotalCount
                    ) : null,

                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,

            };


        }


        [HttpGet("GetChoiceById/{Id}")]
        public async Task<ResponsViewModel<ViewModelChoice>> GetChoiceById(Guid Id)
        {
            var result = await serviceChoice.GetChoiceByIdAsync(Id);
            return new ResponsViewModel<ViewModelChoice>()
            {
                Data = result.IsSuccess ? new ViewModelChoice
                {
                    Name = result.Data.Name,
                    Text = result.Data.Text,
                } : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };


        }
    }
}
