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
    public class LeadModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly LeadsRepository _repository;
        [BindProperty]
        public Lead Lead { get; set; }

        public readonly List<SelectListItem> Statuses = Utilities.SelectListExtension.Create<Lead.StatusEnum>();
        public readonly List<SelectListItem> Courses;

        public LeadModel(ApplicationDbContext context)
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
                _dbContext.Entry(Lead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToPage();
            }
            return Page();   
        }

        public IActionResult OnPostUpdateFreeText(string text, int textId)
        {
            FreeText freeText = _dbContext.FreeTexts.Single(ft => ft.ID == textId);
            freeText.Text = text;
            _dbContext.SaveChanges();
            return RedirectToPage();
        }

        public void OnGet(string leadId, bool addFreeText = false)
        {
            Lead = _repository.GetLeadById(leadId);
            if (addFreeText)
            {
                Lead.LastUpdate = DateTime.Now;
                Lead.FreeTexts.Add(new FreeText());
                _dbContext.Entry(Lead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }
    }
}
