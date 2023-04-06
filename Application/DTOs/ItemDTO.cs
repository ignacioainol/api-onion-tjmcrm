namespace Application.DTOs
{
    public class ItemDTO
    {
        public string Rcv { get; set; }
        public string Ordline { get; set; }
        public string Mnfr { get; set; }
        public string Prdtyp { get; set; }
        public Decimal Linqty { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public string Sizrmd { get; set; }
        public DateTime? Rcvdate { get; set; }
        public string Status { get; set; }
        public DateTime Chgdate { get; set; }
    }
}
