namespace OMS.Common.Extensions.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 20;
        private const int MinPageSize = 5;

        private int _pageSize = 5;

        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < MinPageSize ? MinPageSize : value;
        }

        public PaginationParams() { }

        public PaginationParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
