using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreAndroid.DL
{
    public abstract class TableRow : ITableRow
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int CreatorID { get; set; }
        public DateTime DateOfLastUpdate { get; set; }
        public int LastUpdatorID { get; set; }

		[Ignore]
		public UserRow Creator
		{
			get { return CoreAndroid.DAL.Repository<UserRow>.GetItem (CreatorID);}
		}
		
        [Ignore]
        public UserRow LastUpdator
        {
            get { return CoreAndroid.DAL.Repository<UserRow>.GetItem(LastUpdatorID); }
        }

        // Methods
        public IList<T> GetItemsCreatedBy<T>(int id) where T : ITableRow, new()
        {
			return CoreAndroid.DAL.Repository<T>.GetItemsCreatedBy(id);
        }
        public IList<T> GetItemsLastUpdatedBy<T>(int id) where T : ITableRow, new()
        {
            return CoreAndroid.DAL.Repository<T>.GetItemsLastUpdatedBy(id);
        }
    }

	public partial class TaskRow : TableRow
	{
		public TaskRow ()
		{
		}

        public TaskRow(string Name, string Notes, 
            DateTime? DueDate, 
            byte[] Pronounciation, byte[] PictureThumbnail)
        {
            this.Name = Name;
            this.Notes = Notes;
            this.DueDate = DueDate;
            this.Pronounciation = Pronounciation;
            this.PictureThumnail = PictureThumnail;
        }

        [Indexed, MaxLength(50) ]
		public string Name { get; set; }
		public string Notes { get; set; }
		public DateTime? DueDate { get; set; }
        public byte[] Pronounciation { get; set; }
        public byte[] PictureThumnail { get; set; }
    }

    public partial class UserRow : TableRow
    {
        public UserRow()
        {
        }
        public UserRow(string Name)
        {
            this.Name = Name;
        }

        [Indexed, MaxLength(50)]
        public string Name { get; set; }
    }
}