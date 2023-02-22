using System.ComponentModel.DataAnnotations;

namespace Vmartinez.RequestInspector.Entities
{
    public class HttpContextEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Application { get; set; }

        public string Path { get; set; }

        public string Method { get; set; }

        public string RequestHeader { get; set; }

        public string RequestBody { get; set; }

        public string ResponseHeader { get; set; }

        public string ResponseBody { get; set; }

        public DateTime RequestDateRegistration { get; set; } = DateTime.Now;

        public DateTime ResponseDateRegistration { get; set; }
    }
}
