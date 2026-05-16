using Microsoft.EntityFrameworkCore;
using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public JogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Usuario)
                .Include(jogo => jogo.Plataforma)
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .ToList();

            return jogos;
        }

        public Jogo ObterPorId(int Id)
        {
            Jogo? jogo = _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Log_AlteracaoJogo)
                .Include(jogo => jogo.Plataforma)
                .Include(jogo => jogo.Usuario)
                .FirstOrDefault(jogoDB => jogoDB.JogoID == Id);

            return jogo;
        }

        public Jogo? ObterPorNome(string nome)
        {
            Jogo? jogoNome = _context.Jogo
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .Include(jogo => jogo.Usuario)
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .FirstOrDefault(jogoNome => jogoNome.Nome == nome);

            return jogoNome;
        }
        public bool JogoExiste(string nome, int? jogoIdAtual = null)
        {
            var jogoConsultado = _context.Jogo.AsQueryable();

            if (jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int Id)
        {
            var jogo = _context.Jogo
                .Where(jogo => jogo.JogoID == Id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }

        public void Adicionar(Jogo jogo, List<int> GeneroID, List<int> PlataformaID)
        {
            List<Genero> generos = _context.Genero.Where(genero => GeneroID.Contains(genero.GeneroID)).ToList();
            List<Plataforma> plataformas = _context.Plataforma.Where(plataforma => PlataformaID.Contains(plataforma.PlataformaID)).ToList();

            jogo.Genero = generos;
            jogo.Plataforma = plataformas;
                   

            foreach (var genero in generos)
            {
                jogo.Genero.Add(genero);
            }

            foreach (var plataforma in plataformas)
            {
                jogo.Plataforma.Add(plataforma);
            }

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> GeneroID, List<int> PlataformaID)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .FirstOrDefault(jogoAux => jogo.JogoID == jogo.JogoID);

            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if (jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

            var generos = _context.Genero
                .Where(genero => GeneroID.Contains(genero.GeneroID))
                .ToList();

            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
            { 
                jogoBanco.Genero.Add(genero); 
            }

            var plataformas = _context.Plataforma
                .Where(plataforma => PlataformaID.Contains(plataforma.PlataformaID))
                .ToList();

            jogoBanco.Plataforma.Clear();

            foreach (var plataforma in plataformas)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }

            _context.SaveChanges();
        }

        public void Remover(int Id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == Id);

            if (jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }
    }
}