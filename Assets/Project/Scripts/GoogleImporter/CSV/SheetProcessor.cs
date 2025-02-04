using UnityEngine;

namespace Project.Scripts.GoogleImporter.CSV
{
    public class SheetProcessor
    {
        private const char _cellSeporator = ',';
        private const char _inCellSeporator = ';';

        private int ParseInt(string s)
        {
            int result = -1;
            if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't parse int, wrong text");
            }

            return result;
        }
    
        private float ParseFloat(string s)
        {
            float result = -1;
            if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.Log("Can't pars float,wrong text ");
            }

            return result;
        }
    
        private char GetPlatformSpecificLineEnd()
        {
            char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
            return lineEnding;
        }
    }
}