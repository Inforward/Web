using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Infrastructure.Extensions
{
    public static class FormatExtensions
    {
        public static string Pluralize(this string value, int count)
        {
            return count == 1 ? value : PluralizationService.CreateService(new CultureInfo("en-US")).Pluralize(value);
        }
    }
}
