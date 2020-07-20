using System;
using System.Text;
using System.IO;
using Android.OS;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Xam2.Data;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;
using Xam2.Droid.Data;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Xam2.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }

        public SqliteConnection GetConnection()
        {
            var sqliteFilename = "TestDB.db3";
            string docsPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docsPath, sqliteFilename);
            var conn = new SqliteConnection(path);

            return conn ;
        }
    }
}
