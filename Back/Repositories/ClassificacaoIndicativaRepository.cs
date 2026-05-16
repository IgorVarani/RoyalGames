using Microsoft.EntityFrameworkCore;
using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativaRepository
    {
        private readonly Royal_GamesContext _context;

        public ClassificacaoIndicativaRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int Id)
        {
            ClassificacaoIndicativa classificacaoIndicativa = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == Id)!;

            return classificacaoIndicativa!;
        }

        public bool ClassificacaoIndicativaExiste(string Classificacao, int? ClassificacaoIndicativaIdAtual = null)
        {
            var consulta = _context.ClassificacaoIndicativa.AsQueryable();

            if (ClassificacaoIndicativaIdAtual.HasValue)
            {
                consulta = consulta.Where(classificacaoIndicativa => classificacaoIndicativa.ClassificacaoIndicativaID != ClassificacaoIndicativaIdAtual.Value);
            }

            return consulta.Any(c => c.Classificacao == Classificacao);
        }

        public void Adicionar(ClassificacaoIndicativa classificacaoIndicativa)
        {
            _context.ClassificacaoIndicativa.Add(classificacaoIndicativa);
            _context.SaveChanges();
        }

        public void Atualizar(ClassificacaoIndicativa classificacaoIndicativa)
        {
            ClassificacaoIndicativa classificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(
                c => c.ClassificacaoIndicativaID == classificacaoIndicativa.ClassificacaoIndicativaID)!;

            if (classificacaoBanco != null)
            {
                return;
            }

            classificacaoBanco!.Classificacao = classificacaoIndicativa.Classificacao;

            _context.SaveChanges();
        }

        public void Remover(int Id)
        {
            ClassificacaoIndicativa classificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(
                c => c.ClassificacaoIndicativaID == Id)!;

            if (classificacaoBanco == null)
            {
                return;
            }

            _context.ClassificacaoIndicativa.Remove(classificacaoBanco);
            _context.SaveChanges();
        }
    }
}