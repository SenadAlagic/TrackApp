using System;
using TrackApp.Core;

namespace TrackApp.Service
{
	public interface IListService
	{
		public List GetList(int id);
		public List CreateList();
		public List DeleteList(int id);
	}
	public class ListService : IListService
    { 
		public ListService()
		{
		}

        public List CreateList()
        {
            var newList = new List()
            {
                Id = InMemoryDb.Lists.Count + 1,
                MonthOfYear = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = 0
            };
            InMemoryDb.Lists.Add(newList);
            return newList;
        }

        public List DeleteList(int id)
        {
            var listToDelete=InMemoryDb.Lists.Where(l => l.Id == id).FirstOrDefault();
            if (listToDelete != null)
                InMemoryDb.Lists.Remove(listToDelete);
            return listToDelete;
        }

        public List GetList(int id)
        {
            return InMemoryDb.Lists.Where(l => l.Id == id).FirstOrDefault();
        }
    }
}

