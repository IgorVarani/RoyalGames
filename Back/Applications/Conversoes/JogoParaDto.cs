using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDto;


namespace Royal_Games.Applications.Conversoes
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo ?? true,

                PlataformaID = jogo.Plataforma?.Select(p => p.PlataformaID).ToList() ?? new List<int>(),
                Plataformas = jogo.Plataforma?.Select(p => p.Nome).ToList() ?? new List<string>(),

                GeneroID = jogo.Genero?.Select(g=> g.GeneroID).ToList() ?? new List<int>(),
                Generos = jogo.Genero?.Select(g => g.Nome).ToList() ?? new List<string>(),

                ClassificacaoId = jogo.ClassificacaoIndicativaID,
                Classificacao = jogo.ClassificacaoIndicativa?.Classificacao,

                ImagemUrl = $"jogo/{jogo.JogoID}/imagem",

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };
        }
    }
}