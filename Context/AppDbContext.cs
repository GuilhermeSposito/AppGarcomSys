using AppGarcomSys.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Context
{
    public class AppDbContext : DbContext
    {

        public string? ConnectionString { get; set; } = $"Host=192.168.0.1;Port=5432;Database=integrador;Username=postgres;Password=69063360;";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           string? NewConnectionString = $"Host={AppState.IpDoBanco};Port=5432;Database=integrador;Username=postgres;Password=69063360;";
            ConnectionString = NewConnectionString;

        }

        public AppDbContext()
        {
            string? NewConnectionString = $"Host={AppState.IpDoBanco};Port=5432;Database=integrador;Username=postgres;Password=69063360;";
            ConnectionString = NewConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }



        public DbSet<Garcom> garcons { get; set; }
        public DbSet<Produto> cardapio { get; set; }
        public DbSet<Mesa> mesas { get; set; }
        public DbSet<Grupo> grupos { get; set; }
        public DbSet<Contas> contas { get; set; }
        public DbSet<Incremento> incrementos { get; set; }
        public DbSet<IncrementoCardapio> incrementocardapio { get; set; }
        public DbSet<ConfigAppGarcom> configappgarcom { get; set; }

    }
}
