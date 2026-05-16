using System.Security.Cryptography;
using System.Text;
using Royal_Games.Domains;
using Royal_Games.DTOs.PlataformaDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        private static LerPlataformaDto LerDto(Plataforma plataforma)
        {
            LerPlataformaDto lerPlataforma = new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaID,
                Nome = plataforma.Nome
            };

            return lerPlataforma;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            List<LerPlataformaDto> plataformaDto = plataformas
                .Select(plataformaBanco => LerDto(plataformaBanco))
                .ToList();

            return plataformaDto;
        }

        public LerPlataformaDto ObterPorId(int Id)
        {
            Plataforma? plataforma = _repository.ObterPorId(Id);

            if (plataforma == null)
            {
                throw new DomainException("Plataforma não existe.");
            }

            return LerDto(plataforma);
        }

        private static void ValidarNome(CriarPlataformaDto plataformaDto)
        {
            if(string.IsNullOrWhiteSpace(plataformaDto.Nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto);

            if(_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Já existe uma plataforma com este nome.");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(plataforma);
        }

        public void Atualizar(int Id, CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto);

            Plataforma plataformaBanco = _repository.ObterPorId(Id)!;

            if(plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            if(_repository.NomeExiste(criarDto.Nome, plataformaIdAtual: Id))
            {
                throw new DomainException("Já existe outra plataforma com esse nome.");
            }

            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover(int Id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(Id)!;

            if(plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            _repository.Remover(Id);
        }
    }
}
