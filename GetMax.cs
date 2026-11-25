using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace delegates
{
   
    public static class EnumerableExtensions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber)
            where T : class
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (convertToNumber == null)
                throw new ArgumentNullException(nameof(convertToNumber));

            T maxItem = null;
            float maxValue = float.MinValue;
            bool found = false;

            foreach (var item in collection)
            {
                if (item == null) continue;

                float value = convertToNumber(item);
                if (!found || value > maxValue)
                {
                    maxValue = value;
                    maxItem = item;
                    found = true;
                }
            }

            if (!found)
                throw new InvalidOperationException("Коллекция пуста или все элементы = null");

            return maxItem;
        }
    }
}