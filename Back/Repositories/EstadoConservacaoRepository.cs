using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class EstadoConservacaoRepository : IEstadoConservacaoRepository
    {
        private readonly Royal_GamesContext _context;

        public EstadoConservacaoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<EstadoConservacao> Listar()
        {
            return _context.EstadoConservacao.ToList();
        }

        public EstadoConservacao ObterPorId(int Id)
        {
            EstadoConservacao estadoC = _context.EstadoConservacao.FirstOrDefault(ec => ec.EstadoConservacaoID == Id)!;

            return estadoC;
        }

        public bool EstadoConservacaoExiste(string nome, int? EstadoConservacaoIdAtual = null)
        {
            var consulta = _context.EstadoConservacao.AsQueryable();

            if (EstadoConservacaoIdAtual.HasValue)
            {
                consulta = consulta.Where(estadoC => estadoC.EstadoConservacaoID != EstadoConservacaoIdAtual.Value);
            }

            return consulta.Any(ec => ec.Nome == nome);
        }

        public void Adicionar(EstadoConservacao estadoC)
        {
            _context.EstadoConservacao.Add(estadoC);
            _context.SaveChanges();
        }

        public void Atualizar(EstadoConservacao estadoC)
        {
            EstadoConservacao estadoBanco = _context.EstadoConservacao.FirstOrDefault(ec => ec.EstadoConservacaoID == estadoC.EstadoConservacaoID)!;

            if (estadoBanco != null)
            {
                return;
            }

            estadoBanco!.Nome = estadoC.Nome;

            _context.SaveChanges();
        }

        public void Remover(int Id)
        {
            EstadoConservacao estadoBanco = _context.EstadoConservacao.FirstOrDefault(ec => ec.EstadoConservacaoID == Id)!;

            if (estadoBanco == null)
            {
                return;
            }

            _context.EstadoConservacao.Remove(estadoBanco);
            _context.SaveChanges();
        }
    }
}