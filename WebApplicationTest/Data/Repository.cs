using System.Xml.Linq;
using WebApplicationTest.Models;

namespace WebApplicationTest.Data
{
    public class Repository
    {
        private List<Category> _categories;
        private List<Item> _items;

        private const int _itemsInCategoryCount = 100;
        
        public Repository()
        {
            InitData();
        }

        private void InitData()
        {
            _categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "History"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Detective"
                }
            };

            _items = new List<Item>();

            var itemId = 0;

            foreach (var category in _categories)
            {
                for (int i = 0; i < _itemsInCategoryCount; i++)
                {
                    _items.Add(new Item()
                    {
                        Id = itemId,
                        Name = $"Item{category.Name}{itemId}",
                        Category = category
                    });
                    itemId++;
                }
            }
        }

        public Category[] GetCategories()
        {
            return _categories.ToArray();
        }

        public Category AddCategory(string name)
        {
            try
            {
                var exist = _categories.Any(c=>c.Name == name);

                if (exist)
                {
                    throw new InvalidOperationException($"Category with name: {name} already exists.");
                }

                var newCategory = new Category()
                {
                    Id = _categories.Max(c => c.Id) + 1,
                    Name = name
                };

                _categories.Add(newCategory);

                return newCategory;
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to add Category with name: {name}.", ex);
            }
        }

        public void DeleteCategoryWithRelatedItems(string name)
        {
            try
            {
                var exist = _categories.Any(c => c.Name == name);

                if (!exist)
                {
                    //Log
                    return;
                }

                var categoryToDelete = _categories.First(c => c.Name == name);

                _items.RemoveAll(i => i.Category.Id == categoryToDelete.Id);
                _categories.Remove(categoryToDelete);
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to delete Category with name: {name}", ex);
            }
        }

        public Category UpdateCategory(int id, string name)
        {
            try
            {
                var categoryToUpdate = _categories.FirstOrDefault(c => c.Id == id);

                if (categoryToUpdate == null)
                {
                    //Log
                    return null;
                }
                
                categoryToUpdate.Name = name;

                var itemsToUpdate = _items.Where(i => i.Category.Id == id).ToArray();
                foreach (var item in itemsToUpdate)
                {
                    item.Category = categoryToUpdate;
                }

                return categoryToUpdate;
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to update Category with name: {name}", ex);
            }
        }

        public Item[] GetItems()
        {
            return _items.ToArray();
        }

        public Item AddItem(string name, string categoryName)
        {
            try
            {
                var exist = _items.Any(i => i.Name == name);

                if (exist)
                {
                    throw new InvalidOperationException($"Item with name: {name} already exists.");
                }

                var existCategory = _categories.FirstOrDefault(c => c.Name == categoryName);

                existCategory ??= AddCategory(categoryName);

                var newItem = new Item()
                {
                    Id = _items.Max(c => c.Id) + 1,
                    Name = name,
                    Category = existCategory
                };

                _items.Add(newItem);

                return newItem;
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to add Category with name: {name}.", ex);
            }
        }

        public void DeleteItem(string name)
        {
            try
            {
                var itemToDelete = _items.FirstOrDefault(i => i.Name == name);

                if (itemToDelete == null)
                {
                    //Log
                    return;
                }
                
                _items.Remove(itemToDelete);
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to delete Item with name: {name}", ex);
            }
        }

        public Item UpdateItem(string name)
        {
            try
            {
                var itemToUpdate = _items.FirstOrDefault(i => i.Name == name);

                if (itemToUpdate == null)
                {
                    //Log
                    return null;
                }

                itemToUpdate.Name = name;

                return itemToUpdate;
            }
            catch (Exception ex)
            {
                //Log
                throw new Exception($"Failed to update Category with name: {name}", ex);
            }
        }
    }
}
