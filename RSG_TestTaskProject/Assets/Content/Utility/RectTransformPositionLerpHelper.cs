using UnityEngine;

namespace Utility
{
    public static class RectTransformExtensions
    {
        // метод для вычисления крайних позиций для RectTransform-элемента
        public static void CalculatePositions(this RectTransform rectTransform, out float leftmostPosition, out float rightmostPosition)
        {
            leftmostPosition = rectTransform.anchoredPosition.x - (rectTransform.rect.width * rectTransform.pivot.x);
            rightmostPosition = leftmostPosition + rectTransform.rect.width;
        }
    }

    public class RectTransformPositionLerpHelper : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform; // ссылка на компонент RectTransform, по умолчанию равна текущему объекту, если не назначена

        [SerializeField] private RectTransform leftPoint;
        [SerializeField] private RectTransform rightPoint;

        private float leftmostPosition; // крайняя левая позиция
        private float rightmostPosition; // крайняя правая позиция

        private void Awake()
        {
            // если компонент RectTransform не назначен, то используем текущий объект
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            // вычисляем крайние позиции при старте скрипта
            //rectTransform.CalculatePositions(out leftmostPosition, out rightmostPosition);

            leftmostPosition = leftPoint.position.x;
            rightmostPosition = rightPoint.position.x;
        }

        // метод для получения позиции по коэффициенту лерпа от 0 до 1
        public Vector2 GetPosition(float value)
        {
            float x = Mathf.Lerp(leftmostPosition, rightmostPosition, value);
            return new Vector2(x, rectTransform.position.y); // возвращаем позицию в виде Vector2
        }

        // метод для назначения позиции конкретному элементу по коэффициенту лерпа от 0 до 1
        public void SetPositionX(RectTransform item, float value)
        {
            item.position = new Vector2(Mathf.Lerp(leftmostPosition, rightmostPosition, value), rectTransform.position.y);
        }

        public void SetPositionX(Transform item, float value)
        {
            SetPositionX(item.GetComponent<RectTransform>(), value);
        }
    }

}
