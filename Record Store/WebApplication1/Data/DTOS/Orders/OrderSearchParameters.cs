namespace Record_Store.Data.DTOS.Orders
{
    public class OrderSearchParameters
    {
        private uint _pageSize = 2;
        private const uint MaxPageSize = 50;
        public uint PageNumber { get; set; } = 1;
        public uint PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
