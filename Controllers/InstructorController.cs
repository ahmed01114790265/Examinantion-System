using Application.Examinantion_System.DTOS.Instructor;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
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
        readonly IMapper mapper;
        public InstructorController(IServiceInstructor serviceInstructor,IMapper mapper)
        {
            this.serviceInstructor = serviceInstructor;
            this.mapper = mapper;
        }

        [HttpPost("AddInstructor")]
        public async Task<ResponsViewModel<ViewModelInstructorForAdding>> AddInstructort([FromBody] ViewModelInstructorForAdding model)
        {
            var dto = mapper.Map<DTOInstructorForAdding>(model);
            var result = await serviceInstructor.AddInstructorAsync(dto);
            return new ResponsViewModel<ViewModelInstructorForAdding>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };

        }
        [HttpPut("UpdateInstructor")]
        public async Task<ResponsViewModel<ViewModelInstructorForUpdating>> UpdateInstructor([FromBody] ViewModelInstructorForUpdating model)
        {

            var dto = mapper.Map<DTOInstructorForUpdating>(model);
            var result = await serviceInstructor.UpdateInstructorAsync(dto);

            return new ResponsViewModel<ViewModelInstructorForUpdating>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteInstructor/{Id}")]
        public async Task<ResponsViewModel<Guid>> DeleteInstructor(Guid Id)
        {
            var result = await serviceInstructor.DeleteInstructorAsync(Id);
            return new ResponsViewModel<Guid>
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllInstructors")]

        public async Task<ResponsViewModel<PaginationModel<ViewModelInstructor>>> GetAllInstructor([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceInstructor.GetAllInstructorAsync(PageNumber, PageSize);
            var mappedData = mapper.Map<PaginationModel<ViewModelInstructor>>(result.Data);
            return new ResponsViewModel<PaginationModel<ViewModelInstructor>>
            {
                Data = result.IsSuccess ? mappedData : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }


        [HttpGet("GetInstructorById/{Id}")]
        public async Task<ResponsViewModel<ViewModelInstructor>> GetInstructorById(Guid Id)
        {
            var result = await serviceInstructor.GetInstructorByIdAsync(Id);
            var mappedData = mapper.Map<ViewModelInstructor>(result.Data);
            return new ResponsViewModel<ViewModelInstructor>
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

