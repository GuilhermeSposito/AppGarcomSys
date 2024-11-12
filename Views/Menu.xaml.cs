using AppGarcomSys.Models;
using AppGarcomSys.Views.Modais;

namespace AppGarcomSys.Views;

public partial class Menu : ContentPage
{
    public Menu()
    {
        InitializeComponent();
    }

    private void ClickIniciarPedido_Tapped(object sender, TappedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new IniciarPedidoPage());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;
    }

    private void ClickLblMainPage_Tapped(object sender, TappedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;
    }

    private async void BtnSair_Tapped(object sender, TappedEventArgs e)
    {
        AppState.Autorizado = false;
        AppState.GarconsNaMemoria = null;

        if (AppState.ProdutosCarrinho.Count > 0)
        {
            var DecUser = await DisplayAlert("Tem certeza?", "você tem produtos no carrinho, se decidir sair perdera eles", "Sim", "Não");

            if (DecUser)
            {
                AppState.ProdutosCarrinho.Clear();
                ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
                ((FlyoutPage)App.Current.MainPage).IsPresented = false;
            }
        }
        else
        {

            ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
            ((FlyoutPage)App.Current.MainPage).IsPresented = false;

        }
    }

    private async void ClickIrConfiguracoes_Tapped(object sender, TappedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new SenhaDeConfgisModal(false));
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;
    }

    private void ClickVerPedido_Tapped(object sender, TappedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new PageVerPedidos());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;
    }

    private void ClickEnviarAviso_Tapped(object sender, TappedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new EnviarAvisoPage());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;
    }
}