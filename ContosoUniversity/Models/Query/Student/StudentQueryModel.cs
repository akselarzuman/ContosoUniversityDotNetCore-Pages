using ContosoUniversity.Models.Result.Student;
using MediatR;

namespace ContosoUniversity.Models.Query.Student
{
    public class StudentQueryModel : IRequest<StudentQueryResult>
    {
        public string SortOrder { get; set; }
        
        public string CurrentFilter { get; set; }
        
        public string SearchString { get; set; }
        
        public int? Page { get; set; }
    }
}