using Royal_Games.Domains;
using Royal_Games.DTOs.EstadoConservacaoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class EstadoConservacaoService
    {
        private readonly IEstadoConservacaoRepository _repository;

        public EstadoConservacaoService(IEstadoConservacaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerEstadoConservacaoDto> Listar()
        {
            List<EstadoConservacao> estados = _repository.Listar();
            List<LerEstadoConservacaoDto> estadoDto = estados.Select(estadoC => new LerEstadoConservacaoDto
            {
                EstadoConservacaoId = estadoC.EstadoConservacaoID,
                Nome = estadoC.Nome,
            }).ToList();

            return estadoDto;
        }

        public LerEstadoConservacaoDto ObterPorId(int Id)
        {
            EstadoConservacao estadoC = _repository.ObterPorId(Id);

            if (estadoC == null)
            {
                throw new DomainException("Estado não encontrado.");
            }

            LerEstadoConservacaoDto estadoDto = new LerEstadoConservacaoDto()
            {
                EstadoConservacaoId = estadoC.EstadoConservacaoID,
                Nome = estadoC.Nome,
            };

            return estadoDto;
        }

        private static void ValidarEstado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarEstadoConservacaoDto criarDto)
        {
            ValidarEstado(criarDto.Nome);

            if (_repository.EstadoConservacaoExiste(criarDto.Nome))
            {
                throw new DomainException("Estado já existente.");
            }

            EstadoConservacao estadoC = new EstadoConservacao()
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(estadoC);
        }

        public void Atualizar(int Id, CriarEstadoConservacaoDto criarDto)
        {
            ValidarEstado(criarDto.Nome);

            EstadoConservacao estadoBanco = _repository.ObterPorId(Id);

            if (_repository.EstadoConservacaoExiste(criarDto.Nome, EstadoConservacaoIdAtual: Id))
            {
                throw new DomainException("Já existe outro estado com esse nome.");
            }

            estadoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(estadoBanco);
        }

        public void Remover(int Id)
        {
            EstadoConservacao estadoBanco = _repository.ObterPorId(Id);

            if (estadoBanco == null)
            {
                throw new DomainException("Estado não encontrado.");
            }

            _repository.Remover(Id);
        }
    }
}
