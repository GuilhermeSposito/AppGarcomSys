using AppGarcomSys.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppGarcomSys.Models;

public static class AppState
{
    public static bool Autorizado { get; set; } = false; //Se o usuário está autorizado
    public static string? IpDoBanco { get; set; } = "192.168.0.5"; //Se o caminho do banco de dados foi configurado
    public static bool HabilitarOpcDesenvolvedor = false; //Se o modo desenvolvedor está habilitado

    public static List<Grupo>? GruposMemoria { get; set; } //Categorias Já carregadas na memória do aplicativo
    public static List<Produto>? ProdutosMemoria { get; set; } //Produtos Já carregados na memória do aplicativo
    public static List<Mesa>? MesasNaMemoria { get; set; } //Mesas Já carregadas na memória do aplicativo
    public static List<Incremento>? IncrementosNaMemoria { get; set; } //Incrementos Já carregados na memória do aplicativo
    public static List<IncrementoCardapio>? IncrementoCardapioNaMemoria { get; set; } //IncrementoCardapio Já carregados na memória do aplicativo
    public static List<Garcom>? GarconsNaMemoria { get; set; } //Garçons Já carregados na memória do aplicativo
    public static Garcom? GarconLogado { get; set; } //Garçom logado no sistema

    public static int NumeroDaMesa { get; set; } //Número da mesa selecionada
    public static List<Produto>? ProdutosCarrinho { get; set; } = new List<Produto>(); //Produtos no carrinho

    public static List<Contas> ContasNaMemoria { get; set; } = new List<Contas>(); //Contas Já carregadas na memória do aplicativo
    public static ConfigAppGarcom configuracaoDoApp { get; set; } = new ConfigAppGarcom(); //Configurações do aplicativo

    public async static Task<string>? CarregarConfigs()
    {
        try
        {
            string? ConfigDoIP = await SecureStorage.GetAsync("IP");
            if (ConfigDoIP is not null)
            {
                IpDoBanco = ConfigDoIP;
                return ConfigDoIP;
            }
            else
            {
                IpDoBanco = "192.168.0.1";
                return "192.168.0.1";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return "192.168.0.1";
    }

    public async static Task CarregaConfigsDeParametros()
    {
        try
        {

            using (AppDbContext db = new AppDbContext())
            {
                configuracaoDoApp = await db.configappgarcom.FirstOrDefaultAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async static Task CarregarContas()
    {
        ContasNaMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                ContasNaMemoria = db.contas.ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async static Task CarregarGarcons()
    {
        GarconsNaMemoria = null;

        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                GarconsNaMemoria = await db.garcons.ToListAsync();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async static Task CarregarGrupos()
    {
        GruposMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                GruposMemoria = await db.grupos.ToListAsync();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    public async static Task CarregarProdutos()
    {
        ProdutosMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                ProdutosMemoria = await db.cardapio.ToListAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async static Task CarregarMesas()
    {
        MesasNaMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                MesasNaMemoria = await db.mesas.ToListAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async static Task CarregarIncremento()
    {
        IncrementosNaMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                IncrementosNaMemoria = await db.incrementos.ToListAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async static Task CarregarIncrementosCardapio()
    {
        IncrementoCardapioNaMemoria = null;
        try
        {
            await using (AppDbContext db = new AppDbContext())
            {
                IncrementoCardapioNaMemoria = await db.incrementocardapio.ToListAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public static void HideKeyboard(Entry entry)
    {
#if ANDROID
        var context = entry.Handler?.MauiContext?.Context;
        var inputMethodManager = context?.GetSystemService(Android.Content.Context.InputMethodService)
                               as Android.Views.InputMethods.InputMethodManager;

        if (inputMethodManager != null && context is Android.App.Activity activity)
        {
            var token = activity.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, Android.Views.InputMethods.HideSoftInputFlags.None);
            activity.Window.DecorView.ClearFocus(); // Remove o foco do Entry
        }
#elif IOS || MACCATALYST
        UIKit.UIApplication.SharedApplication.KeyWindow?.EndEditing(true);
#elif WINDOWS
            var nativeView = entry.Handler?.PlatformView;
            if (nativeView is Microsoft.UI.Xaml.Controls.Control control)
            {
                control.Focus(Microsoft.UI.Xaml.FocusState.Unfocused); // Tira o foco do Entry no Windows
            }
#endif
    }
}
