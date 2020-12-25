using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCRM.Utilities
{
    public static class SelectListExtension
    {
        public static List<SelectListItem> Create<TEnum>(bool withEmptyOption = false, string emptyOptionString = "")
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException("Object must be Enum", nameof(type));
            var result = Enum.GetValues(type).Cast<TEnum>().Select(value => new SelectListItem(value.ToString(), value.ToString())).ToList();
            if (withEmptyOption)
                result.Insert(0, new SelectListItem(emptyOptionString, ""));
            return result;
        }
    }
}
