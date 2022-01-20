using CriadorBaseDados.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Respositories;
using System.Globalization;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IRepository<Estado> _repo;
        private readonly ILogger<EstadoController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public EstadoController(IRepository<Estado> repo, ILogger<EstadoController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Obtém todos os municipios daquele estado
        [HttpGet("{uf}")]
        public async Task<IActionResult> GetMunicipios(string uf)
        {
            var t = await _repo.GetById(uf);
            return t != null ? Ok(t.Municipios) : NotFound();
        }
    }
}
