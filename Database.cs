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

        private List<CodingSession> LoadSessions()
        {
            string query = "SELECT * FROM coding_sessions;";
            SqliteConnection conn = new(_connectionString);
            return [.. conn.Query<CodingSession>(query)];
        }
    }
}
