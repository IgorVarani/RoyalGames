using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IClassificacaoIndicativaRepository
    {
        List<ClassificacaoIndicativa> Listar();
        ClassificacaoIndicativa ObterPorId(int Id);

        bool ClassificacaoIndicativaExiste(string Classificacao, int? ClassificacaoIndicativaIdAtual = null);

        void Adicionar(ClassificacaoIndicativa classificacaoIndicativa);
        void Atualizar(ClassificacaoIndicativa classificacaoIndicativa);
        void Remover(int Id);
    }
}