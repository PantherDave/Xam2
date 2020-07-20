using System;
using Xam2.Data;
using Xamarin.Forms;
using System.IO;
using Microsoft.Data.Sqlite;
using Xam2.iOS.Data;

[assembly: Dependency(typeof(SQLite_IOS))]

namespace Xam2.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS()
        {
        }

        public SqliteConnection GetConnection()
        {
            var sqliteFilename = "TestDB.db3";
            string docsPath = Environment.GetFolderPath(
                Environment.SpecialFolder.Personal);
            var libPath = Path.Combine(docsPath, "..", "Library");
            var path = Path.Combine(libPath, sqliteFilename);
            var conn = new SqliteConnection(path);

            return conn;
        }
    }
}
