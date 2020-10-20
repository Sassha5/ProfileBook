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
        private static readonly object locker = new object();

        public Repository()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME));
            database.CreateTable<T>();
        }

        public IEnumerable<T> GetItems()
        {
            lock (locker)
            {
                return database.Table<T>();
            }
        }

        public T GetItem(int id)
        {
            lock (locker)
            {
                return database.Get<T>(id);
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<T>(id);
            }
        }

        public int InsertItem(T item)
        {
            lock (locker)
            {
                return database.Insert(item);
            }
        }

        public int UpdateItem(T item)
        {
            lock (locker)
            {
                return database.Update(item);
            }
        }
    }
}
