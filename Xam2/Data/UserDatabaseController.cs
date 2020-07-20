using System;
using Xam2.Models;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;
namespace Xam2.Data
{
    public class UserDatabaseController
    {
        public static object locker = new object();
        public SqliteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            using (var command = database.CreateCommand())
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS User" +
                    "("+
                    "[Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "[Username] VARCHAR(64), [Password] VARCHAR(64)" +
                    ");" ;

                command.ExecuteNonQuery();
            }
        }

        public User GetUser()
        {
            lock (locker)
            {
                using (var command = database.CreateCommand())
                {
                    command.CommandText = "SELECT Username, Password from" +
                        " User LIMIT 1;";
                    string uname = "", passwd = "";

                    var result = command.ExecuteReader();
                    while (result.Read())
                    {
                        uname = result.GetString(0);
                        passwd = result.GetString(1);
                    }

                    return new User(uname, passwd);
                }
            }
        }

        public void SaveUser(User user)
        {
            lock (locker)
            {
                if (user.Id != 0)
                {
                    using (var command = database.CreateCommand())
                    {

                        command.CommandText = @"UPDATE User
                            SET Username='" + user.Username + "', Password='"
                            +user.Username + "'" +
                            "WHERE Id=" + user.Id + ";";
                        command.ExecuteNonQuery();
                    }
                    return;
                }
                else
                {
                    using (var command = database.CreateCommand())
                    {

                        command.CommandText = @"INSERT INTO User(Id,
                            Username, Password) VALUES('"
                            +user.Id+"', '"+user.Username+"', '"
                            +user.Password+"') ;" ;
                        command.ExecuteNonQuery();  
                    }
                    return ;
                }

            }
            
        }

        public void DeleteUser(int id)
        {
            lock (locker)
            {
                using (var command = database.CreateCommand())
                {

                    command.CommandText = @"DELETE FROM User WHERE Id = "
                        + id + ";" ;
                    command.ExecuteNonQuery();
                }
                return ;
            }
        }
    }
}
