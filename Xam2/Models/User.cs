using System;
using SQLite;
using Microsoft.Data.Sqlite;
using System.Data;
namespace Xam2.Models
{
    
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string username, string password)
        {
            this.Username = username ;
            this.Password = password ;
        }

        public bool CheckInfo()
        {
            if ((this.Username.Equals("") || this.Username.Equals(null)) &&
                (this.Password.Equals("") || this.Password.Equals(null)))
                return true ;
            return false ;
        }
    }
}
