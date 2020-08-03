using System;
using SQLite;
namespace Xam2.Models
{
    public class Token
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Access_token { get; set; }
        public string Error_description { get; set; }
        public DateTime Expire_date { get; set; }

        public Token()
        {
        }
    }
}
