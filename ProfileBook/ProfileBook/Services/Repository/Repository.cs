using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProfileBook.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        private readonly SQLiteConnection database;

        public Repository()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME));
            database.CreateTable<T>();
        }

        public IEnumerable<T> GetItems()
        {
            return database.Table<T>().ToList();
        }

        public T GetItem(int id)
        {
            return database.Get<T>(id);
        }

        public int DeleteItem(int id)
        {
            return database.Delete<T>(id);
        }

        public int InsertItem(T item)
        {
            return database.Insert(item);
        }

        public int UpdateItem(T item)
        {
            return database.Update(item);
        }
    }
}
