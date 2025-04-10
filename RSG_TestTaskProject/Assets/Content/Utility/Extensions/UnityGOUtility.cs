using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility.Extensions
{
    public static class UnityGOUtility
    {
        public static Vector2 Sum(this IEnumerable<Vector2> points)
        {
            var sum = Vector2.zero;
            foreach (var v in points)
            {
                sum += v;
            }
            return sum;
        }
        public static Vector3 Sum(this IEnumerable<Vector3> points)
        {
            var sum = Vector3.zero;
            foreach (var v in points)
            {
                sum += v;
            }
            return sum;
        }
        public static Vector4 Sum(this IEnumerable<Vector4> points)
        {
            var sum = Vector4.zero;
            foreach (var v in points)
            {
                sum += v;
            }
            return sum;
        }
        public static Vector2 MiddlePoint(IEnumerable<Vector2> points)
        {
            return points.Sum() / points.Count();
        }
        public static Vector3 MiddlePoint(IEnumerable<Vector3> points)
        {
            return points.Sum() / points.Count();
        }
        public static Vector4 MiddlePoint(IEnumerable<Vector4> points)
        {
            return points.Sum() / points.Count();
        }
        public static Vector3 MiddlePosition(IEnumerable<Component> components)
        {
            var sum = Vector3.zero;
            foreach (var v in components)
            {
                sum += v.transform.position;
            }
            return sum / components.Count();
        }
        public static Vector3 MiddleLocalPosition(IEnumerable<Component> components)
        {
            var sum = Vector3.zero;
            foreach (var v in components)
            {
                sum += v.transform.localPosition;
            }
            return sum / components.Count();
        }
        public static Bounds GetRectTransformBounds(RectTransform transform)
        { 
            Vector3[] worldCorners = new Vector3[4];
            transform.GetWorldCorners(worldCorners);
            Bounds bounds = new Bounds(worldCorners[0], Vector3.zero);
            for (int i = 1; i < 4; ++i)
            {
                bounds.Encapsulate(worldCorners[i]);
            }
            return bounds;
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }    
            return component;
        }
    }

}
