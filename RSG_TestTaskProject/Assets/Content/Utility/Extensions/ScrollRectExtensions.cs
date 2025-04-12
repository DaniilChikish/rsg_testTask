using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.Extensions
{
    public static class ScrollRectExtensions
    {
        public static bool NeedHorizontalScrollbar(this ScrollRect value)
        {
            if (value == null) return false;
            var contentCorners = new Vector3[4];
            var scrollCorners = new Vector3[4];
            value.content.GetWorldCorners(contentCorners);
            ((RectTransform) value.transform).GetWorldCorners(scrollCorners);

            //если контент меньше скролла тогда скроллбар не нужен
            if (scrollCorners[3].x - scrollCorners[0].x >= contentCorners[3].x - contentCorners[0].x)
                return false;
            return true;
        }
        
        public static bool NeedVerticalScrollbar(this ScrollRect value)
        {
            if (value == null) return false;
            var contentCorners = new Vector3[4];
            var scrollCorners = new Vector3[4];
            value.content.GetWorldCorners(contentCorners);
            ((RectTransform) value.transform).GetWorldCorners(scrollCorners);

            //если контент меньше скролла тогда скроллбар не нужен
            if (scrollCorners[1].y - scrollCorners[0].y >= contentCorners[1].y - contentCorners[0].y)
                return false;
            return true;
        }

        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            Vector2 contentSize = scrollView.content.rect.size;
            Vector2 viewportSize = ((RectTransform)scrollView.content.parent).rect.size;
            Vector2 contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            Vector2 scrollPosition = scrollView.normalizedPosition;
            if (scrollView.horizontal && contentSize.x > viewportSize.x)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical && contentSize.y > viewportSize.y)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));

            Vector2 contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);

            return scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
        }

        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint);
        }

        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item);
        }

        private static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed, Action whileAction, Action onComplete)
        {
            Vector2 initialNormalizedPos = scrollView.normalizedPosition;
            float time = 0f;
            whileAction?.Invoke();
            while (time <= 1f)
            {
                whileAction?.Invoke();
                scrollView.normalizedPosition = Vector2.LerpUnclamped(initialNormalizedPos, targetNormalizedPos, time);

                yield return null;
                time += speed * Time.unscaledDeltaTime;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
            onComplete?.Invoke();
        }

        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed, null, null);
        }

        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed, null, null);
        }

        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed, Action onComplete)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed, null, onComplete);
        }
        
        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed, Action whileAction, Action onComplete)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed, whileAction, onComplete);
        }
    }
}