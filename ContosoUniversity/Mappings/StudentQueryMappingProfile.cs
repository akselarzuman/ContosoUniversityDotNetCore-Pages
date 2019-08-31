using AutoMapper;
using ContosoUniversity.Models;
using ContosoUniversity.Models.Result.Student;

namespace ContosoUniversity.Mappings
{
    public class StudentQueryMappingProfile : Profile
    {
        public StudentQueryMappingProfile() => CreateMap<Student, StudentQueryResult>();
    }
}