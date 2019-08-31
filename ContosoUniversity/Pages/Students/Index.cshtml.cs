using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.Query.Student;
using ContosoUniversity.Models.Result.Student;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students
{
    public class Index : PageModel
    {
        private readonly IMediator _mediator;

        public Index(IMediator mediator) => _mediator = mediator;

        public StudentQueryResult Data { get; private set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
            => Data = await _mediator.Send(new StudentQueryModel {CurrentFilter = currentFilter, Page = pageIndex, SearchString = searchString, SortOrder = sortOrder});
    }
}