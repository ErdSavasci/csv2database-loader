using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLoader.App_Code
{
    class UniqueIndex
    {
        private static long uniqueIndex = 1;
        private static long beginValue = 1;

        public static void SetBeginningValue(long bValue)
        {
            uniqueIndex = beginValue;
            beginValue = bValue;
        }

        public static string GetUniqueIndex()
        {
            return uniqueIndex.ToString();
        }

        public static void IncreaseUniqueIndex()
        {
            uniqueIndex++;
        }

        public static void ResetUniqueIndex()
        {
            uniqueIndex = beginValue;
        }
    }
}
