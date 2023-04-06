namespace Application.Wrappers
{
    public class PageResponse<T> : Response<T>
    {
        public PageResponse(T data, int recordsTotal, int recordsFiltered)
        {
            this.RecordsFiltered = recordsFiltered;
            this.RecordsTotal = recordsTotal;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }

        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
