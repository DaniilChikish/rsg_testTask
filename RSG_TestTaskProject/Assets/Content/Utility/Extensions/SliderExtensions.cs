using UnityEngine.UI;

namespace Utility.Extensions
{
    public static class SliderExtensions
    {
        public static void SetValue(this Slider obj, float value)
        {
            obj.value = value;
        }
    }
}