using System;
using TrackApp.Core;

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
		public CategoryService()
		{
		}

        public Category Add(string name)
        {
			var newCategory = new Category()
			{
				Id = InMemoryDb.Categories.Count + 1,
				Name = name
			};
			InMemoryDb.Categories.Add(newCategory);
			return newCategory;
		}

        public List<Category> GetAll()
		{
			return InMemoryDb.Categories;
		}

        public Category GetById(int id)
        {
			return InMemoryDb.Categories.Where(c => c.Id == id).FirstOrDefault();
			
        }

        public Category Remove(int id)
        {
			var categoryToRemove = InMemoryDb.Categories.Where(c => c.Id == id).FirstOrDefault();
			if (categoryToRemove != null)
				InMemoryDb.Categories.Remove(categoryToRemove);
			return categoryToRemove;
        }
    }
}

