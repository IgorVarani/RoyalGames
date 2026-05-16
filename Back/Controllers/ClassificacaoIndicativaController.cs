using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.Domains;
using Royal_Games.DTOs.ClassificacaoIndicativaDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificacaoIndicativaDto>> Listar()
        {
            List<LerClassificacaoIndicativaDto> classificacoes = _service.Listar();
            return Ok(classificacoes);
        }

        [HttpGet("{Id}")]
        public ActionResult<LerClassificacaoIndicativaDto> ObterPorId(int Id)
        {
            LerClassificacaoIndicativaDto classificacaoIndicativa = _service.ObterPorId(Id);

            if (classificacaoIndicativa == null)
            {
                return NotFound();
            }

            return Ok(classificacaoIndicativa);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        [Authorize]
        public ActionResult Atualizar(int Id, CriarClassificacaoIndicativaDto criarDto)
        {
            try
            {
                _service.Atualizar(Id, criarDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public ActionResult Remover(int Id)
        {
            try
            {
                _service.Remover(Id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}