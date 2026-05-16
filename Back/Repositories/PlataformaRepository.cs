using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly Royal_GamesContext _context;

        public PlataformaRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }

        public Plataforma? ObterPorId(int Id)
        {
            // "Find" performa melhor com chave primária.
            return _context.Plataforma.Find(Id);
        }

        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var plataformaConsultada = _context.Plataforma.AsQueryable();

            if (plataformaIdAtual.HasValue)
            {
                plataformaConsultada = plataformaConsultada.Where(plataforma => plataforma.PlataformaID != plataformaIdAtual.Value);
            }

            return plataformaConsultada.Any(plataforma => plataforma.Nome == nome);
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar(Plataforma plataforma)
        {
            Plataforma? plataformaBanco =
                _context.Plataforma.FirstOrDefault(plataforma => plataforma.PlataformaID == plataforma.PlataformaID);

            if (plataformaBanco == null)
            {
                return;
            }

            plataformaBanco.Nome = plataforma.Nome;

            _context.SaveChanges();
        }

        public void Remover(int Id)
        {
            Plataforma? plataforma = _context.Plataforma.FirstOrDefault(plataforma => plataforma.PlataformaID == Id);

            if (plataforma == null)
            {
                return;
            }

            _context.Plataforma.Remove(plataforma);
            _context.SaveChanges();
        }
    }
}