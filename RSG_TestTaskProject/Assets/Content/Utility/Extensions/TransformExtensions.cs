using System.Collections.Generic;
using UnityEngine;

namespace Utility.Extensions
{
    public static class TransformExtensions
    {
        public static List<Transform> GetChilds(this Transform item)
        {
            var output = new List<Transform>();
            foreach (Transform child in item)
            {
                output.Add(child);
            }
            return output;
        }
        /// <summary>
        /// Destroy all children
        /// </summary>
        public static void DestroyChildren(this Transform item)
        {
            foreach (Transform child in item.GetChilds())
            {
                Object.Destroy(child.gameObject);
            }
        }
        public static void DestroyChildrenImmediate(this Transform item)
        {
            foreach (Transform child in item.GetChilds())
            {
                Object.DestroyImmediate(child.gameObject);
            }
        }
    }
}