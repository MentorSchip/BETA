using UnityEngine;

namespace MentorSchip
{
    public static class Conversions
    {
        const int SECOND = 1;
        const int MINUTE = 60;
        const int HOUR = 3600;
        const int DAY = 86400;
        const int WEEK = 604800;
        const int MONTH = 2628000;
        const int YEAR = 31540000;

        public static int StringToInt(string text)
        {
            //Debug.Log(text);
            if (text == "1 hour")
                return HOUR;
            else if (text == "1 day")
                return DAY;
            else if (text == "1 week")
                return WEEK;
            else if (text == "1 month")
                return MONTH;
            else if (text == "1 year")
                return YEAR;
            else
                return MONTH;
        }
    }
}
