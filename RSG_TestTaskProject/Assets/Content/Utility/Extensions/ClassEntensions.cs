using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
    public static class DOTweenHelper
    {
        public static Color Transparent(this Color color)
        {
            return new Color(color.r, color.g, color.b, 0);
        }
        public static Color NotTransparent(this Color color)
        {
            return new Color(color.r, color.g, color.b, 1);
        }
        public static Color SetTransparent(this Color color, bool value)
        {
            return new Color(color.r, color.g, color.b, value ? 0 : 1);
        }
        public static void SetAlpha(this Image image, float a) =>
    image.color = new Color(image.color.r, image.color.g, image.color.b, a);

        public static Color SetAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);
    }
}
