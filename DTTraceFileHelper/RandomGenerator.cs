using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTraceFileHelper
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int minValue,int maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new IndexOutOfRangeException(String.Format("maxValue provided ({0}) cannot be less than 1",maxValue));
            }

            var result = _random.Next(minValue, maxValue);
            return result;
        }
    }
}
