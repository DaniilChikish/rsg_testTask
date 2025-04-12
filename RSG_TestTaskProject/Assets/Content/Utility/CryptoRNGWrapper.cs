using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace Utility
{
    public class CryptoRNGWrapper
    {
        private RandomNumberGenerator generator;
        public CryptoRNGWrapper()
        {
            generator = new RNGCryptoServiceProvider();
        }
        public bool NextBool()
        {
            var bytes = new byte[2];
            generator.GetBytes(bytes);
            return BitConverter.ToBoolean(bytes, 0);
        }
        public int NextInt()
        {
            var bytes = new byte[4];
            generator.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public float NetFloat()
        {
            var bytes = new byte[4];
            generator.GetBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }
        public double NetDouble()
        {
            var bytes = new byte[8];
            generator.GetBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Float flom 0 to 1</returns>
        public float NextFussy()
        {
            var bytes = new byte[8];
            generator.GetBytes(bytes);
            return Mathf.InverseLerp(ulong.MinValue, ulong.MaxValue, BitConverter.ToUInt64(bytes, 0));
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="min">Include</param>
        /// <param name="max">Exclude</param>
        /// <returns>Return int in range</returns>
        public int NextInt(int min, int max)
        {
            return Mathf.FloorToInt(Mathf.Lerp(min, max, NextFussy()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">Include</param>
        /// <param name="max">Include</param>
        /// <returns>Return float in range</returns>
        public float NextFloat(float min, float max)
        {
            return Mathf.Lerp(min, max, NextFussy());
        }
    }
}
