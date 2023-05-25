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
        private readonly IRepository<List> _listRepository;

        public ListService(IRepository<List> listRepository)
        {
            this._listRepository = listRepository;
        }

        public List CreateList()
        {
            var currentList = GetCurrentWorkingList();
            if (currentList != null)
            {
                currentList.CurrentWorkingList = false;
                _listRepository.Update(currentList);
            }

            var newList = new List()
            {
                TotalPrice = 0,
                MonthOfYear = DateOnly.FromDateTime(DateTime.Now),
                DateModified = DateTime.Now.ToUniversalTime(),
                IsVisible = true,
                CurrentWorkingList = true
            };
            _listRepository.Add(newList);
            return newList;
        }

        public List DeleteList(int id)
        {
            var listToDelete = _listRepository.GetAll().FirstOrDefault(l => l.ListId == id);
            if (listToDelete != null)
            {
                listToDelete.IsVisible = false;
                listToDelete.CurrentWorkingList = false;
            }

            _listRepository.Update(listToDelete);
            return listToDelete;
        }

        public List GetList(int id)
        {
            var listToReturn = _listRepository.GetAll().FirstOrDefault(l => l.ListId == id && l.IsVisible == true);
            if (listToReturn?.IsVisible == false || listToReturn == null)
                return null;
            return listToReturn;
        }

        public List<List> GetAllLists()
        {
            return _listRepository.GetAll().ToList();
        }

        public void UpdatePrice(int id, double price)
        {
            var listToReturn = _listRepository.GetAll()
                .FirstOrDefault(l => l.ListId == id && l.CurrentWorkingList == true);
            if (listToReturn == null)
                return;
            listToReturn.TotalPrice += price;
            _listRepository.Update(listToReturn);
        }

        public bool CheckIfExisting(int id)
        {
            var listToReturn = _listRepository.GetAll().FirstOrDefault(l => l.ListId == id);
            if (listToReturn == null)
                return false;
            return true;
        }

        public List GetCurrentWorkingList()
        {
            var listToReturn = _listRepository.GetAll().FirstOrDefault(l => l.CurrentWorkingList == true);
            return listToReturn;
        }
    }
}