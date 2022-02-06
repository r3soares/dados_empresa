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
    public class ClasseController : ControllerBase
    {
        private readonly IRepository<CnaeClasse> _repo;
        private readonly ILogger<ClasseController> _logger;
        //private readonly CultureInfo dataFormato = CultureInfo.GetCultureInfo("pt-BR");

        public ClasseController(IRepository<CnaeClasse> repo, ILogger<ClasseController> logger)
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
        public async Task<IActionResult> GetSubClasses(string id)
        {
            var t = await _repo.GetById(id);
            return t != null ? Ok(t.Subclasses) : NotFound();
        }
    }
}
