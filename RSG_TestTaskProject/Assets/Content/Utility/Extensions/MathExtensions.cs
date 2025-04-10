using UnityEngine;

namespace Utility.Extensions
{

    /// <summary>
    /// Some extensions for values
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Is value is in range (includes)
        /// </summary>
        public static bool IsInRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Is value is in range (includes)
        /// </summary>
        public static bool IsInRange(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        public static Vector2Int FlipAxis(this Vector2Int vector)
        {
            return new Vector2Int(vector.y, vector.x);
        }
        public static Vector3 Round(this Vector3 vector)
        {
            return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
        }
        public static Vector3Int RoundToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }

        public static Vector3 Floor(this Vector3 vector)
        {
            return new Vector3(Mathf.Floor(vector.x), Mathf.Floor(vector.y), Mathf.Floor(vector.z));
        }
        public static Vector3Int FloorToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
        }
        public static Vector3 Ceil(this Vector3 vector)
        {
            return new Vector3(Mathf.Ceil(vector.x), Mathf.Ceil(vector.y), Mathf.Ceil(vector.z));
        }
        public static Vector3Int CeilToInt(this Vector3 vector)
        {
            return new Vector3Int(Mathf.CeilToInt(vector.x), Mathf.CeilToInt(vector.y), Mathf.CeilToInt(vector.z));
        }

        public static Vector3 Scale(this Vector3 vector, float x, float y, float z)
        {
            return new Vector3(vector.x * x, vector.y * y, vector.z * z);
        }
        public static Vector2 Scale(this Vector2 vector, float x, float y)
        {
            return new Vector2(vector.x * x, vector.y * y);
        }
        public static Vector3Int Scale(this Vector3Int vector, int x, int y, int z)
        {
            return new Vector3Int(vector.x * x, vector.y * y, vector.z * z);
        }
        public static Vector2Int Scale(this Vector2Int vector, int x, int y)
        {
            return new Vector2Int(vector.x * x, vector.y * y);
        }
        public static float[] AsArray(this Vector3 vector)
        {
            return new float[] { vector.x, vector.y, vector.z };
        }
        public static Vector3 ToVector3(this float[] arr)
        {
            if (arr.Length == 3) 
            {
                return new Vector3(arr[0], arr[1], arr[2]);
            }
            else if (arr.Length == 2)
            {
                return new Vector3(arr[0], arr[1]);
            }
            else 
            { 
                return Vector3.zero;
            }
        }
        public static int[] AsArray(this Vector3Int vector)
        {
            return new int[] { vector.x, vector.y, vector.z };
        }
        public static Vector3Int ToVector3(this int[] arr)
        {
            if (arr.Length == 3)
            {
                return new Vector3Int(arr[0], arr[1], arr[2]);
            }
            else if (arr.Length == 2)
            {
                return new Vector3Int(arr[0], arr[1]);
            }
            else
            {
                return Vector3Int.zero;
            }
        }
        public static float[] AsArray(this Vector2 vector)
        {
            return new float[] { vector.x, vector.y};
        }
        public static Vector2 ToVector2(this float[] arr)
        {
            if (arr.Length == 2)
            {
                return new Vector2(arr[0], arr[1]);
            }
            else
            {
                return Vector2.zero;
            }
        }
        public static int[] AsArray(this Vector2Int vector)
        {
            return new int[] { vector.x, vector.y};
        }
        public static Vector2Int ToVector2(this int[] arr)
        {
            if (arr.Length == 2)
            {
                return new Vector2Int(arr[0], arr[1]);
            }
            else
            {
                return Vector2Int.zero;
            }
        }

        public static float GoldenRatio => (1 + Mathf.Sqrt(5)) / 2;

        public static int FibonacciFunc(int n)
        {
            return Mathf.CeilToInt((Mathf.Pow(GoldenRatio, n) - Mathf.Pow(GoldenRatio, -n)) / Mathf.Sqrt(5));
        }
    }
}
