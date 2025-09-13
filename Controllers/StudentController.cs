using Application.Examinantion_System.DTOS.Student;
using Application.Examinantion_System.Interfaces.IServices;
using Application.Examinantion_System.PaginationModel;
using AutoMapper;
using Examinantion_System.ResponsViewModel;
using Examinantion_System.ViewModels.choic;
using Examinantion_System.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace Examinantion_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly IServiceStudent serviceStudent;
        readonly IMapper mapper;
        public StudentController(IServiceStudent serviceStudent,IMapper mapper)
        {
            this.serviceStudent = serviceStudent;
            this.mapper = mapper;
        }

        [HttpPost("AddStudent")]
        public async Task<ResponsViewModel<ViewmodelStudentForAdding>> AddStudent([FromBody] ViewmodelStudentForAdding model)
        {
            var dto = mapper.Map<DTOStudentFor_Adding>(model); 
            var result = await serviceStudent.AddStudentAsync(dto);
            return new ResponsViewModel<ViewmodelStudentForAdding>
            {

                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };


        }
        [HttpPut("UpdateStudent")]
        public async Task<ResponsViewModel<ViewmodelStudentForUpdating>> UpdateStudent([FromBody] ViewmodelStudentForUpdating model)
        {
            var dto = mapper.Map<DTOStudentFor_Updating>(model);
            var result = await serviceStudent.UpdateStudentAsync(dto);
            return new ResponsViewModel<ViewmodelStudentForUpdating>
            {
                Data = result.IsSuccess ? model : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }
        [HttpDelete("DeleteStudent/{Id}")]
        public async Task<ResponsViewModel<Guid>> DeleteStudent(Guid Id)
        {
            var result = await serviceStudent.DeleteStudentAsync(Id);
            return new ResponsViewModel<Guid>
            {
                Data = result.IsSuccess ? Id : Guid.Empty,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };
        }

        [HttpGet("GetAllStudents")]

        public async Task<ResponsViewModel<PaginationModel<ViewModelStudent>>> GetAllStudents([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var result = await serviceStudent.GetAllStudentsAsync(PageNumber, PageSize);
            return new ResponsViewModel<PaginationModel<ViewModelStudent>>()
            {
                Data = result.IsSuccess ? new PaginationModel<ViewModelStudent>(

                    result.Data.Data.Select(c => new ViewModelStudent
                    {
                        Name = c.Name

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


        [HttpGet("GetStudentById/{Id}")]
        public async Task<ResponsViewModel<ViewModelStudent>> GetStudentById(Guid Id)
        {
            var result = await serviceStudent.GetStudentByIdAsync(Id);
            return new ResponsViewModel<ViewModelStudent>()
            {
                Data = result.IsSuccess ? new ViewModelStudent
                {
                    Name = result.Data.Name
                } : null,
                IsSuccess = result.IsSuccess,
                Massage = result.Message,
                Errors = result.Errors,
                Status = result.Status,
            };



        }
    }
}
