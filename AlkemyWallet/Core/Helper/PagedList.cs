namespace AlkemyWallet.Core.Helper
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        //public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);

        public PagedList(List<T> items, int count, int pageNumber)
        {
            TotalCount = count;
            //PageSize = 10;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)10);
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> sourse, int pageNumber)
        {
            var count = sourse.Count();
            var items = sourse.Skip((pageNumber - 1) * 10).Take(10).ToList();
            return new PagedList<T>(items, count, pageNumber);
        }
    }
}
