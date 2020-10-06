using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using ProfileBook.iOS.SQLite;
using ProfileBook.SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace ProfileBook.iOS.SQLite
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            return path;
        }
    }
}