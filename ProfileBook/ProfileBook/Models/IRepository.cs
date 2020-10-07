using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetItems();
        T GetItem(int id);
        int DeleteItem(int id);
        int SaveItem(T item);
    }
}
