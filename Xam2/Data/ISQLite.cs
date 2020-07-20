using System;
using Microsoft.Data.Sqlite;

namespace Xam2.Data
{
    public interface ISQLite
    {
        SqliteConnection GetConnection();
    }
}
