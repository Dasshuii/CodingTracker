using Spectre.Console;
using CodingTracker.Models.Enums;

namespace CodingTracker;
internal class UserInterface
{
    private readonly CodingController _controller = new();
    internal void MainMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();

            var codeAction = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .Title("What do you want to do next?")
                .AddChoices(Enum.GetValues<MenuAction>())
            );

            switch(codeAction)
            {
                case MenuAction.AddSession:
                    Console.WriteLine("Adding session.");
                    _controller.AddCodeSession();
                    break;
                case MenuAction.DeleteSession:
                    _controller.DeleteSession();
                    break;
                case MenuAction.UpdateSession:
                    _controller.UpdateSession();
                    break;
                case MenuAction.ViewSessions:
                    _controller.ViewSessions();
                    Console.WriteLine("Press Any Key to Continue.");
                    Console.ReadKey();
                    break;
                case MenuAction.Exit:
                    exit = true;
                    break;
            }
        }
    }
}
