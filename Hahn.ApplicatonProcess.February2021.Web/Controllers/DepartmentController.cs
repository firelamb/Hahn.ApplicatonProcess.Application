using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDepartmentService _service;

        public DepartmentController(ILogger<AssetController> logger, IDepartmentService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns>A list of departments</returns>
        /// <response code="201">Returns the list of departments</response>
        /// <response code="400">If the list is null or an error occurred</response>  
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentModel>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"[${nameof(DepartmentController)}] GetAll called {DateTimeOffset.UtcNow}");

            var result = await _service.GetAll();

            return Ok(result);
        }
    }
}