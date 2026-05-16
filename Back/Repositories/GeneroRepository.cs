using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly Royal_GamesContext _context;

        public GeneroRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Genero> Listar()
        {
            return _context.Genero.ToList();
        }

        public Genero? ObterPorId(int id)
        {
            return _context.Genero.Find(id);
        }

        public bool NomeExiste(string nome, int? GeneroIdAtual = null)
        {
            var consulta = _context.Genero.AsQueryable();

            if (GeneroIdAtual.HasValue)
            {

                consulta = consulta.Where(genero => genero.GeneroID != GeneroIdAtual.Value);
            }

            return consulta.Any(g => g.Nome == nome);
        }

        public void Adicionar(Genero genero)
        {
            _context.Genero.Add(genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero genero)
        {
            Genero? generoBanco = _context.Genero.FirstOrDefault(genAux => genAux.GeneroID == genero.GeneroID);
            if (generoBanco == null)
                return;

            generoBanco.Nome = genero.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Genero? genero = _context.Genero.FirstOrDefault(genAux => genAux.GeneroID == id);
            if (genero == null)
                return;

            _context.Genero.Remove(genero);
            _context.SaveChanges();
        }

    }
}
