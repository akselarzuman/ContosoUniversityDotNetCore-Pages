using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Data;
using ContosoUniversity.Models.Query.Student;
using ContosoUniversity.Models.Result.Student;
using ContosoUniversity.Pages;
using MediatR;

namespace ContosoUniversity.Handlers.Student
{
    public class StudenQueryHandler : IRequestHandler<StudentQueryModel, StudentQueryResult>
    {
        private readonly SchoolContext _db;
            private readonly IConfigurationProvider _configuration;

            public StudenQueryHandler(SchoolContext db, IConfigurationProvider configuration)
            {
                _db = db;
                _configuration = configuration;
            }

            public async Task<StudentQueryResult> Handle(StudentQueryModel message, CancellationToken token)
            {
                var model = new StudentQueryResult
                {
                    CurrentSort = message.SortOrder,
                    NameSortParm = String.IsNullOrEmpty(message.SortOrder) ? "name_desc" : "",
                    DateSortParm = message.SortOrder == "Date" ? "date_desc" : "Date",
                };

                if (message.SearchString != null)
                {
                    message.Page = 1;
                }
                else
                {
                    message.SearchString = message.CurrentFilter;
                }

                model.CurrentFilter = message.SearchString;
                model.SearchString = message.SearchString;

                IQueryable<Models.Student> students = _db.Students;
                if (!String.IsNullOrEmpty(message.SearchString))
                {
                    students = students.Where(s => s.LastName.Contains(message.SearchString)
                                                   || s.FirstMidName.Contains(message.SearchString));
                }

                switch (message.SortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        students = students.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default: // Name ascending 
                        students = students.OrderBy(s => s.LastName);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (message.Page ?? 1);
                model.Results = await students
                    //.Select(src => new Model
                    //{
                    //    ID = src.Id,
                    //    FirstMidName = src.FirstMidName,
                    //    LastName = src.LastName,
                    //    EnrollmentsCount = src.Enrollments.Count(),
                    //    EnrollmentDate = src.EnrollmentDate
                    //})
                    .ProjectTo<Model>(_configuration)
                    .PaginatedListAsync(pageNumber, pageSize);

                return model;
            }
    }
}