namespace Application.Parameters
{
    public class RequestParameter
    {
        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SearchOrder = "";
            this.SalesPerson = "";
        }

        public RequestParameter(int pageNumber, int pageSize)
        {
            //si es menor a 1 entonces que sea 1. si no que use el valor de pagenumber
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            //si es mayor a 10 entonces que sea 10. si no que use el valor de pagesize
            PageSize = pageSize > 10 ? 10 : pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchOrder { get; set; }
        public string? SalesPerson { get; set; }
    }
}
