using Application.Examinantion_System.DTOS.Choice;
using Application.Examinantion_System.DTOS.Course;
using Application.Examinantion_System.DTOS.Exam;
using Application.Examinantion_System.DTOS.Instructor;
using Application.Examinantion_System.DTOS.Question;
using Application.Examinantion_System.DTOS.Student;
using AutoMapper;
using Examinantion_System.ViewModels.choic;
using Examinantion_System.ViewModels.Course;
using Examinantion_System.ViewModels.Exam;
using Examinantion_System.ViewModels.Instructor;
using Examinantion_System.ViewModels.Question;
using Examinantion_System.ViewModels.Student;

namespace Examinantion_System.MappingProfil
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            CreateMap<DTOChoiceForAdding, ViewModelChoiceForAdding>();
            CreateMap<ViewModelChoiceForAdding, DTOChoiceForAdding>();
            CreateMap<DTOChoiceForUpdating, ViewModelChoiceForUpdating>();
            CreateMap<ViewModelChoiceForUpdating, DTOChoiceForUpdating>();

            CreateMap<DTOCourseForAdding, ViewModelCourseForAdding>();
            CreateMap<ViewModelCourseForAdding, DTOCourseForAdding>();
            CreateMap<DTOCourseForUpdating, ViewModelCourseForUpdating>();
            CreateMap<ViewModelCourseForUpdating, DTOCourseForUpdating>();

            CreateMap<DTOExamForAdding, ViewModelExamForAdding>();
            CreateMap<ViewModelExamForAdding, DTOExamForAdding>();
            CreateMap<DTOExamForUpdating, ViewModelExamForUpdating>();
            CreateMap<ViewModelExamForUpdating, DTOExamForUpdating>();

            CreateMap<DTOQuestionForAdding, ViewModelQuestionForAdding>();
            CreateMap<ViewModelQuestionForAdding, DTOQuestionForAdding>();
            CreateMap<DTOQuestionForUpdating, ViewModelQuestionForUpdating>();
            CreateMap<ViewModelQuestionForUpdating, DTOQuestionForUpdating>();

            CreateMap<DTOInstructorForAdding, ViewModelInstructorForAdding>();
            CreateMap<ViewModelInstructorForAdding, DTOInstructorForAdding>();
            CreateMap<DTOInstructorForUpdating, ViewModelInstructorForUpdating>();
            CreateMap<ViewModelInstructorForUpdating, DTOInstructorForUpdating>();

            CreateMap<DTOStudentFor_Adding, ViewmodelStudentForAdding>();
            CreateMap<ViewmodelStudentForAdding, DTOStudentFor_Adding>();
            CreateMap<DTOStudentFor_Updating, ViewmodelStudentForUpdating>();
            CreateMap<ViewmodelStudentForUpdating, DTOStudentFor_Updating>();


        }
    }
}
