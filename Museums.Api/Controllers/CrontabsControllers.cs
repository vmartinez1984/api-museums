using Microsoft.AspNetCore.Mvc;
using Museums.Core.Dtos;
using Museums.Core.Interfaces;

namespace Museums.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CrontabsController : ControllerBase
    {
        private IUnitOfWorkBl _unitOfWorkBl;
        private ILogger<MuseumsController> _logger;

        public CrontabsController(
           IUnitOfWorkBl unitOfWorkBl
           , ILogger<MuseumsController> logger
       )
        {
            _unitOfWorkBl = unitOfWorkBl;
            _logger = logger;
        }

        /// <summary>
        /// Get list of Crontabs
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return list crontabs</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CrontabDto>> Get()
        {
            List<CrontabDto> list;

            list = await _unitOfWorkBl.Crontab.GetAsync();

            return list;
        }

        /// <summary>
        /// Get list of Crontabs
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Return id</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CrontabDtoIn item)
        {
            string id;

            id = await _unitOfWorkBl.Crontab.AddAsync(item);

            return Created("", new { Id = id });
        }
    }
}