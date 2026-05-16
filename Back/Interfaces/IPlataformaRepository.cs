using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IPlataformaRepository
    {
        List<Plataforma> Listar();

        // Pode ser que não retorne plataformas na busca, então coloca-se "?" para permitir ser nulo.
        Plataforma? ObterPorId(int Id);

        bool NomeExiste(string nome, int? plataformaIdAtual = null);

        void Adicionar(Plataforma plataforma);
        void Atualizar(Plataforma plataforma);
        void Remover(int Id);
    }
}