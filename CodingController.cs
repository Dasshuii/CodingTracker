using CodingTracker.Util;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal class CodingController
    {
        private readonly Database _database = Database.getInstance();
        public void AddCodeSession()
        {
            string dateFormat = "dd-MM-yy";
            string datePrompt = $"Type the [green]date[/]. Format: [blue]{dateFormat}[/]";

            string timeFormat = "hh\\:mm";
            string startTimePrompt = $"Type the [green]start time[/]. Format [blue]{timeFormat}[/]";
            string endTimePrompt = $"Type the [green]end time[/]. Format [blue]{timeFormat}[/]";

            DateTime date = Input.GetDate(datePrompt, dateFormat);
            TimeSpan startTime = Input.GetTime(startTimePrompt, timeFormat);
            TimeSpan endTime = Input.GetTime(endTimePrompt, timeFormat); ;
            while (endTime < startTime)
            {
                Console.WriteLine("Invalid end time. Try again.");
                endTime = Input.GetTime(endTimePrompt, timeFormat);
            }
            TimeSpan duration = endTime - startTime;
            _database.AddSession(new CodingSession { Date = date, StartTime = startTime, EndTime = endTime, Duration = duration});
        }

        public void ViewSessions()
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);

            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[yellow]Date[/]");
            table.AddColumn("[yellow]Duration (hh:mm)[/]");

            var sessions = _database.LoadSessions();

            foreach (var session in sessions)
            {
                table.AddRow(
                    session.Id.ToString(),
                    session.Date.ToShortDateString(),
                    session.Duration.ToString("hh\\:mm")
                );
            }

            SpectreConsole.PrintSessions(table);
        }

        public void DeleteSession()
        {
            ViewSessions();
            int id = SpectreConsole.GetIntegerInput("Type session's [red]id[/]? ");
            _database.DeleteSession(id);
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
    }
}
