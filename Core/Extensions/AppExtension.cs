using System;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class AppExtension
    {
        public static bool IsEmail(this string text)
        {
            return Regex.IsMatch(text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        }
        public static bool IsNumber(this string number)
        {
            return Regex.IsMatch(number, @"^((\+?994)?[ -]?([0-9]{2,3})[ -]?([0-9]{3})[ -]?([0-9]{2})[ -]?([0-9]{2}))$", RegexOptions.IgnoreCase);


        }
    }
}


