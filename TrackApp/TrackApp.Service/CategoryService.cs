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

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> repository)
        {
            _categoryRepository = repository;
        }

        public Category Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var newCategory = new Category()
            {
                Name = name
            };
            _categoryRepository.Add(newCategory);
            return newCategory;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetAll().FirstOrDefault(c => c.CategoryId == id);
        }

        public Category Remove(int id)
        {
            var categoryToRemove = _categoryRepository.GetAll().FirstOrDefault(c => c.CategoryId == id);
            if (categoryToRemove != null)
                _categoryRepository.Remove(categoryToRemove);
            return categoryToRemove;
        }
    }
}