using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int Id);
        Jogo ObterPorNome(string Nome);
        byte[] ObterImagem(int Id);
        bool JogoExiste(string nome, int? jogoIdAtual = null);

        void Adicionar(Jogo jogo, List<int> GeneroID, List<int>PlataformaID);
        void Atualizar(Jogo jogo, List<int> GeneroID,List<int>PlataformaID);
        void Remover(int Id);
    }
}