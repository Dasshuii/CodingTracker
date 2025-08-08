using Spectre.Console;
using System.Globalization;

namespace CodingTracker.Util;

internal class Input
{
    public static DateTime GetDate(string prompt, string format)
    {
        string dateInput = SpectreConsole.GetInput(prompt);
        DateTime date;
        while (!DateTime.TryParseExact(dateInput, format, null, DateTimeStyles.None, out date))
            dateInput = SpectreConsole.GetInput(prompt);

        return date;
    }

    public static TimeSpan GetTime(string prompt, string format)
    {
        string timeInput = SpectreConsole.GetInput(prompt);
        TimeSpan time;
        while (!TimeSpan.TryParseExact(timeInput, format, null, TimeSpanStyles.None, out time))
            timeInput = SpectreConsole.GetInput(prompt);
        return time;
    }
}
