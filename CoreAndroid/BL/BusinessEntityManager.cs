using System;
using System.Linq;
using System.Collections.Generic;
using CoreAndroid.DL;
using CoreAndroid.DAL;

namespace CoreAndroid.BL
{
    public abstract class BusinessEntityManager<T> where T : ITableRow, new()
    {
        static BusinessEntityManager() { }

        public static T GetItem(int id) 
        {
            return Repository<T>.GetItem(id);
        }
        public static IList<T> GetItems()
        {
            return Repository<T>.GetItems();
        }
        public static int SaveItem(T item)
        {
            return Repository<T>.SaveItem(item);
        }
        public static void DeleteItem(int id)
        {
            Repository<T>.DeleteItem(id);
        }
        public static void DeleteAllItems()
        {
            Repository<T>.DeleteAllItem();
        }
        public static IList<T> GetItemsCreatedBy(int id)
        {
            return Repository<T>.GetItemsCreatedBy(id);
        }
        public static IList<T> GetItemsLastUpdatedBy(int id)
        {
            return Repository<T>.GetItemsLastUpdatedBy(id);
        }
    }

	public class TaskManager : BusinessEntityManager<Task>
	{
        public static IList<Task> GetItemsSorted()
        {
            return GetItems().OrderBy(x => x.Name).ToList();
        }
	}

    public class UserManager : BusinessEntityManager<User>
    {
    }
}