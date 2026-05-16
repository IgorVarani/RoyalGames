using Microsoft.Identity.Client;
using Royal_Games.Domains;
using Royal_Games.DTOs.GeneroDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            List<LerGeneroDto> generoDto = generos.Select(genero => new LerGeneroDto
            {
                GeneroId = genero.GeneroID,
                Nome = genero.Nome
            }).ToList();

            return generoDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);
            if (genero == null)
            {
                throw new DomainException("Genero nao encontrado");
            }
            LerGeneroDto generoDto = new LerGeneroDto
            {
                GeneroId = genero.GeneroID,
                Nome = genero.Nome
            };
            return generoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome eh obrigatorio");
            }
        }

        public void Adicionar(AdicionarGeneroDto adicionarDto)
        {
            ValidarNome(adicionarDto.Nome);

            if (_repository.NomeExiste(adicionarDto.Nome))
            {
                throw new DomainException("Categoria ja existente");
            }

            Genero genero = new Genero
            {
                Nome = adicionarDto.Nome
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar(int id,AdicionarGeneroDto adicionarDto)
        {
            ValidarNome(adicionarDto.Nome);

            Genero generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Ja existe outro genero com esse nome");
            }

            generoBanco.Nome = adicionarDto.Nome;
            _repository.Atualizar(generoBanco);
        }

        public void Remover(int id)
        {
            Genero generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Genero nao encontrado");
            }

            _repository.Remover(id);
        }
    }
}
