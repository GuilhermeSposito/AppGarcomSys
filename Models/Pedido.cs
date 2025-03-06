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
    public string? IdPedido { get; set; } = Guid.NewGuid().ToString();
    public string? NomeClienteNaMesa { get; set; } = String.Empty; //Nome do cliente na mesa
    public string? HorarioFeito { get; set; } = DateTimeOffset.Now.ToLocalTime().ToString();
    public string? GarcomResponsavel { get; set; }
    public List<Produto> produtos { get; set; } = new List<Produto>();
    public bool EBalcao { get; set; }
    public Balcao? BalcaoInfos { get; set; } = null;
}

public class Balcao
{
    public bool Repetido { get; set; } = false;
    public string? CodBalcao { get; set; } = String.Empty;
    public string? NomeCliente { get; set; } = String.Empty;
}