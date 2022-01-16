using CriadorBaseDados.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Respositories;
using System.Globalization;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly IRepository<Estado> _repo;
        private readonly ILogger<EstadosController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public EstadosController(IRepository<Estado> repo, ILogger<EstadosController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{uf}")]
        public async Task<IActionResult> Get(string uf)
        {
            var t = await _repo.GetById(uf);
            return t != null ? Ok(t) : NotFound();
        }
    }
}
