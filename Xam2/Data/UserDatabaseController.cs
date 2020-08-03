using System;
using Xam2.Models;
using SQLite;
using Xamarin.Forms;
namespace Xam2.Data
{
    public class UserDatabaseController
    {
        public static object locker = new object();
        public SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
        }

        public User GetUser()
        {
            lock (locker)
            {
                if (database.Table<User>().Count() == 0)
                    return null;
                else
                    return database.Table<User>().First();
            }
        }

        public int SaveUser(User user)
        {
            lock (locker)
            {
                if (user.Id != 0)
                {
                    database.Update(user);
                    return user.Id;
                }
                else
                {
                    return database.Insert(user);
                }   

            }
            
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
    }
}
