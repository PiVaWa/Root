using System;

namespace CoreAndroid.DL
{
	public interface ITableRow
	{
		int ID { get; set; }
		DateTime DateOfCreation { get; set; }
		int CreatorID { get; set; }
		DateTime DateOfLastUpdate { get; set; }
		int LastUpdatorID { get; set; }
	}
}