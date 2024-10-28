using AppGarcomSys.Context;
using AppGarcomSys.Models;

namespace AppGarcomSys.Views.Modais;

public partial class ModalDePedidoEnviado : ContentPage
{
    public int IdDoPedido { get; set; }
    public ModalDePedidoEnviado(int idDoPedido)
    {
        IdDoPedido = idDoPedido;

        InitializeComponent();
    }

    private async Task GeraLogicaDeStatusDePedido()
    {
        try
        {
            bool pedidoLido = false;
            int contador = 1;

            var condicao = true;

            while (condicao)
            {
                pedidoLido = await PedidoLido();

                if (pedidoLido)
                {
                    condicao = false;
                }
                else
                {
                    contador++;
                    await Task.Delay(1000);
                }

                if (contador > 10)
                {
                    condicao = false;
                }
            }


            if (pedidoLido)
            {
                CarregandoIndicador.IsRunning = false;
                LayoutDePedidoConcluido.IsVisible = true;
                AppState.ProdutosCarrinho!.Clear();
            }
            else
            {
                using (AppDbContext db = new AppDbContext())
                {
                    db.apoioappgarcom.Remove(db.apoioappgarcom.FirstOrDefault(x => x.Id == IdDoPedido)!);
                    await db.SaveChangesAsync();
                }

                CarregandoIndicador.IsRunning = false;
                LayoutDePedidoNaoEnviado.IsVisible = true;
            }


            base.OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");

        }
    }

    protected async override void OnAppearing()
    {
        await GeraLogicaDeStatusDePedido();


        base.OnAppearing();
    }

    private async void TapDePedidoEnviado_Tapped(object sender, TappedEventArgs e)
    {
        await Task.Delay(10);

        await Navigation.PopModalAsync();
    }

    private async Task<bool> PedidoLido()
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                ApoioAppGarcom pedido = db.apoioappgarcom.FirstOrDefault(x => x.Id == IdDoPedido)!;

                if (pedido != null)
                {
                    return pedido.Processado;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");
        }
        return false;
    }
}