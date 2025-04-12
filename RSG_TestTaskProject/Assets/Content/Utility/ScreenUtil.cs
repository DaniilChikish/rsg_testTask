using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;

namespace Utility
{
    public class ScreenUtil
    {
        /// <summary>
        /// Возвращает значение для масштабирования канваса в зависимости от переданных параметров
        /// </summary>
        /// <param name="width"> ширина экрана</param>
        /// <param name="height"> высота экрана </param>
        /// <param name="scalerReferenceResolution"> референсное разрешение компонента <see cref="CanvasScaler"/>></param>
        /// <param name="scalerMatchWidthOrHeight">Параметр для масштабирования канваса в соответствии с шириной
        /// или высотой эталонного разрешения или их комбинацией <see cref="CanvasScaler"/></param>
        /// <returns></returns>
        public static float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
        {
            return Pow(width / scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight) *
                   Pow(height / scalerReferenceResolution.y, scalerMatchWidthOrHeight);
        }
    }
}