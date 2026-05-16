using Microsoft.EntityFrameworkCore;
using Royal_Games.Applications.Conversoes;
using Royal_Games.Applications.Regras;
using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Royal_Games.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        // Para cada jogo que veio do banco
        // Crie um Dto só com o que a requisição/front precisa.
        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            // SELECT percorre cada Produto e transforma em Dto -> LerProdutoDto
            List<LerJogoDto> jogosDto =
                jogos.Select(JogoParaDto.ConverterParaDto).ToList();

            return jogosDto;
        }

        public LerJogoDto ObterPorId(int Id)
        {
            Jogo jogo = _repository.ObterPorId(Id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            // converte o jogo encontrado para Dto e devolve.
            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto ObterPorNome(string Nome)
        {
            Jogo jogo = _repository.ObterPorNome(Nome);

            if (jogo == null)
            {
                throw new DomainException("Jogo nao encontrado");
            }

            return JogoParaDto.ConverterParaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior do que zero.");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória.");
            }

            if (jogoDto.GeneroID == null || jogoDto.GeneroID.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos um gênero.");
            }

            if (jogoDto.PlataformaID == null || jogoDto.PlataformaID.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos uma plataforma.");
            }
        }

        public byte[] ObterImagem(int Id)
        {
            byte[] imagem = _repository.ObterImagem(Id);

            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if (_repository.JogoExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioId
            };


            _repository.Adicionar(jogo, jogoDto.GeneroID, jogoDto.PlataformaID);

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int Id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(Id);

            if (jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            // jogoIdAtual: -> dois pontos serve para passar o valor do parâmetro.
            if (_repository.JogoExiste(jogoDto.Nome, jogoIdAtual: Id))
            {
                throw new DomainException("Já existe outro jogo com esse nome.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior do que zero.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }       

            if (jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.GeneroID, jogoDto.PlataformaID);

            return JogoParaDto.ConverterParaDto(jogoBanco);
        }

        public void Remover(int Id)
        {            

            Jogo jogo = _repository.ObterPorId(Id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            _repository.Remover(Id);
        }
    }
}
