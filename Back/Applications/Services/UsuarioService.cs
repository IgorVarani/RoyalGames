using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using Royal_Games.Domains;
using Royal_Games.DTOs.UsuarioDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Royal_Games.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerUsuario = new LerUsuarioDto
            {
                UsuarioId = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? true
            };

            return lerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();
            return usuariosDto;
        }

        public static void ValidarUsuario(bool? StatusUsuario)
        {

            if (StatusUsuario == false)
            {
                throw new DomainException("Usuario Invalido");
            }
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email Invalido");
            }
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome Invalido");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha Invalida");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);
            if (usuario == null)
            {
                throw new DomainException("Usuario nao existe");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);
            if (usuario == null)
            {
                throw new DomainException("Usuario nao existe");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto Adicionar(AdicionarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);
            ValidarNome(usuarioDto.Nome);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Email ja existe");
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true
            };

            _repository.Adicionar(usuario);
            return LerDto(usuario);
        }

        public LerUsuarioDto Atualizar(int id, AdicionarUsuarioDto usuarioDto)
        {
            Usuario? usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            ValidarEmail(usuarioDto.Email);

            Usuario? usuarioMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if (usuarioMesmoEmail != null && usuarioMesmoEmail.UsuarioID == id)
            {
                throw new DomainException("Email Invalido");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);
            return LerDto(usuarioBanco);
        }

        public void Remover(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao encontrado");
            }

            _repository.Remover(id);
        }
    }
}
