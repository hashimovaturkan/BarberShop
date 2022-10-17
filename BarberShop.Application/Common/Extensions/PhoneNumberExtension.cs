using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Services;
using BarberShop.Application.Models.Template;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarberShop.Application.Common.Extensions
{
    public static partial class Extension
    {
        public static string PhoneNumber(this string number)
        {
            //if (number.Contains("+994"))
            //    number = number.Substring(4, number.Length-4);

            //if (number.Contains("+48"))
            //    number = number.Substring(3, number.Length-3);

            number = number.Substring(1, number.Length - 1);

            return number;
        }
    }
}
