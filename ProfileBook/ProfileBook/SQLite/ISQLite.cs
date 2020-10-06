using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.SQLite
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
