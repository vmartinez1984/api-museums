using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Museums.Core.Entities
{
    public class MuseumEntity
    {
        [JsonProperty("museo_id")]
        public int MuseoId { get; set; }

        [JsonProperty("museo_tematica_n1")]
        public string MuseoTematicaN1 { get; set; }

        [JsonProperty("museo_nombre")]
        public string MuseoNombre { get; set; }

        [JsonProperty("museo_fecha_fundacion")]
        public string MuseoFechaFundacion { get; set; }

        [JsonProperty("museo_adscripcion")]
        public string MuseoAdscripcion { get; set; }

        [JsonProperty("museo_tipo_de_propiedad")]
        public string MuseoTipoDePropiedad { get; set; }

        [JsonProperty("museo_calle_numero")]
        public string MuseoCalleNumero { get; set; }

        [JsonProperty("museo_colonia")]
        public string MuseoColonia { get; set; }

        [JsonProperty("museo_cp")]
        public string MuseoCp { get; set; }

        [JsonProperty("museo_telefono1")]
        public string MuseoTelefono1 { get; set; }

        [JsonProperty("pagina_web")]
        public string PaginaWeb { get; set; }

        [JsonProperty("pagina_web2")]
        public string PaginaWeb2 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("gmaps_latitud")]
        public double GmapsLatitud { get; set; }

        [JsonProperty("gmaps_longitud")]
        public double GmapsLongitud { get; set; }

        [JsonProperty("estado_id")]
        public int EstadoId { get; set; }

        [JsonProperty("municipio_id")]
        public int MunicipioId { get; set; }

        [JsonProperty("localidad_id")]
        public int LocalidadId { get; set; }

        [JsonProperty("nom_ent")]
        public string NomEnt { get; set; }

        [JsonProperty("nom_mun")]
        public string NomMun { get; set; }

        [JsonProperty("nom_loc")]
        public string NomLoc { get; set; }

        [JsonProperty("link_sic")]
        public string LinkSic { get; set; }

        [JsonProperty("fecha_mod")]
        public DateTime? FechaMod { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string HoariosYCostos { get; set; }
        public string DatosGenerales { get; set; }
        public List<string> ListUrlImg { get; set; }
        
        public DateTime? FechaDeActualizacion { get; set; }

        public string State { get; set; }
    }
}