using CodingTracker;
using CodingTracker.Util;
using SQLitePCL;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        Batteries.Init();
        Database db = Database.getInstance();
        db.InitializeSchema();
        UserInterface userInterface = new();
        userInterface.MainMenu();
    }
}