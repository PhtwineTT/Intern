namespace AuthAPI.Models.DTO.Auth
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 50) ? value : 50;
        }
        public string? SearchTerm { get; set; }
    }
}
