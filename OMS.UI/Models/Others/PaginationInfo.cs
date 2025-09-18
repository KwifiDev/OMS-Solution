using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models.Others
{
    public partial class PaginationInfo : ObservableObject
    {
        public event Func<Task>? PageChanged;


        private int _currentPage = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (SetProperty(ref _currentPage, Math.Max(1, Math.Min(value, TotalPages))))
                    PageChanged?.Invoke();
            }
        }

        private int _totalPages = 1;

        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, Math.Max(1, value));
        }

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (SetProperty(ref _pageSize, Math.Max(5, value)))
                    PageChanged?.Invoke();
            }
        }

        [ObservableProperty]
        private int _totalItems = 0;


        public void NextPage() => CurrentPage = Math.Min(CurrentPage + 1, TotalPages);
        public bool CanNextPage => CurrentPage < TotalPages;

        public void PreviousPage() => CurrentPage = Math.Max(CurrentPage - 1, 1);
        public bool CanPreviousPage => CurrentPage > 1;

        public void FirstPage() => CurrentPage = 1;
        public bool CanFirstPage => CurrentPage > 1;

        public void LastPage() => CurrentPage = TotalPages;
        public bool CanLastPage => CurrentPage < TotalPages;
    }
}
