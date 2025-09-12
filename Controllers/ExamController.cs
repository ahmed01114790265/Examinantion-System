using Application.Examinantion_System.DTOS.Exam;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
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
        readonly IMapper mapper;
        public ExamController(IServiceExam serviceExam,IMapper mapper)
        {
            this.serviceExam = serviceExam;
            this.mapper = mapper;
        }

        [HttpPost("AddExam")]
        public async Task<ResponsViewModel<ViewModelExamForAdding>> AddExam([FromBody] ViewModelExamForAdding model)
        {
            var dto = mapper.Map<DTOExamForAdding>(model);
            var result = await serviceExam.AddExamAsync(dto);

            return new ResponsViewModel<ViewModelExamForAdding>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };  

        }
        [HttpPut("UpdateExam")]
        public async Task<ResponsViewModel<ViewModelExamForUpdating>> UpdateExam([FromBody] ViewModelExamForUpdating model)
        {

            var dto = mapper.Map<DTOExamForUpdating>(model);
            var result = await serviceExam.UpdateExamAsync(dto);

            return new ResponsViewModel<ViewModelExamForUpdating>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteExam/{Id}")]
        public async Task<ResponsViewModel<Guid>> DeleteExam(Guid Id)
        {
            var result = await serviceExam.DeleteExamAsync(Id);

            return new ResponsViewModel<Guid>
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllExam")]

        public async Task<ResponsViewModel<PaginationModel<ViewModelExam>>> GetAllExam([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceExam.GetAllExamAsync(PageNumber, PageSize);

            var mappedData = mapper.Map<PaginationModel<ViewModelExam>>(result.Data);
            return new ResponsViewModel<PaginationModel<ViewModelExam>>
            {
                Data = result.IsSuccess ? mappedData : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }


        [HttpGet("GetExamById/{Id}")]
        public async Task<ResponsViewModel<ViewModelExam>> GetExamById(Guid Id)
        {
            var result = await serviceExam.GetExamByIdAsync(Id);

            var mappedData = mapper.Map<ViewModelExam>(result.Data);
            return new ResponsViewModel<ViewModelExam>
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
