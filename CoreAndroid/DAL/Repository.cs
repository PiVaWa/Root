using System;
using System.IO;
using System.Collections.Generic;

using CoreAndroid.DL;

namespace CoreAndroid.DAL
{
    /// <summary>
    /// Generic Data Access Layer Class for the T type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> where T : ITableRow, new()
    { 
		SQDatabase _db = null;
        public SQDatabase db
        {
            get { return _db; }
        }
		protected static string _dbLocation;		
		protected static Repository<T> _me;

        static Repository()
		{
			_me = new Repository<T>();
		}
		
		protected Repository()
		{
            const int CurrentUserID = 1;
			// set the db location
			//_dbLocation = Path.Combine (NSBundle.MainBundle.BundlePath, "Library/TaskDB.db3");
			_dbLocation = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "CfBridgeDB.db3");
			// instantiate the database	
			this._db = new SQDatabase(_dbLocation, CurrentUserID);
		}

        public static T GetItem(int id)
        {
            return _me._db.GetItemByID<T>(id);
        }
        public static IList<T> GetItems()
        {
            return _me._db.GetAllItems<T>();
        }
        public static int SaveItem(T item)
        {
            return _me._db.AddOrEditItem<T>(item);
        }
        public static void DeleteItem(int id) 
        {
            _me._db.DeleteItem<T>(id);
        }
        public static void DeleteAllItem()
        {
            _me._db.DeleteAllItems<T>();
        }
        public static IList<T> GetItemsCreatedBy(int id)
        {
            return _me._db.GetItemsCreatedBy<T>(id);
        }
        public static IList<T> GetItemsLastUpdatedBy(int id)
        {
            return _me._db.GetItemsLastUpdatedBy<T>(id);
        }

    }
}