using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesCRM.Data;
using SalesCRM.Data.Models;

namespace SalesCRM.Pages.Leads
{
    public class AddLeadModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly LeadsRepository _repository;
        [BindProperty]
        public Lead Lead { get; set; }

        public readonly List<SelectListItem> Statuses = Utilities.SelectListExtension.Create<Lead.StatusEnum>();
        public readonly List<SelectListItem> Courses;

        public AddLeadModel(ApplicationDbContext context)
        {
            _dbContext = context;
            _repository = new LeadsRepository(_dbContext);
            Courses = _dbContext.Courses.Select(course => new SelectListItem { Value = course.Name, Text = course.Name }).ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Lead.LastUpdate = DateTime.Now;
                _dbContext.Leads.Add(Lead);
                _dbContext.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }

        public void OnGet()
        {
            Lead = new Lead();
        }
    }
}
