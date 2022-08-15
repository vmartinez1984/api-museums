using System.ComponentModel;

namespace Museums.Core.Dtos
{
    public class LogDto
    {
        public string Id { get; set; }

        public DateTime DateExecution { get; set; } = DateTime.Now;

        public int NumberOfUpdates { get; set; } =0;

        public int NumberErrors { get; set; } = 0;

        public string MuseumIdInProcess { get; set; }

        public int TotalRecords { get; set; } =0;

        [DefaultValue(null)]
        public DateTime? DateCancelation { get; set; }        
        
        public DateTime? DateEndExecution { get; set; }
    }
}