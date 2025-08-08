using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace CodingTracker
{
    internal class Database
    {
        private static Database? _instance;
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        private static List<CodingSession> sessions = [];

        private Database() 
        {
            CreateTables();
        }
        public static Database getInstance()
        {
            _instance ??= new Database();
            return _instance;
        }

        public void InitializeSchema()
        {
            CreateTables();
            sessions = LoadSessions();
        }

        private void CreateTables()
        {
            string query = "CREATE TABLE IF NOT EXISTS coding_sessions (" +
                "id INTEGER NOT NULL PRIMARY KEY," +
                "date TEXT NOT NULL," +
                "startTime TEXT NOT NULL," +
                "endTime TEXT NOT NULL," +
                "duration TEXT NOT NULL" +
                ");";
            using SqliteConnection conn = new(_connectionString);
            conn.Open();
            using SqliteCommand command = new(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public List<CodingSession> LoadSessions()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
            string query = "SELECT * FROM coding_sessions;";
            SqliteConnection conn = new(_connectionString);
            return [.. conn.Query<CodingSession>(query)];
        }

        public void AddSession(CodingSession session)
        {
            using SqliteConnection conn = new(_connectionString);
            conn.Open();
            string query = "INSERT INTO coding_sessions (date, startTime, endTime, duration) VALUES (@date, @startTime, @endTime, @duration);";
            var codeSession = new { 
                    date = session.Date.ToShortDateString(), 
                    startTime = session.StartTime.ToString("hh\\:mm"), 
                    endTime = session.EndTime.ToString("hh\\:mm"),
                    duration = session.Duration.ToString("hh\\:mm"),
            };
            var rowsAffected = conn.Execute(query, codeSession);
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
        }

        public void DeleteSession(int id)
        {
            using SqliteConnection conn = new(_connectionString);
            conn.Open();
            string query = "DELETE FROM coding_sessions WHERE id = @id";
            conn.Execute(query, new { id });
        }
    }
}
