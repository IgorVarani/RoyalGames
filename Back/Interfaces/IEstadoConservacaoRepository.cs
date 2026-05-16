using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IEstadoConservacaoRepository
    {
        List<EstadoConservacao> Listar();
        EstadoConservacao ObterPorId(int Id);

        bool EstadoConservacaoExiste(string nome, int? EstadoConservacaoIdAtual = null);

        void Adicionar(EstadoConservacao estadoC);
        void Atualizar(EstadoConservacao estadoC);
        void Remover(int Id);
    }
}