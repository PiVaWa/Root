using System;

namespace CoreAndroid
{
	public interface IBusinessEntity
	{
		int ID { get; set; }
		DateTime DateOfCreation { get; set; }
		int CreatorID { get; set; }
		DateTime DateOfLastUpdate { get; set; }
		int LastUpdatorID { get; set; }
	}
}

