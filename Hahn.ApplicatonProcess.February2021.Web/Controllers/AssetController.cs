using System;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAssetService _service;

        public AssetController(ILogger<AssetController> logger, IAssetService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get one asset given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A asset</returns>
        /// <response code="201">Returns the asset</response>
        /// <response code="400">If the asset is null or an error occurred</response>  
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(AssetModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"[${nameof(AssetController)}] GetById called {DateTimeOffset.UtcNow}");

            var result = await _service.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Add one asset given an id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "assetName": "This is a name",
        ///         "department": 1,
        ///         "countryOfDepartment": "Dominican Republic",
        ///         "eMailAdressOfDepartment": "ronnysuero@gmail.com",
        ///         "purchaseDate": "2021-04-24T18:50:49.114Z",
        ///         "broken": true
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A asset</returns>
        /// <response code="201">Returns the new created asset</response>
        /// <response code="400">If the asset is null or an error occurred</response>  
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AssetModel), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> Insert(AssetModel model)
        {
            _logger.LogInformation($"[${nameof(AssetController)}] Insert called {DateTimeOffset.UtcNow}");

            var result = await _service.Insert(model);

            return StatusCode((int) HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Update one asset given an id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A asset</returns>
        /// <response code="201">Returns the asset</response>
        /// <response code="400">If the asset is null or an error occurred</response> 
        [HttpPut("[action]")]
        [ProducesResponseType(typeof(AssetModel), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Update(AssetModel model)
        {
            _logger.LogInformation($"[${nameof(AssetController)}] Update called {DateTimeOffset.UtcNow}");

            var result = await _service.Update(model);

            return Ok(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Delete one asset given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A asset</returns>
        /// <response code="201">Returns true if the asset was deleted</response>
        /// <response code="400">If the asset is null or an error occurred</response> 
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"[${nameof(AssetController)}] Delete called {DateTimeOffset.UtcNow}");

            var result = await _service.Delete(id);

            return Ok(result);
        }
    }
}