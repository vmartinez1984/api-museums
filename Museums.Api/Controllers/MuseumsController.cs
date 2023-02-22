using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Museums.Api.Helpers;
using Museums.Api.Models;
using Museums.Core.Dtos;
using Museums.Core.Interfaces;
using Museums.Service.Scraping;

namespace Museums.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MuseumsController : ControllerBase
    {
        private IUnitOfWorkBl _unitOfWorkBl;
        //private ScrapService _scrapService;
        private ILogger<MuseumsController> _logger;

        //private IBackgroundJobClient _backgroundJobClient;

        public MuseumsController(
            IUnitOfWorkBl unitOfWorkBl
            //, ScrapService scrapService
            , ILogger<MuseumsController> logger
            //, IBackgroundJobClient backgroundJobClient
        )
        {
            _unitOfWorkBl = unitOfWorkBl;
            //_scrapService = scrapService;
            _logger = logger;
            //_backgroundJobClient = backgroundJobClient;
        }

        /// <summary>
        /// Get list of museums from CDMX paginated, total records and total records filtered in the header /
        /// Obtien una lista de museos de la CDMX paginada, total de registros y total de registros filtrados en el header
        /// </summary>
        /// <response code="200">Returns list of museums/Regresa una lista de museos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromQuery] PagerDtoIn pagerIn)
        {
            PagerDto pager;

            pager = new PagerDto
            {
                PageCurrent = pagerIn.PageCurrent,
                RecordsPerPage = pagerIn.RecordsPerPage,
                Search = pagerIn.Search
            };
            var museumPager = await _unitOfWorkBl.Museum.GetAsync(pager);
            this.HttpContext.AddHeaderTotalRecords(museumPager.TotalRecords);
            this.HttpContext.AddHeaderTotalRecordsFiltered(museumPager.TotalRecordsFiltered);

            return Ok(museumPager);
        }

        /// <summary>
        /// Get museum by museumId or id from CDMX
        /// </summary>
        /// <response code="200">Museum</response>
        /// <response code="404">Museum no found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MuseumDto), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _unitOfWorkBl.Museum.GetAsync(id);
            if (item is null)
                return NotFound(new
                {
                    Message = "https://http.cat/404"
                });

            return Ok(item);
        }

        /// <summary>
        /// Update museum by Id or museumId from SIC
        /// </summary>
        /// <response code="202">Update is init</response>
        [HttpGet("{id}/UpdateFromSic")]
        [ProducesResponseType(typeof(ResponseId), StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        public IActionResult Update(string id)
        {
            // string _id;

            // _id = _unitOfWorkBl.Scrapy.Process(id);
            LogDto log;

            log = new LogDto
            {
                DateExecution = DateTime.Now,
                MuseumIdInProcess = id
            };
            log.Id = _unitOfWorkBl.Log.Add(log);
            //_backgroundJobClient.Enqueue(()=>
            _unitOfWorkBl.Scrapy.UpdateMuseums(log);
            //);  

            return Accepted($"Api/Museums/UpdateFromSic/{log.Id}/status", new { Id = log.Id });
        }

        /// <summary>
        /// Update all museums from SIC
        /// </summary>
        /// <response code="202">The update is init</response>
        [ProducesResponseType(typeof(ResponseId), StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        [HttpGet("UpdateFromSic")]
        public IActionResult Update()
        {
            string id;

            id = _unitOfWorkBl.Scrapy.Process();

            return Accepted($"Api/Museums/UpdateFromSic/{id}/status", new { Id = id });
        }

        /// <summary>
        /// Get status of update
        /// </summary>
        /// <param name="id"> id to search</param>
        /// <response code="201">Update finish</response>
        /// <response code="102">Update in process</response>
        [HttpGet("UpdateFromSic/{id}/Status")]
        [ProducesResponseType(typeof(LogDto), StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        public async Task<IActionResult> GetStatus(string id)
        {
            LogDto item;

            item = await _unitOfWorkBl.Log.GetAsync(id);
            if (item.DateEndExecution is null)
                return StatusCode(102, item);

            return Created("https://http.cat/201", item);
        }

        /// <summary>
        /// Cancel the update
        /// </summary>
        /// <param name="id"> id to cancel</param>
        /// <response code="200">Update canceled</response>
        [HttpGet("UpdateFromSic/{id}/Cancel")]
        public async Task<IActionResult> CancelUpdate(string id)
        {
            LogDto item;

            item = await _unitOfWorkBl.Log.GetAsync(id);
            item.DateCancelation = DateTime.Now;
            await _unitOfWorkBl.Log.UpdateAsync(item);

            return Ok(item);
        }
    }
}