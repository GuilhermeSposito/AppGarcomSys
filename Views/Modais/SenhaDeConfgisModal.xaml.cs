using AppGarcomSys.Models;

namespace AppGarcomSys.Views.Modais;

public partial class SenhaDeConfgisModal : ContentPage
{
    public string SenhaDigitada { get; private set; }
    public string? SenhaDaTelaDeDesenvolvedor { get; private set; } = "Sys@Logica";
    public bool TelaDesenvolvedor { get; set; }
    public bool Autorizado { get; set; } = false;

    public SenhaDeConfgisModal(bool eTelaDeDesenvolvedor)
    {
        TelaDesenvolvedor = eTelaDeDesenvolvedor;

        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        if (Autorizado && TelaDesenvolvedor)
        {
            await Navigation.PopModalAsync();
        }
        else if (Autorizado && !TelaDesenvolvedor)
        {
            ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
            ((FlyoutPage)App.Current.MainPage).IsPresented = false;
        }

        base.OnAppearing();
    }

    private async void OK_Clicked(object sender, EventArgs e)
    {
        try
        {

            SenhaDigitada = SenhaEntry.Text;

            if (SenhaDigitada != SenhaDaTelaDeDesenvolvedor)
            {
                await DisplayAlert("Erro", "Senha incorreta!", "OK");
                await Navigation.PopAsync(); // Volta se a senha for errada
            }
            else
            {
                if (TelaDesenvolvedor)
                {
                    await Navigation.PushModalAsync(new Configuracoes(eTelaDeDesenvolvedor: true));
                    Autorizado = true;
                }
                else
                {
                    await Navigation.PushModalAsync(new Configuracoes(eTelaDeDesenvolvedor: false));
                    Autorizado = true;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            AppState.HideKeyboard(SenhaEntry);
        }
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        SenhaDigitada = null;

        if (Navigation.ModalStack.Count > 0)
            await Navigation.PopModalAsync();
        else
        {
            ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
            ((FlyoutPage)App.Current.MainPage).IsPresented = false;
        }
    }
}