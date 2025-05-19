using AppGarcomSys.Models;
using AppGarcomSys.Views.Modais;

namespace AppGarcomSys.Views;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(100);

        string? ConfigDoIP = await SecureStorage.GetAsync("IP");
        if (String.IsNullOrEmpty(ConfigDoIP))
        {
            bool IpFoiInformado = true;

            while (IpFoiInformado)
            {
                string TextoDigitado = await DisplayPromptAsync("IP não encontrado", "IP não foi encontrado na configuração, por favor informe ele aqui. \n \tExemplo: 192.168.0.5 (Não se esqueça dos pontos)", "OK", "Cancelar");

                if (!String.IsNullOrEmpty(TextoDigitado))
                {
                    await SecureStorage.SetAsync("IP", TextoDigitado);
                    AppState.IpDoBanco = TextoDigitado;
                    IpFoiInformado = false;

                    await AppState.CarregarConfigs()!;
                    await AppState.CarregarGarcons();
                    await AppState.CarregarGrupos();
                    await AppState.CarregarProdutos();
                    await AppState.CarregarMesas();
                    await AppState.CarregarIncremento();
                    await AppState.CarregarIncrementosCardapio();
                    await AppState.CarregarContas();
                    await AppState.CarregaConfigsDeParametros();
                }
            }
           
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Exibir um diálogo de confirmação para o usuário
        Device.BeginInvokeOnMainThread(async () =>
        {
            bool confirmar = await DisplayAlert("Sair", "Tem certeza de que deseja sair do aplicativo?", "Sim", "Não");
            if (confirmar)
            {
                // Fecha o aplicativo no Android
#if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
                // iOS não permite fechamento programático
#endif
            }
        });

        // Impede o fechamento imediato
        return true;
    }

    private async void BtnDeEntrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            BtnDeEntrar.IsEnabled = false;

            bool LembrarSenha = false;//CheckBoxDeLembrarSenha.IsChecked;

            AppState.Autorizado = true;
            string? SenhaDigitada = txtSenha.Text;

            if (AppState.GarconsNaMemoria is null)
                throw new Exception("Nenhum garçom cadastrado");

            var Usuario = AppState.GarconsNaMemoria?.FirstOrDefault(x => x.Senha == SenhaDigitada);

            if (Usuario is not null)
            {
                AppState.GarconLogado = Usuario;

                if (LembrarSenha)
                    await SecureStorage.Default.SetAsync("SenhaALembrar", $"{SenhaDigitada}");

                await Task.Delay(30);

                await Navigation.PopModalAsync();
            }
            else
            {
                AppState.Autorizado = false;
                await DisplayAlert("Erro", "Usuario Não encontrado", "OK");
            }
        }
        catch (Exception ex) when (ex.Message.Contains("Value cannot be null"))
        {
            await DisplayAlert("Erro", "Nenhum banco de dados configurado", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
        finally
        {
            BtnDeEntrar.IsEnabled = true;
            AppState.HideKeyboard(txtSenha);
        }
    }




    private async void ClickOpcDesenvolvedor_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new SenhaDeConfgisModal(eTelaDeDesenvolvedor: true));
    }
}