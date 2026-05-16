namespace Royal_Games.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public bool? StatusJogo { get; set; }
        public int ClassificacaoId { get; set; }
        public List<int> GeneroID { get; set; } = new();
        public List<int> PlataformaID { get; set; } = new();       
    }
}