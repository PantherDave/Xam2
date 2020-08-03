using System;
using SQLite;

namespace Xam2.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
