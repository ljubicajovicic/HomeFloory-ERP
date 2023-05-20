namespace HomeFloory.Models
{
    public class Pagination<T>
    {
        public Pagination( IReadOnlyList<T> data, decimal pageNumber, decimal pageSize, decimal total, decimal totalPages)
        {
            this.data = data;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.total = total;
            this.totalPages = totalPages;
            
        }

        public IReadOnlyList<T> data { get; set; }
        public decimal pageNumber { get; set; }
        public decimal pageSize { get; set; }
        public decimal total { get; set; }
        public decimal totalPages { get; set; }

    }
}
