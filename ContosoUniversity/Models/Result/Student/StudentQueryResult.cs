using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.Result.Student
{
    public class StudentQueryResult
    {
        public string CurrentSort { get; set; }
        public string NameSortParm { get; set; }
        public string DateSortParm { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }

        public PaginatedList<Model> Results { get; set; }
    }
    
    public class Model
    {
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentsCount { get; set; }
    }
}