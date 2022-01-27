using CriadorBaseDados.Model.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Respositories;
using System.Globalization;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MotivoController : ControllerBase
    {
        private readonly IRepository<MotivoSituacao> _repo;
        private readonly ILogger<MotivoController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public MotivoController(IRepository<MotivoSituacao> repo, ILogger<MotivoController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Obtém todos os municipios daquele estado
        [HttpGet("{cod}")]
        public async Task<IActionResult> Get(int cod)
        {
            var t = await _repo.GetById(cod);
            return t != null ? Ok(t) : NotFound();
        }
    }
}
