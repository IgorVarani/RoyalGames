using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAlteracaoJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogAlteracaoJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }
        [HttpGet("produto/{id}")]
        [Authorize]
        public ActionResult ListarJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }



    }
}
