using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Utilities.GenricCaseNumber
{
    public static class Utility
    {
        static string PaddingSequence(int sequencePadding, int paddingNumber)
        {
            // string used in Format() method
            string s = "{0:";
            for (int i = 0; i < sequencePadding; i++)
            {
                s += "0";
            }
            s += "}";
            // use of string.Format() method
            string paddingValue = string.Format(s, paddingNumber);
            return paddingValue;
        }
        public static string GenerateUniqueSerial(string prefix, string dateFormat, int lastSequence, out int lastGeneratedSequence, int sequencePadding = 5)//
        {
            lastSequence = lastSequence+1;
            lastGeneratedSequence = lastSequence;

            StringBuilder sbUniqueueNumber = new StringBuilder();

            if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(dateFormat) && lastSequence > 0)
            {
                sbUniqueueNumber.Append(prefix);
                sbUniqueueNumber.Append(DateTime.Now.ToString(dateFormat));
                sbUniqueueNumber.Append(PaddingSequence(sequencePadding, lastSequence));
            }
            else if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(dateFormat))
            {
                sbUniqueueNumber.Append(prefix);
                sbUniqueueNumber.Append(DateTime.Now.ToString(dateFormat));
            }
            else if (!string.IsNullOrEmpty(prefix) && lastSequence > 0)
            {
                sbUniqueueNumber.Append(prefix);
                sbUniqueueNumber.Append(PaddingSequence(sequencePadding, lastSequence));
            }
            else if (!string.IsNullOrEmpty(dateFormat) && lastSequence > 0)
            {
                sbUniqueueNumber.Append(DateTime.Now.ToString(dateFormat));
            }
            return sbUniqueueNumber.ToString();
        }
    }
}

