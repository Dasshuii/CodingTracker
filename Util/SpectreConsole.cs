using Spectre.Console;

namespace CodingTracker.Util;

internal class SpectreConsole
{
    public static string GetInput(string prompt) => AnsiConsole.Prompt(new TextPrompt<string>(prompt));

    public static int GetIntegerInput(string prompt) => AnsiConsole.Prompt(new TextPrompt<int>(prompt));

    public static void PrintSessions(Table table)
    {
        AnsiConsole.Write(table);
    }
}
