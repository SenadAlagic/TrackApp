using System;
using TrackApp.Core;
using TrackApp.Repository;

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
        IRepository<List> listRepository;
		public ListService(IRepository<List> listRepository)
		{
            this.listRepository = listRepository;
		}

        public List CreateList()
        {
            var currentList = GetCurrentWorkingList();
            currentList.CurrentWorkingList = false;
            listRepository.Update(currentList);
            var newList = new List()
            {
                TotalPrice = 0,
                MonthOfYear = DateOnly.FromDateTime(DateTime.Now),
                DateModified = DateTime.Now,
                IsVisible = true,
                CurrentWorkingList = true
            };
            this.listRepository.Add(newList);
            return newList;
        }

        public List DeleteList(int id)
        {
            var listToDelete=listRepository.GetAll().Where(l => l.Id == id).FirstOrDefault();
            if (listToDelete != null)
                listToDelete.IsVisible=false;
            listRepository.Update(listToDelete);
            return listToDelete;
        }

        public List GetList(int id)
        {
            var listToReturn = listRepository.GetAll().Where(l => l.Id == id && l.IsVisible == true).FirstOrDefault();
            if (listToReturn?.IsVisible == false || listToReturn == null)
                return null;
            return listToReturn;
        }

        public List<List> GetAllLists()
        {
            return listRepository.GetAll().ToList();
        }

        public void UpdatePrice(int id, double price)
        {
            var listToReturn = listRepository.GetAll().Where(l => l.Id == id && l.CurrentWorkingList == true).FirstOrDefault();
            if (listToReturn == null)
                return;
            listToReturn.TotalPrice += price;
            listRepository.Update(listToReturn);
        }

        public bool CheckIfExisting(int id)
        {
            var listToReturn = listRepository.GetAll().Where(l => l.Id == id).FirstOrDefault();
            if (listToReturn == null)
                return false;
            return true;
        }

        public List GetCurrentWorkingList()
        {
            var listToReturn = listRepository.GetAll().Where(l => l.CurrentWorkingList == true).FirstOrDefault();
            if (listToReturn == null)
                return null;
            return listToReturn;
        }
    }
}

