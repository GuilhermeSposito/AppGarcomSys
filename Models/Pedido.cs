using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public class Pedido
{
    public string? Mesa { get; set; }
    public string? Comanda { get; set; }
    public string? HorarioFeito { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    public string? GarcomResponsavel { get; set; }
    public List<Produto> produtos { get; set; } = new List<Produto>();
}
