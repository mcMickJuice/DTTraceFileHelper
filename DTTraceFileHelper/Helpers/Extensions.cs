using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTraceFileHelper.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Selects a random element from a List Collection. If collection is empty, returns default value from Element Type
        /// </summary>
        /// <typeparam name="T">Return Type of Collection</typeparam>
        /// <param name="source"></param>
        /// <returns>Random Element select from Collection</returns>
        public static T SelectRandom<T>(this IList<T> source)
        {
            var elementCount = source.Count;

            if (elementCount == 0)
            {
                return default(T);
            }

            var randomNumber = RandomGenerator.GetRandomNumber(0, elementCount);
            return source.ElementAt(randomNumber);
        }
    }
}
