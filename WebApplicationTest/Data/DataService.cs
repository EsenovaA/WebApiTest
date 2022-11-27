using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    public class DataService
    {
        private static DataService _service;

        private readonly Repository _repository;
        protected DataService()
        {
            _repository = new Repository();
        }

        public static DataService GetDataService()
        {
            if (_service == null)
            {
                _service = new DataService();
            }

            return _service;
        }

        public IEnumerable<Item> GetItems()
        {
            return _repository.GetItems();
        }

        public Item AddItem(ItemModel item)
        {
            return _repository.AddItem(item.Name, item.CategoryName);
        }

        public Item UpdateItem(ItemModel item)
        {
            return _repository.UpdateItem(item.Name);
        }

        public void DeleteItem(string itemName)
        {
            _repository.DeleteItem(itemName);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _repository.GetCategories();
        }

        public Category AddCategory(string categoryName)
        {
            return _repository.AddCategory(categoryName);
        }

        public Category UpdateCategory(int id, string categoryName)
        {
            return _repository.UpdateCategory(id, categoryName);
        }

        public void DeleteCategory(string categoryName)
        {
            _repository.DeleteItem(categoryName);
        }
    }
}
