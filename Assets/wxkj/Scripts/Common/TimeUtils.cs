using UnityEngine;
using System;
using System.Collections;

public class TimeUtils
{
    // get the normal time
    public static DateTime GetNoralTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dtResult = dtStart.Add(toNow);
        return dtResult;
    }

    //get the stamp time
    public static string GetTimeStamp()
    {
        return ((System.DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
    }

    public static bool TimeMaxThanNow(string compareTime)
    {
        return int.Parse(compareTime) > int.Parse(((System.DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString()) ? true : false;
    }

    public static string GetNowTimeNormal()
    {
        return System.DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    public string convertTime(long startTime, long endTime)
    {
        string result = "";
        long gapTime = endTime - startTime;

        if (gapTime > 0)
        {
            TimeSpan tSpan = new TimeSpan(gapTime);
            if (tSpan.Days > 0)
            {
                result = string.Format("剩余 {0}天", tSpan.Days);
            }
            else
            {
                result = string.Format("{0}:{1}:{2}", tSpan.Hours.ToString("#00"), tSpan.Minutes.ToString("#00"), tSpan.Seconds.ToString("#00"));
            }
        }
        return result;
    }

    public static string GetStringDHMS(System.TimeSpan span, string formart = "{0}:{1}:{2}:{3}")
    {
        string d = span.Days.ToString().PadLeft(2);
        string h = span.Hours.ToString().PadLeft(2);
        string m = span.Minutes.ToString().PadLeft(2);
        string s = span.Seconds.ToString().PadLeft(2);

        return string.Format(formart, d, h, m, s);
    }

    public static string GetStringDHMS(DateTime from, DateTime to, string formart = "{0}:{1}:{2}:{3}")
    {
        TimeSpan span = to - from;
        return GetStringDHMS(span, formart);
    }

    public static string GetStringDHM(System.TimeSpan span, string formart = "{0}:{1}:{2}")
    {
        string d = span.Days.ToString().PadLeft(2);
        string h = span.Hours.ToString().PadLeft(2);
        string m = span.Minutes.ToString().PadLeft(2);

        return string.Format(formart, d, h, m);
    }
    public static string GetStringDHM(DateTime from, DateTime to, string formart = "{0}:{1}:{2}")
    {
        TimeSpan span = to - from;
        return GetStringDHM(span, formart);
    }

    public static string GetStringHMS(System.TimeSpan span, string formart = "{0}:{1}:{2}")
    {
        string h = span.Hours.ToString().PadLeft(2);
        string m = span.Minutes.ToString().PadLeft(2);
        string s = span.Seconds.ToString().PadLeft(2);

        return string.Format(formart, h, m, s);
    }

    public static string GetStringHMS(DateTime from, DateTime to, string formart = "{0}:{1}:{2}")
    {
        TimeSpan span = to - from;
        return GetStringHMS(span, formart);
    }
}
