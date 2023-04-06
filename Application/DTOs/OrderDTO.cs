namespace Application.DTOs
{
    public class OrderDTO
    {
        public string ORDNBR { get; set; }
        public string ORGLOC { get; set; }
        public DateTime ORDDATE { get; set; }
        public string COFFST { get; set; }
        public string SLSPROF { get; set; }
        public string CUST { get; set; }
        public string LNAME { get; set; }
        public string FNAME { get; set; }
        public string MINIT { get; set; }
        public string CLIENTNAME
        {
            get { return $"{LNAME.Trim()} {FNAME.Trim()} {MINIT}"; }
        }
        public string DESCR { get; set; }
    }
}
