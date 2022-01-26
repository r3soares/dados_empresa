using CriadorBaseDados.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Respositories;
using System.Globalization;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly IRepository<Municipio> _repo;
        private readonly ILogger<MunicipioController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public MunicipioController(IRepository<Municipio> repo, ILogger<MunicipioController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Obtém todas as empresas daquele Municipio
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresas(int id)
        {
            var t = await _repo.GetById(id);
            return t != null ? Ok(t.Empresas) : NotFound();
        }
    }
}
