using UnityEngine;

public static class TimeUtils
{
    public static string SecondsToCountdown (int totalSeconds)
    {
        int days, hours, minutes, seconds;

        seconds = Mathf.FloorToInt(totalSeconds % 60);
        totalSeconds -= seconds;

        minutes = Mathf.FloorToInt(totalSeconds / 60);
        minutes = minutes % 60;
        totalSeconds -= minutes * 60;

        hours = Mathf.FloorToInt(totalSeconds / (60 * 60));
        hours = hours % 24;
        totalSeconds -= hours * 60 * 60;

        days = Mathf.FloorToInt(totalSeconds / (60 * 60 * 24));
        
        return LeadingZeros(days) + days + ":" + 
            LeadingZeros(hours) + hours + ":" + 
            LeadingZeros(minutes) + minutes + ":" + 
            LeadingZeros(seconds) + seconds;
    }

    private static string LeadingZeros(int number)
    {
        return number >= 10 ? "" : "0";
    }
}
