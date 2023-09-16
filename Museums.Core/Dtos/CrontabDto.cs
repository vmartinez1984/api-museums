using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Museums.Core.Dtos
{
    public class CrontabDto : CrontabDtoIn
    {
        public string Id { get; set; }

        public bool IsActivate { get; set; } = true;
    }

    public class CrontabDtoIn
    {
        [Range(0, 59)]
        public int? Minute { get; set; } = 0;

        [Range(0, 23)]
        public int? Hour { get; set; } = 0;

        [DefaultValue(null)]
        [Range(1, 31)]
        public int? DayOfMonth { get; set; }

        [DefaultValue(null)]
        [Range(1, 12)]
        public int? Month { get; set; }

        [DefaultValue(null)]
        [Range(1, 7)]
        public int? DayOfWeek { get; set; }

        public string Operacion { get; set; }


        public string Comentario { get; set; }

        public string Estado { get; set; }
    }
}