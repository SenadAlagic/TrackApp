using System;
using TrackApp.Core;
using TrackApp.Repository;

namespace TrackApp.Service
{
	public interface ICategoryService
	{
		public Category Add(string name);
		public Category Remove(int id);
		List<Category> GetAll();
		public Category GetById(int id); 
	}

	public class CategoryService:ICategoryService
	{
		IRepository<Category> categoryRepository;
		public CategoryService(IRepository<Category> repository)
		{
			categoryRepository = repository;
		}

        public Category Add(string name)
        {
			var newCategory = new Category()
			{
				Name = name
			};
			categoryRepository.Add(newCategory);
			return newCategory;
		}

        public List<Category> GetAll()
		{
			return categoryRepository.GetAll().ToList();
		}

        public Category GetById(int id)
        {
			return categoryRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();
			
        }

        public Category Remove(int id)
        {
			var categoryToRemove = categoryRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();
			if (categoryToRemove != null)
				categoryRepository.Remove(categoryToRemove);
			return categoryToRemove;
        }
    }
}

