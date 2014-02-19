using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DTTraceFileHelper.Helpers
{
    public static class HelperMethods
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        /// <summary>
        /// Returns string of specified character value repeated x times
        /// </summary>
        /// <param name="characterToRepeat">Character To Repeat</param>
        /// <param name="numberOfRepetitions">Number of repetitions of provided character</param>
        /// <returns>String</returns>
        public static string GenerateRepeatCharacter(char characterToRepeat, int numberOfRepetitions)
        {
            if (numberOfRepetitions < 0)
            {
                throw new  InvalidOperationException(String.Format("numberOfRepetitions {0} cannot be less than 1",numberOfRepetitions));
            }

            var stringToReturn = "";

            for (var i = 0; i < numberOfRepetitions; i++)
            {
                stringToReturn += characterToRepeat;
            }

            return stringToReturn;
        }

        public static string TruncateOrPad(this string stringToFormat, int specifiedLength, char charToPad = ' ')
        {
            string resultString;
            var stringLength = stringToFormat.Length;

            if (stringLength > specifiedLength)
            {
                resultString = stringToFormat.Substring(0, specifiedLength);
            }
            else
            {
                resultString = stringToFormat +
                               GenerateRepeatCharacter(charToPad, specifiedLength - stringLength);
            }

            return resultString;
        }
    }
}
