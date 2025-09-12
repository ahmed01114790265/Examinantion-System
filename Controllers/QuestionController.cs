using Application.Examinantion_System.DTOS.Question;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
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
        readonly IMapper mapper;
        public QuestionController(IServiceQuestion serviceQuestion, IMapper mapper)
        {
            this.serviceQuestion = serviceQuestion;
            this.mapper = mapper;
        }

        [HttpPost("AddQuestion")]
        public async Task<ResponsViewModel<ViewModelQuestionForAdding>> AddQuestion([FromBody] ViewModelQuestionForAdding model)
        {
            var dto = mapper.Map<DTOQuestionForAdding>(model);
            var result = await serviceQuestion.AddQuestionAsync(dto);

            return new ResponsViewModel<ViewModelQuestionForAdding>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpPut("UpdateQuestion")]
        public async Task<ResponsViewModel<ViewModelQuestionForUpdating>> UpdateQuestion([FromBody] ViewModelQuestionForUpdating model)
        {
            var dto = mapper.Map<DTOQuestionForUpdating>(model);
            var result = await serviceQuestion.UpdateQuestionAsync(dto);

            return new ResponsViewModel<ViewModelQuestionForUpdating>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteQuestion/{Id}")]
        public async Task<ResponsViewModel<Guid>> DeleteQuestion(Guid Id)
        {
            var result = await serviceQuestion.DeleteQuestionAsync(Id);
            return new ResponsViewModel<Guid>
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllQuestion")]

        public async Task<ResponsViewModel<PaginationModel<ViewModelQuestion>>> GetAllQuestion([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceQuestion.GetAllQuestionAsync(PageNumber, PageSize);

            var mappedData = mapper.Map<PaginationModel<ViewModelQuestion>>(result.Data);

            return new ResponsViewModel<PaginationModel<ViewModelQuestion>>
                {
                Data = result.IsSuccess ? mappedData : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }


        [HttpGet("GetQuestionById/{Id}")]
        public async Task<ResponsViewModel<ViewModelQuestion>> GetQuestionById(Guid Id)
        {
            var result = await serviceQuestion.GetQuestionByIdAsync(Id);

            var mappedData = mapper.Map<ViewModelQuestion>(result.Data);
            return new ResponsViewModel<ViewModelQuestion>
            {
                Data = result.IsSuccess ? mappedData : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };


        }
    }
}

