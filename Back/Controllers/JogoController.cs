using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.JogoDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        // Autenticação de usuário.
        private int ObterUsuarioIdLogado()
        {
            // busca no token/claims o valor armazenado como Id do usuário, ClaimTypes.NameIdentifier geralmente guarda o Id do usuário no Jwt.
            string? IdTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(IdTexto))
            {
                throw new DomainException("Usuário não autenticado");
            }

            // Converte o Id que veio como texto para inteiro, nosso UsuarioId no sistema está como int.
            // As Claims (informações do usuário dentro do token) sempre são armazenadas como texto.
            return int.Parse(IdTexto);
        }

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogos = _service.Listar();

            return Ok(jogos);
        }

        [HttpGet("{Id:int}")]
        public ActionResult<LerJogoDto> ObterPorId(int Id)
        {
            LerJogoDto jogo = _service.ObterPorId(Id);

            if (jogo == null)
            {
                return NotFound();
            }

            return Ok(jogo);
        }

        [HttpGet("nome/{Nome}")]
        public ActionResult<LerJogoDto> ObterPorNome(string Nome)
        {
            LerJogoDto jogo = _service.ObterPorNome(Nome);

            if (jogo == null)
            {
                return NotFound();
            }

            return Ok(jogo);
        }

        // GET -> api/produto/5/imagem
        [HttpGet("{Id}/imagem")]
        public ActionResult ObterImagem(int Id)
        {
            try
            {
                var imagem = _service.ObterImagem(Id);
                // Retorna o arquivo para o navegador, "image/jpeg" informa o tipo da imagem (MIME type).
                // O navegador entende que deve renderizar como imagem.
                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost] // indica que recebe dados no formato multipart/form-data, necessário quando enviamos arquivos (ex. imagem do produto).
        [Consumes("multipart/form-data")]
        [Authorize] // exige login para adicionar produtos

        // [FromForm] -> diz que os dados vem do formulário da requisição (multipart/form-data)
        public ActionResult Adicionar([FromForm] CriarJogoDto jogoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();

                // o cadastro fica associado ao usuário logado.
                _service.Adicionar(jogoDto, usuarioId);

                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int Id, [FromForm] AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(Id, jogoDto);
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
                return StatusCode(204, Id);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}