namespace Museums.Core.Dtos
{
    public class MuseumDto
    {
        public int MuseoId { get; set; }

        public string MuseoTematicaN1 { get; set; }

        public string MuseoNombre { get; set; }

        public string MuseoFechaFundacion { get; set; }

        public string MuseoAdscripcion { get; set; }

        public string MuseoTipoDePropiedad { get; set; }

        public string MuseoCalleNumero { get; set; }

        public string MuseoColonia { get; set; }

        public string MuseoCp { get; set; }

        public string MuseoTelefono1 { get; set; }

        public string PaginaWeb { get; set; }

        public string PaginaWeb2 { get; set; }

        public string Email { get; set; }

        public string Twitter { get; set; }

        public double GmapsLatitud { get; set; }

        public double GmapsLongitud { get; set; }

        public int EstadoId { get; set; }

        public int MunicipioId { get; set; }

        public int LocalidadId { get; set; }

        public string NomEnt { get; set; }

        public string NomMun { get; set; }

        public string NomLoc { get; set; }

        public string LinkSic { get; set; }

        public DateTime FechaMod { get; set; }

        public string Id { get; set; }
        
        public string HoariosYCostos { get; set; }
        
        public string DatosGenerales { get; set; }

        public List<string> ListUrlImg { get; set; }

        public DateTime? FechaDeActualizacion { get; set; }

        public string State { get; set; }
    }
}