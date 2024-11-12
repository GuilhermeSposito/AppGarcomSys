using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public class ClsDeAcesso
{
    [Column("id")]public int Id { get; set; }
    [Column("usuario")]public string? Usuario { get; set; }
    [Column("senha")] public string? Senha { get; set; }

}
