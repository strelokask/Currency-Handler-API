using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Currency_Handler_API.Models;
using Currency_Handler_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace Currency_Handler_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public CurrencyService Service { get; }

        public CurrencyController(CurrencyService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                IEnumerable<Valuta> currencies = await Service.GetDataAsync();

                return Ok(currencies
                        .OrderBy(x => x.Name)
                        .Skip((pageNumber ?? 1 - 1) * pageSize ?? 5)
                        .Take(pageSize ?? 5)
                    );
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Currencies(string id)
        {
            try
            {
                var result = (await Service.GetDataAsync())
                    .First(x => x.ID == id);

                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}