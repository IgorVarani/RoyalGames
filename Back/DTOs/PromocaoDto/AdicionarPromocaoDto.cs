namespace Royal_Games.DTOs.PromocaoDto
{
    public class AdicionarPromocaoDto
    {
        public string Nome { get; set; } = null!;
        public DateTime DataExpiracao { get; set; }
        public bool StatusPromocao { get; set; }
    }
}
