namespace Museums.Core.Entities
{
    public class Pager
    {
        
        public int PageCurrent { get; set; } = 1;

        
        public int RecordsPerPage { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalRecordsFiltered { get; set; }
        public string Search { get; set; }
    }
}