namespace Royal_Games.DTOs.LogAlteracaoJogoDto
{
    public class LerLogJogoDto
    {
        public int Log_AlteracaoJogoID { get; set; }
        public int? JogoID { get; set; }
        public string NomeAnterior { get; set; } = null!;
        public decimal? PrecoAnterior { get; set; }
        public DateTime DataAlteracao { get; set; }

    }
}
