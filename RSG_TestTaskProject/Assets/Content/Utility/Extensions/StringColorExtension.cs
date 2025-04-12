namespace Utility.Extensions
{
    public static class StringColorExtension
    {
        public static string ColorTag(this int i, string hex) => i.ToString().ColorTag(hex);
        public static string ColorTag(this string text, string hex) => $"<color={hex}>{text}</color>";



        public static string ToRed(this string str) => $"<color=red>{str}</color>";
        public static string ToBlue(this string str) => $"<color=blue>{str}</color>";
        public static string ToYellow(this string str) => $"<color=#e49600>{str}</color>";
        public static string ToOrange(this string str) => $"<color=orange>{str}</color>";
        public static string ToGreen(this string str) => $"<color=#0AA22E>{str}</color>";
        public static string ToCyan(this string str) => $"<color=#00FFFF>{str}</color>";


        public static string ToLightRed(this int i) => i.ToString().ToLightRed();
        public static string ToLightRed(this string str) => $"<color=#FF7766>{str}</color>";

        public static string ToWhite(this string str) => $"<color=white>{str}</color>";

        public static string ToGreenPrice(this string str) => $"<color=#78D056>{str}</color>";
        public static string ToRedPrice(this string str) => $"<color=#FF7058>{str}</color>";
    }

    public static class HexColor
    {
        public const string DarkYellow = "#e49600";
    }
}
