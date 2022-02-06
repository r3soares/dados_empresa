using CriadorBaseDados.Model.DB;
using CriadorBaseDados.Model.DB.CNAE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Respositories;
using System.Globalization;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SubClasseController : ControllerBase
    {
        private readonly IRepository<CnaeSubclasse> _repo;
        private readonly ILogger<SubClasseController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public SubClasseController(IRepository<CnaeSubclasse> repo, ILogger<SubClasseController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public async Task<IActionResult> GetAll()
        {
            var t = await _repo.GetAll();
            return t != null ? Ok(t) : NotFound();
        }

        //Obtém todas as divisões da seção informada
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresas(string id)
        {
            var t = await _repo.GetById(id);
            return t != null ? Ok(t.Empresas) : NotFound();
        }
    }
}
