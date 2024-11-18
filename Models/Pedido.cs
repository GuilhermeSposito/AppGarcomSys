using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public class Pedido
{
    public string? Mesa { get; set; }
    public float? Couvert { get; set; } = 0f;
    public string? Comanda { get; set; }
    public string? HorarioFeito { get; set; } = DateTimeOffset.Now.ToLocalTime().ToString();
    public string? GarcomResponsavel { get; set; }
    public List<Produto> produtos { get; set; } = new List<Produto>();
}
