using System;
using TrackApp.Core;

namespace TrackApp.Service
{
	public interface IListService
	{
		public List GetList(int id);
		public List CreateList();
		public List DeleteList(int id);
        public List<List> GetAllLists();
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
                listToDelete.IsVisible=false;
            return listToDelete;
        }

        public List GetList(int id)
        {
            var listToReturn = InMemoryDb.Lists.Where(l => l.Id == id && l.IsVisible == true).FirstOrDefault();
            if (listToReturn?.IsVisible == false || listToReturn == null)
                return null;
            return listToReturn;
        }

        public List<List> GetAllLists()
        {
            return InMemoryDb.Lists.ToList();
        }
    }
}

