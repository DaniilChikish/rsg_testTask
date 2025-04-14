using System.Text.RegularExpressions;

namespace Utility
{
    public static class StringValidator
    {
        public static string FormatName(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            //убираем все пробелы в начале строки
            //убираем все кроме букв, цифр, пробелов и этих символов _,=-.
            input = Regex.Replace(input, @"^[ \s]+|[^\p{L}\p{N} _,=\-\.]", "", RegexOptions.None);
            //заменяем больше чем 1 пробел на 1 пробел
            input = Regex.Replace(input, @"\s{2,}", " ", RegexOptions.None);
            return input;
        }
    }
}
