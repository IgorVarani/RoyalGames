using Royal_Games.Applications.Regras;
using Royal_Games.Domains;
using Royal_Games.DTOs.PromocaoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class PromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerPromocaoDto> Listar()
        {
            List<Promocao> promocoes = _repository.Listar();

            List<LerPromocaoDto> promocoesDto = promocoes.Select(promocao => new LerPromocaoDto
            {
                PromocaoId = promocao.PromocaoID,
                Nome = promocao.Nome,
                DataExpiracao = promocao.DataExpiracao,
                StatusPromocao = promocao.StatusPromocao

            }).ToList();

            return promocoesDto;
        }

        public LerPromocaoDto ObterPorId(int id)
        {
            Promocao promocao = _repository.ObterPorId(id);
            if (promocao == null)
            {
                throw new DomainException("Promocao nao encontrada");
            }
            LerPromocaoDto promocaoDto = new LerPromocaoDto()
            {
                PromocaoId = promocao.PromocaoID,
                Nome = promocao.Nome,
                DataExpiracao = promocao.DataExpiracao,
                StatusPromocao = promocao.StatusPromocao
            };
            return promocaoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome invalido");
            }
        }

        public void Adicionar(AdicionarPromocaoDto promoDto)
        {
            ValidarNome(promoDto.Nome);
            ValidarDataExpiracaoPromocao.ValidarDataExpiracao(promoDto.DataExpiracao);

            if (_repository.NomeExiste(promoDto.Nome))
            {
                throw new DomainException("Nome invalido");
            }

            Promocao promocao = new Promocao
            {
                Nome = promoDto.Nome,
                DataExpiracao = promoDto.DataExpiracao,
                StatusPromocao = promoDto.StatusPromocao
            };

            _repository.Adicionar(promocao);
        }

        public void Atualizar(int id, AdicionarPromocaoDto promoDto)
        {
            ValidarNome(promoDto.Nome);
            ValidarDataExpiracaoPromocao.ValidarDataExpiracao(promoDto.DataExpiracao);

            Promocao promocaoBanco = _repository.ObterPorId(id);

            if (promocaoBanco == null)
            {
                throw new DomainException("Promocao nao encontrada");
            }

            if (_repository.NomeExiste(promoDto.Nome, promocaoIdAtual:id))
            {
                throw new DomainException("Essa promocao ja existe");
            }

            promocaoBanco.Nome = promoDto.Nome;
            promocaoBanco.DataExpiracao = promoDto.DataExpiracao;
            promocaoBanco.StatusPromocao = promoDto.StatusPromocao;

            _repository.Atualizar(promocaoBanco);
        }

        public void Remover(int id)
        {
            Promocao promocao = _repository.ObterPorId(id);

            if (promocao == null)
            {
                throw new DomainException("Promocao nao encontrada");
            }

            _repository.Remover(id);
        }
    }
}
