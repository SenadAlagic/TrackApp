using System;
using TrackApp.Core;

namespace TrackApp.Service
{
	public interface IListService
	{
		public List GetList(int id);
        public List GetCurrentWorkingList();
		public List CreateList();
		public List DeleteList(int id);
        public List<List> GetAllLists();
        public void UpdatePrice(int id, double price);
        public bool CheckIfExisting(int id);
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

        public void UpdatePrice(int id, double price)
        {
            var listToReturn = InMemoryDb.Lists.Where(l => l.Id == id && l.CurrentWorkingList == true).FirstOrDefault();
            if (listToReturn == null)
                return;
            listToReturn.TotalPrice += price;
        }

        public bool CheckIfExisting(int id)
        {
            var listToReturn = InMemoryDb.Lists.Where(l => l.Id == id).FirstOrDefault();
            if (listToReturn == null)
                return false;
            return true;
        }

        public List GetCurrentWorkingList()
        {
            var listToReturn = InMemoryDb.Lists.Where(l => l.CurrentWorkingList == true).FirstOrDefault();
            if (listToReturn == null)
                return null;
            return listToReturn;
        }
    }
}

