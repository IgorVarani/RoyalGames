using Royal_Games.Domains;
using Royal_Games.DTOs.ClassificacaoIndicativaDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativaRepository _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativaRepository repository)
        {
            _repository = repository;
        }

        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacoes = _repository.Listar();

            List<LerClassificacaoIndicativaDto> classificacaoIndicativaDto = classificacoes.Select(classificacaoIndicativa => new LerClassificacaoIndicativaDto
            {
                ClassificacaoIndicativaId = classificacaoIndicativa.ClassificacaoIndicativaID,
                Classificacao = classificacaoIndicativa.Classificacao,
            }).ToList();

            return classificacaoIndicativaDto;
        }

        public LerClassificacaoIndicativaDto ObterPorId(int Id)
        {
            ClassificacaoIndicativa classificacaoIndicativa = _repository.ObterPorId(Id);

            if(classificacaoIndicativa == null)
            {
                throw new DomainException("Classificação Indicativa não encontrada.");
            }

            LerClassificacaoIndicativaDto classificacaoIndicativaDto = new LerClassificacaoIndicativaDto()
            {
                ClassificacaoIndicativaId = classificacaoIndicativa.ClassificacaoIndicativaID,
                Classificacao = classificacaoIndicativa.Classificacao,
            };

            return classificacaoIndicativaDto;
        }

        private static void ValidarClassificacaoIndicativa(string Classificacao)
        {
            if (string.IsNullOrWhiteSpace(Classificacao))
            {
                throw new DomainException("A Classificação é obrigatória.");
            }
        }

        public void Adicionar(CriarClassificacaoIndicativaDto criarDto)
        {
            ValidarClassificacaoIndicativa(criarDto.Classificacao);

            if (_repository.ClassificacaoIndicativaExiste(criarDto.Classificacao))
            {
                throw new DomainException("Classificação já existente.");
            }

            ClassificacaoIndicativa classificacaoIndicativa = new ClassificacaoIndicativa
            {
                Classificacao = criarDto.Classificacao,
            };

            _repository.Adicionar(classificacaoIndicativa);
        }

        public void Atualizar(int Id, CriarClassificacaoIndicativaDto criarDto)
        {
            ValidarClassificacaoIndicativa(criarDto.Classificacao);

            ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(Id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Já existe.");
            }

            classificacaoBanco.Classificacao = criarDto.Classificacao;
            _repository.Atualizar(classificacaoBanco);
        }

        public void Remover(int Id)
        {
            ClassificacaoIndicativa classificacaoBanco = _repository.ObterPorId(Id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Classificação não encontrada.");
            }

            _repository.Remover(Id);
        }
    }
}
