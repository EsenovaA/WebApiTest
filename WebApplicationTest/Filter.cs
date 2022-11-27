namespace WebApplicationTest
{
    public class Filter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CategoryId { get; set; }
        public Filter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public Filter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
