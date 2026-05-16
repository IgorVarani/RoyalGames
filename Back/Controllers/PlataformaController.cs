using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.PlataformaDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly PlataformaService _service;

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        [HttpGet] // "GET" --> Listar
        public ActionResult<List<LerPlataformaDto>> Listar()
        {
            List<LerPlataformaDto> plataformas = _service.Listar();
            return Ok(plataformas);
        }

        [HttpGet("{Id}")] // "GET" --> Listar
        public ActionResult<LerPlataformaDto> ObterPorId(int Id)
        {
            LerPlataformaDto plataforma = _service.ObterPorId(Id);

            if (plataforma == null)
            {
                return NotFound();
            }

            return Ok(plataforma);
        }

        [HttpPost] // "Post" --> Adicionar/Cadastrar
        [Authorize]
        public ActionResult Adicionar(CriarPlataformaDto criarDto)
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

        [HttpPut("{Id}")] // "Put" --> Atualizar/Substituir
        [Authorize]
        public ActionResult Atualizar(int Id, CriarPlataformaDto criarDto)
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

        [HttpDelete("{Id}")] // "Delete" --> Remover
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