using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models.Others
{
    public partial class PaginationInfo : ObservableObject
    {
        public event Func<Task>? PageChanged;
        partial void OnCurrentPageChanged(int value) => PageChanged?.Invoke();
        partial void OnPageSizeChanged(int value) => PageChanged?.Invoke();


        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _pageSize = 5;

        [ObservableProperty]
        private int _totalPages = 1;

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
