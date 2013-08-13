using System;
using System.Linq;
using System.Collections.Generic;

namespace CoreAndroid.DL
{
	/// <summary>
	/// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
	/// It contains methods for retreival and persistance as well as db creation, all based on the 
	/// underlying ORM.
	/// </summary>
	public class SQDatabase : SQLiteConnection
	{
		int _CurrentUserID;

		/// <summary>
		/// Initializes a new instance of the <see cref="CoreAndroid.DL.SQDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public SQDatabase (string path, int CurrentUserID) : base (path)
		{
            _CurrentUserID = CurrentUserID;
			// create the tables
			// CreateTable<Task> ();
        }
		
        /// <summary>
        /// Gets the item having the given ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns>The item of type T if found or null if not found</returns>
	    public T GetItemByID<T>(int ID) where T : IBusinessEntity, new()
        {
            return Table<T>().FirstOrDefault<T>(x => x.ID == ID);
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetAllItems<T>() where T : IBusinessEntity, new()
        {
            return Table<T>().ToList<T>();
        }

        /// <summary>
        /// Inserts the given item into the table. The ID, CreatorID, DateOfCreation, LastUpdator and DateOfLastUpdate are set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>ID of the new item</returns>
        public int AddItem<T>(T item) where T : IBusinessEntity, new()
        {
            item.DateOfCreation = item.DateOfLastUpdate = DateTime.UtcNow;
            item.CreatorID = item.LastUpdatorID = _CurrentUserID;
            return base.Insert(item);
        }

        /// <summary>
        /// Updates the given item. The LastUpdator and DateOfLastUpdate are set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>ID of the item</returns>
        public int EditItem<T>(T item) where T : IBusinessEntity, new()
        {
            item.DateOfLastUpdate = DateTime.UtcNow;
            item.LastUpdatorID = _CurrentUserID;
            base.Update(item);
            return item.ID;
        }

        /// <summary>
        /// If the item does not exist it is created else updated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>Returns the Id of the inserted or updated item</returns>
        public int AddOrEditItem<T>(T item) where T : IBusinessEntity, new()
        {
            return item.ID != 0 ? EditItem(item) : AddItem(item);
        }

        /// <summary>
        /// Deletes the item with the given ID from the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        public void DeleteItem<T>(int ID) where T : IBusinessEntity, new()
        {
            T item = GetItemByID<T>(ID);
            if (item != null) base.Delete(item);
        }

        public void DeleteAllItems<T>() where T : IBusinessEntity, new()
        {
            try
            {
                base.BeginTransaction();
                foreach (T item in GetAllItems<T>()) Delete<T>(item);
                base.Commit();

            }
            catch (Exception)
            {
                base.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets the list of items of type T created by the user with the given ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IList<T> GetItemsCreatedBy<T>(int ID) where T : IBusinessEntity, new()
        {
            return GetAllItems<T>().Where(x => x.CreatorID == ID).ToList();
        }

        /// <summary>
        /// Gets the list of items of type T last updated by the user with the given ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IList<T> GetItemsLastUpdatedBy<T>(int ID) where T : IBusinessEntity, new()
        {
            return GetAllItems<T>().Where(x => x.LastUpdatorID == ID).ToList();
        }
	
	}
}