using ProfileBook.Models;
using SQLite;
using System.Collections.Generic;

namespace ProfileBook.Services.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetItems();
        T GetItem(int id);
        int DeleteItem(int id);
        int InsertItem(T item);
        int UpdateItem(T item);
    }
}
