using AppGarcomSys.Context;
using AppGarcomSys.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Repositorys;

public class GruposRepository
{
    private readonly AppDbContext _context;

    public GruposRepository()
    {
        _context = new AppDbContext();
    }

    public async Task<List<Grupo>> GetGrupos()
    {
        return await _context.grupos.ToListAsync();
    }
}
