using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public class ConfigAppGarcom
{
    [Column("id")] public int Id { get; set; }
    [Column("listadeitens")] public bool ListaDeItens { get; set; }
    [Column("buscadeitens")] public bool BuscaDeItens { get; set; }
    [Column("listaporgrupo")] public bool ListaPorGrupo { get; set; }
    [Column("requisicaoalfanumerica")] public bool RequisicaoAlfaNumerica { get; set; }
    [Column("requisicaonumerica")] public bool RequisicaoNumerica { get; set; }
    [Column("comanda")] public bool Comanda { get; set; }
    [Column("mesa")] public bool Mesa { get; set; }
    [Column("semrequisicao")] public bool SemRequisicao { get; set; }
}

