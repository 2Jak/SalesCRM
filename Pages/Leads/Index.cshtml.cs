using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesCRM.Data;
using SalesCRM.Data.Models;

namespace SalesCRM.Pages.Leads
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly LeadsRepository _repository;
        public readonly List<SelectListItem> LeadProperties;
        public IEnumerable<Lead> Leads { get; set; }
        public Dictionary<string, bool> SortStateForColumn { get; set; }
        public string SearchState { get; set; }
        public string SearchProperty { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _dbContext = context;
            _repository = new LeadsRepository(_dbContext);
            LeadProperties = new List<SelectListItem>();
            SortStateForColumn = new Dictionary<string, bool>();
            string[] properties = typeof(Lead).GetProperties().Select(property => property.Name).ToArray();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                SortStateForColumn[properties[i]] = false;
                LeadProperties.Add(new SelectListItem { Value = properties[i], Text = Utilities.Localization.EnglishHebrew[properties[i]] });
            }             
        }

        

        public async Task OnGet(string sortBy = "ID", string searchWord = "", string searchProperty = "ID")
        {
            deserializeSessionData();
            if (searchWord != null)
                searchWord = searchWord.Trim();
            SearchProperty = searchProperty;
            SearchState = searchWord;
            if (string.IsNullOrWhiteSpace(searchWord))
                Leads = await _repository.GetSortedLeadsListByAsync(_repository.GetLeadsQuery(), sortBy, SortStateForColumn[sortBy]);
            else
                Leads = await _repository.GetSortedLeadsListByAsync(_repository.FilterResultsToQueriable(SearchState, SearchProperty), sortBy, SortStateForColumn[sortBy]);
            SortStateForColumn[sortBy] = !SortStateForColumn[sortBy];
            serializeSessionData();
        }

        private void deserializeSessionData()
        {
            byte[] SortStateForColumnRaw;
            if (HttpContext.Session.TryGetValue("SortStateForColumn", out SortStateForColumnRaw))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    stream.Write(SortStateForColumnRaw, 0, SortStateForColumnRaw.Length);
                    stream.Position = 0;
                    SortStateForColumn = formatter.Deserialize(stream) as Dictionary<string, bool>;
                }
            }
        }

        private void serializeSessionData()
        {
            byte[] rawData;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, SortStateForColumn);
                rawData = stream.ToArray();
            }
            HttpContext.Session.Set("SortStateForColumn", rawData);
        }
    }
}
