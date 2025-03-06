using AppGarcomSys.Context;
using AppGarcomSys.Models;
using Microsoft.EntityFrameworkCore;

namespace AppGarcomSys.Views.Modais;

public partial class ModalDePedidoEnviado : ContentPage
{
    public int IdDoPedido { get; set; }
    public int TempoDeEspera { get; set; } = 10;
    public ModalDePedidoEnviado(int idDoPedido)
    {
        IdDoPedido = idDoPedido;

        InitializeComponent();
    }

    private async Task GeraLogicaDeStatusDePedido()
    {
        try
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapDePedidoEnviado_Tapped;

            

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

                if (contador > TempoDeEspera)
                {
                    condicao = false;
                }
            }


            if (pedidoLido)
            {
                CarregandoIndicador.IsRunning = false;
                LayoutDePedidoConcluido.IsVisible = true;
                AppState.SetaInfosDeBalcao();
                AppState.SetaInfosDePedidoRepitido();
                AppState.ProdutosCarrinho!.Clear();
                MajorLayout.GestureRecognizers.Add(tapGesture);
            }
            else
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var PedidoNaoEnviado = db.apoioappgarcom.FirstOrDefault(x => x.Id == IdDoPedido);

                    string? MotiVoDoErro = PedidoNaoEnviado!.Obs;

                    if(MotiVoDoErro is not null)
                    {
                        Label label = new Label
                        {
                            Text = $"Motivo - {MotiVoDoErro}",
                            FontSize = 20,
                            TextColor = Color.Parse("#FF4242"),
                            FontFamily = "OpenSansSemibold",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Margin = new Thickness(10, 0, 0, 0)
                        };

                        LayoutDePedidoNaoEnviado.Children.Add(label);
                    }
                    else
                    {
                        PedidoNaoEnviado.Obs = "Não foi possivel inserir o pedido, confira se o integrador esta aberto!";
                        db.SaveChanges();

                        Label label = new Label
                        {
                            Text = $"Motivo - {PedidoNaoEnviado.Obs}",
                            FontSize = 20,
                            TextColor = Color.Parse("#FF4242"),
                            FontFamily = "OpenSansSemibold",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Margin = new Thickness(10, 0, 0, 0)

                        };

                        LayoutDePedidoNaoEnviado.Children.Add(label);
                    }
                }

                CarregandoIndicador.IsRunning = false;
                LayoutDePedidoNaoEnviado.IsVisible = true;

                MajorLayout.GestureRecognizers.Add(tapGesture);
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
        await DefineTempoDeEspera();    
        await GeraLogicaDeStatusDePedido();


        base.OnAppearing();
    }

    private async Task DefineTempoDeEspera()
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                var ConfigApp = await db.configappgarcom.FirstOrDefaultAsync();

                if(ConfigApp is not null)
                {
                    TempoDeEspera = ConfigApp.TempoEnvioPedido;
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    private async void TapDePedidoEnviado_Tapped(object sender, TappedEventArgs e)
    {
        await Task.Delay(10);

        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;

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