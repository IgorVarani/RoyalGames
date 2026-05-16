using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.UsuarioDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerUsuarioDto>> Listar()
        {
            List<LerUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDto> ObterPorId(int id)
        {
            LerUsuarioDto usuario = _service.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDto> ObterPorEmail(string email)
        {
            LerUsuarioDto usuario = _service.ObterPorEmail(email);
            if(usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
        [HttpPost]
        public ActionResult<LerUsuarioDto> Adicionar(AdicionarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioAdicionado = _service.Adicionar(usuarioDto);
                return StatusCode(201, usuarioAdicionado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDto> Atualizar(int id, AdicionarUsuarioDto usuarioDto)
        {
            try
            {
                LerUsuarioDto usuarioAtualizado = _service.Atualizar(id, usuarioDto);
                return StatusCode(201, usuarioAtualizado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
