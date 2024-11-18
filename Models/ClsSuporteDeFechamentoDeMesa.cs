using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public class ClsSuporteDeFechamentoDeMesa
{
    public string? NumeroMesaOuComanda { get; set; }
    public float ValorCouvert { get; set; } = 0f;
    public int QtdDePessoas { get; set; } = 0;
}
