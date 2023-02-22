using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Museums.Core.Dtos
{
    public class PagerDto : PagerDtoIn
    {

        public int TotalRecords { get; set; }
        public int CountPage
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecordsFiltered / RecordsPerPage);
            }
        }

        public int TotalRecordsFiltered { get; set; }

    }

    public class PagerDtoIn
    {
        public string Search { get; set; }
        [DefaultValue(1)]
        public int PageCurrent { get; set; } = 1;

        [Range(10, 50)]
        [DefaultValue(10)]
        public int RecordsPerPage { get; set; } = 10;
    }
}