using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Library.Extensions
{
    public static class StringExtension
    {
        public static string SafeTrim(this string input)
        {
            if (input.IsNotEmpty())
            {
                return input.Trim();
            }

            return input;
        }

        public static bool IsNotEmpty(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static string RemoveInvalidXmlChars(this string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }

        public static bool IsValidXmlString(this string text)
        {
            try
            {
                XmlConvert.VerifyXmlChars(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string RemoveNonAlphaNumeric(this string text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]");
            text = regex.Replace(text, string.Empty);
            return text;
        }
    }
}