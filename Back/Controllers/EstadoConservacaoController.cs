using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.EstadoConservacaoDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoConservacaoController : ControllerBase
    {
        private readonly EstadoConservacaoService _service;

        public EstadoConservacaoController(EstadoConservacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerEstadoConservacaoDto>> Listar()
        {
            List<LerEstadoConservacaoDto> estados = _service.Listar();
            return Ok(estados);
        }

        [HttpGet("{Id}")]
        public ActionResult<LerEstadoConservacaoDto> ObterPorId(int Id)
        {
            LerEstadoConservacaoDto estadoC = _service.ObterPorId(Id);

            if (estadoC == null)
            {
                return NotFound();
            }

            return Ok(estadoC);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarEstadoConservacaoDto criarDto)
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
        public ActionResult Atualizar(int Id, CriarEstadoConservacaoDto criarDto)
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