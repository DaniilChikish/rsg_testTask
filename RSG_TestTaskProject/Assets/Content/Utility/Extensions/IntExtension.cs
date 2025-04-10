
namespace Utility.Extensions
{
    public static class IntExtension
    {
        /// <summary>
        /// Конвертировать число в строку денежного типа. Пример: 9999999 в 9 999 999
        /// </summary>
        public static string ConvertToCashForm(this int number)
        {
            if (number == 0) return "0";

            return number.ToString("N0");
        }

        public static string ConvertToRoundCashForm(this int number)
        {
            // Пороги для скорочення числа
            float billion = 1000000000;
            float million = 1000000;
            float thousand = 1000;

            // Форматуємо число
            if (number >= (billion * 10) - 1)
                return $"{(number / billion):N1}B";
            else if (number >= (million * 10) - 1)
                return $"{(number / million):N1}M";
            else if (number >= (thousand * 10) - 1)
                return $"{(number / thousand):N1}K";
            else
                return number.ToString("N0");
        }
    }
}