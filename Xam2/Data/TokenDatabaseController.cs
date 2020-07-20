using System;
using Xam2.Models;
using Xamarin.Forms;
using Microsoft.Data.Sqlite;
namespace Xam2.Data
{
    public class TokenDatabaseController
    {
        public static object locker = new object();
        public SqliteConnection database;

        public TokenDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            using (var command = database.CreateCommand())
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Token" +
                    "(" +
                    "[Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "[Access_token] VARCHAR(64), [Error_description VARCHAR(64)" +
                    "[Expire_date]) VARCHAR(64);";

                command.ExecuteNonQuery();
            }
        }

        public Token GetToken()
        {
            lock (locker)
            {
                using (var command = database.CreateCommand())
                {
                    command.CommandText = "SELECT Access_token, Error_description, Expire_date from Token LIMIT 1;";
                    string at = "", ed = "";
                    DateTime dt = new DateTime();

                    var result = command.ExecuteReader();
                    while (result.Read())
                    {
                        at = result.GetString(0);
                        ed = result.GetString(1);
                        dt = DateTime.Parse(result.GetString(2));
                    }

                    Token t = new Token();
                    t.Access_token = at;
                    t.Error_description = ed;
                    t.Expire_date = dt;
                    return t;
                }
            }
        }

        public void SaveToken(Token token)
        {
            lock (locker)
            {
                if (token.Id != 0)
                {
                    using (var command = database.CreateCommand())
                    {

                        command.CommandText = @"UPDATE Token
                            SET Access_token='" + token.Access_token + "', Error_description='"
                            + token.Error_description + "', Expire_date='"+token.Expire_date+"' " +
                            "WHERE Id=" + token.Id + ";";
                        command.ExecuteNonQuery();
                    }
                    return;
                }
                else
                {
                    using (var command = database.CreateCommand())
                    {

                        command.CommandText = @"INSERT INTO Token(Id,
                            Access_token, Error_description, Expire_date) VALUES('"
                            + token.Id + "', '" + token.Access_token + "', '"
                            + token.Error_description + "', '" +
                            token.Expire_date+"') ;";
                        command.ExecuteNonQuery();
                    }
                    return;
                }

            }

        }

        public void DeleteToken(int id)
        {
            lock (locker)
            {
                using (var command = database.CreateCommand())
                {

                    command.CommandText = @"DELETE FROM Token WHERE Id = "
                        + id + ";";
                    command.ExecuteNonQuery();
                }
                return;
            }
        }
    }
}
