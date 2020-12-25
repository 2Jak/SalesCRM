using Microsoft.EntityFrameworkCore;
using SalesCRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SalesCRM.Data
{
    public class LeadsRepository
    {
        private ApplicationDbContext _dbContext;

        public LeadsRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }


        public Lead GetLeadById(string id)
        {
            Lead lead = _dbContext.Leads.Single(lead => lead.ID == id);
            _dbContext.Entry(lead).Collection(lead => lead.FreeTexts).Load();
            return lead;
        }

        public async Task<List<Lead>> GetPaginatedLeadsListAsync(int currentPage, int pageSize = 5)
        {
            var leads = await _dbContext.Leads.ToListAsync();
            return leads.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Lead>> GetLeadsAsync()
        {
            return await _dbContext.Leads.ToListAsync();
        }

        public IQueryable<Lead> GetLeadsQuery()
        {
            return _dbContext.Leads.AsQueryable();
        }

        public async Task<List<Lead>> GetSortedLeadsListByAsync(IQueryable<Lead> leadsQuery, string property, bool asccending)
        {
            string sortQuery = $"{property} " + (asccending ? "ASC" : "DESC");
            leadsQuery = leadsQuery.OrderBy(sortQuery);
            return await leadsQuery.AsNoTracking().ToListAsync();
        }

        public async Task<List<Lead>> FilterResultsToListAsync(string filter, string property)
        {
            object convertedFilter = convertFilter(filter, property);
            if (property == "Male")
                return await _dbContext.Leads.AsQueryable().Where(lead => lead.Male == (bool?)convertedFilter).ToListAsync();
            var emergedPredicate = DynamicExpressions.DynamicExpressions.GetPredicate<Lead>(property, DynamicExpressions.FilterOperator.Equals, convertedFilter);
            if (convertedFilter is string)
                emergedPredicate = DynamicExpressions.DynamicExpressions.GetPredicate<Lead>(property, DynamicExpressions.FilterOperator.Contains, convertedFilter);
            return await _dbContext.Leads.AsQueryable().Where(emergedPredicate).ToListAsync();
        }

        public IQueryable<Lead> FilterResultsToQueriable(string filter, string property)
        {
            object convertedFilter = convertFilter(filter, property);
            if (property == "Male")
                return _dbContext.Leads.AsQueryable().Where(lead => lead.Male == (bool?)convertedFilter);
            var emergedPredicate = DynamicExpressions.DynamicExpressions.GetPredicate<Lead>(property, DynamicExpressions.FilterOperator.Equals, convertedFilter); 
            if (convertedFilter is string)
                emergedPredicate = DynamicExpressions.DynamicExpressions.GetPredicate<Lead>(property, DynamicExpressions.FilterOperator.Contains, convertedFilter);
            return _dbContext.Leads.AsQueryable().Where(emergedPredicate);
        }

        private object convertFilter(string filter, string property)
        {
            switch(property)
            {
                case "ID": case "Name": case "Address": case "Phonenumber": case "SecondaryPhonenumber": case "Email": case "DesiredCourse":
                    return filter;
                case "Male":
                    return Utilities.Localization.GenderStringToBool[filter];
                case "Status":
                    return Enum.Parse(typeof(Lead.StatusEnum), filter, true);
                case "LastUpdate":
                    return DateTime.Parse(filter);
                default:
                    return Convert.ToBoolean(filter);
            }
        }
    }
}
