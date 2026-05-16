using System;
using System.Collections.Generic;

namespace Royal_Games.Domains;

public partial class Promocao
{
    public int PromocaoID { get; set; }

    public DateTime DataExpiracao { get; set; }

    public bool StatusPromocao { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<JogoPromocao> JogoPromocao { get; set; } = new List<JogoPromocao>();
}
