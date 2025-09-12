using Application.Examinantion_System.DTOS.Course;
using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
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
        readonly IMapper mapper;
        public CourseController(IServiceCourse serviceCourse,IMapper mapper)
        {
            this.serviceCourse = serviceCourse;
            this.mapper = mapper;
        }

        [HttpPost("AddCourse")]
        public async Task<ResponsViewModel<ViewModelCourseForAdding>> AddCourse([FromBody] ViewModelCourseForAdding model)
        {
            var dto = mapper.Map<DTOCourseForAdding>(model);  
            var result = await serviceCourse.AddCourseAsync(dto);
            return new ResponsViewModel<ViewModelCourseForAdding>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpPut("UpdateCourse")]
        public async Task<ResponsViewModel<ViewModelCourseForUpdating>> UpdateCourse([FromBody] ViewModelCourseForUpdating model)
        {
            var dto = mapper.Map<DTOCourseForUpdating>(model);
            var result = await serviceCourse.UpdateCourseAsync(dto);
            return new ResponsViewModel<ViewModelCourseForUpdating>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteCourse/{Id}")]
        public async Task<ResponsViewModel<Guid>> DeleteCourse(Guid Id)
        {
            var result = await serviceCourse.DeleteCourseAsync(Id);

            return new ResponsViewModel<Guid>
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllCourse")]

        public async Task<ResponsViewModel<PaginationModel<ViewModelCourse>>> GetAllCourse([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceCourse.GetAllCourseAsync(PageNumber, PageSize);
            var mappedData = mapper.Map<PaginationModel<ViewModelCourse>>(result.Data);
            return new ResponsViewModel<PaginationModel<ViewModelCourse>>
            {
                Data = result.IsSuccess ? mappedData : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }


        [HttpGet("GetCourseById/{Id}")]
        public async Task<ResponsViewModel<ViewModelCourse>> GetCourseById(Guid Id)
        {
            var result = await serviceCourse.GetCourseByIdAsync(Id);
            var mappedData = mapper.Map<ViewModelCourse>(result.Data);
            return new ResponsViewModel<ViewModelCourse>
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

