using HtmlAgilityPack;
using Museums.Core.Entities;
using Newtonsoft.Json;
using ScrapySharp.Extensions;

namespace Museums.Service.Scraping
{
    public class ScrapService
    {
        const string url = "https://sic.cultura.gob.mx/opendata/d/9_museo_directorio.json";

        public async Task<List<MuseumEntity>> GetMuseumsAsync()
        {
            List<MuseumEntity> list;

            using (var httpClient = new HttpClient())
            {
                string json;

                json = await httpClient.GetStringAsync(url);

                list = JsonConvert.DeserializeObject<List<MuseumEntity>>(json);
            }

            return list;
        }

        public async Task GetMuseumsAsync(MuseumEntity entity)
        {
            HtmlWeb htmlWeb;
            HtmlDocument htmlDocument;

            htmlWeb = new HtmlWeb();
            htmlDocument = await htmlWeb.LoadFromWebAsync(entity.LinkSic);
            entity.HoariosYCostos = GetScheduleAndPrice(htmlDocument);
            entity.DatosGenerales = GetGeneralData(htmlDocument);
            entity.ListUrlImg = GetListUrlImg(htmlDocument);
            entity.FechaDeActualizacion = GetDatetime(htmlDocument);
        }

        private string GetGeneralData(HtmlDocument htmlDocument)
        {
            string generalData;
            HtmlNode htmlNode;

            htmlNode = htmlDocument.DocumentNode.CssSelect("#contenedor_subtemas > div > div").First();
            generalData = htmlNode.InnerText;

            return generalData;
        }

        private string GetScheduleAndPrice(HtmlDocument htmlDocument)
        {
            string scheduleAndPrice;
            HtmlNode htmlNode;

            htmlNode = htmlDocument.DocumentNode.CssSelect("#datoscomplemento > p").First();
            scheduleAndPrice = htmlNode.InnerText;

            return scheduleAndPrice;
        }

        private DateTime? GetDatetime(HtmlDocument htmlDocument)
        {
            DateTime? dateTime;
            HtmlNode htmlNode;

            htmlNode = htmlDocument.DocumentNode.CssSelect("#dproblemas").First();
            var lines = htmlNode.InnerHtml.Split("<br>");
            dateTime = GetDatetime(lines[0]);

            return dateTime;
        }
        private List<string> GetListUrlImg(HtmlDocument htmlDocument)
        {
            List<HtmlNode> htmlNode;
            List<string> list;

            list = new List<string>();
            htmlNode = htmlDocument.DocumentNode.CssSelect("#fotobanda_ficha > img").ToList();
            htmlNode.ForEach(node =>
            {
                list.Add(node.GetAttributeValue("src"));
            });

            return list;
        }

        public void GetMuseum(MuseumEntity entity)
        {
            HtmlWeb htmlWeb;
            HtmlDocument htmlDocument;

            htmlWeb = new HtmlWeb();
            htmlDocument = htmlWeb.LoadFromWebAsync(entity.LinkSic).Result;
            entity.HoariosYCostos = GetScheduleAndPrice(htmlDocument);
            entity.DatosGenerales = GetGeneralData(htmlDocument);
            entity.ListUrlImg = GetListUrlImg(htmlDocument);
            entity.FechaDeActualizacion = GetDatetime(htmlDocument);
        }

        /// <summary>
        /// Regresa la una fecha valida a partir de una cadena
        /// </summary>
        private DateTime? GetDatetime(string date)
        {
            //ej. 18 de junio del 2022
            string data;
            int day;
            int year;
            int month;

            data = date.Trim();
            day = 0;
            month = 0;
            year = 0;
            var list = data.Split(" ");
            foreach (var item in list)
            {
                day = ValidateDay(item);
                if (day != 0)
                    break;
            }
            foreach (var item in list)
            {
                month = ValidateMonth(item);
                if (month != 0)
                    break;
            }
            foreach (var item in list)
            {
                year = ValidateYear(item);
                if (year != 0)
                    break;
            }
            if (year == 0 || month == 0 || day == 0)
                return null;
            else
                return new DateTime(year, month, day);
        }

        private int ValidateMonth(string item)
        {
            int month;

            switch (item.ToLower())
            {
                case "enero":
                    month = 1;
                    break;
                case "febrero":
                    month = 2;
                    break;
                case "marzo":
                    month = 3;
                    break;
                case "abril":
                    month = 4;
                    break;
                case "mayo":
                    month = 5;
                    break;
                case "junio":
                    month = 6;
                    break;
                case "julio":
                    month = 7;
                    break;
                case "agosto":
                    month = 8;
                    break;
                case "septiembre":
                    month = 9;
                    break;
                case "octubre":
                    month = 10;
                    break;
                case "noviembre":
                    month = 11;
                    break;
                case "diciembre":
                    month = 12;
                    break;
                default:
                    month = 0;
                    break;
            }

            return month;
        }

        private int ValidateYear(string item)
        {
            int year;

            year = 0;
            item = item.Trim().Replace(",", string.Empty);
            if (Int32.TryParse(item, out year) && year > 1900)
            {

            }
            else
            {
                year = 0;
            }

            return year;
        }

        private int ValidateDay(string item)
        {
            int day;

            day = 0;
            if (Int32.TryParse(item, out day) && day > 0 && day <= 31)
            {

            }
            else
            {
                day = 0;
            }

            return day;
        }
    }
}