using AppGarcomSys.Context;
using AppGarcomSys.Models;
using AppGarcomSys.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AppGarcomSys;

public partial class App : Application
{
    public App(AppDbContext dbContext)
    {
        InitializeComponent();
        MainPage = new FlyoutPageMenu();
    }

    protected override async void OnStart()
    {
        try
        {
            var CarrinhoJson = await SecureStorage.GetAsync("CARRINHO");

            if(CarrinhoJson is not null)
            {
                var produtos = JsonSerializer.Deserialize<List<Produto>>(CarrinhoJson);

                AppState.ProdutosCarrinho = produtos;

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected async override void OnSleep()
    {
        if (AppState.ProdutosCarrinho!.Count > 0)
        {
            var ProutosJson = JsonSerializer.Serialize(AppState.ProdutosCarrinho);

            await SecureStorage.Default.SetAsync("CARRINHO", ProutosJson);
        }
        else
        {
             SecureStorage.Default.Remove("CARRINHO");
        }


        base.OnSleep();
    }

}
