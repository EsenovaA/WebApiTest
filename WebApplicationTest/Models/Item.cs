namespace WebApplicationTest.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }

    public class ItemModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
