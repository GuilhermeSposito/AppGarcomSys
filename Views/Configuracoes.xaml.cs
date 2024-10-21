using AppGarcomSys.Context;
using AppGarcomSys.Models;

namespace AppGarcomSys.Views;

public partial class Configuracoes : ContentPage
{
    public bool TelaOpcDesenvolvedor { get; set; } = false;


    public Configuracoes(bool eTelaDeDesenvolvedor)
    {
        TelaOpcDesenvolvedor = eTelaDeDesenvolvedor;

        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        try
        {

            if (TelaOpcDesenvolvedor)
            {
                FrameDeIP.IsVisible = true;
                frameFormaDeLancamento.IsVisible = false;
                frameDeRequisicao.IsVisible = false;
            }
            else
            {
                AtualizaFrameDeRadioButtonsDeRequisicao();
                AtualizaFrameDeRadioButtonsDeListagemDeProdutos();
            }

            var ConfigDoIP = await SecureStorage.Default.GetAsync("IP");

            if (ConfigDoIP is not null)
                txtCaminhoBanco.Text = ConfigDoIP;

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        base.OnAppearing();
    }

    private  void txtCaminhoBanco_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private async void BtnSalvarConfigs_Clicked(object sender, EventArgs e)
    {
        try
        {
            await SecureStorage.Default.SetAsync("IP", txtCaminhoBanco.Text);

            if (Navigation.ModalStack.Count > 0)
                await Navigation.PopModalAsync();
            else
            {
                ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
                ((FlyoutPage)App.Current.MainPage).IsPresented = false;
            }

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
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            AppState.HideKeyboard(txtCaminhoBanco);
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            string? selecionado = radioButton.Content.ToString();

            DefineEscolhaNoDbDeRequisicaoEFormaDeLancamento(selecionado);
        }
    }

    private void AtualizaFrameDeRadioButtonsDeRequisicao()
    {
        var configs = AppState.configuracaoDoApp;

        if(configs.RequisicaoAlfaNumerica)
            radioReqAlfaNumerica.IsChecked = true;

        if(configs.RequisicaoNumerica)
            radioReqNumerica.IsChecked = true;

        if (configs.Comanda)
            radioComanda.IsChecked = true;

        if(configs.Mesa)
            radioMesa.IsChecked = true;

        if (configs.SemRequisicao)
            radioSemReq.IsChecked = true;

    }

    private void AtualizaFrameDeRadioButtonsDeListagemDeProdutos()
    {
        var configs = AppState.configuracaoDoApp;

        if(configs.ListaDeItens)
            radioListaDeItens.IsChecked = true;

        if(configs.ListaPorGrupo)
            radioListaDeGrupos.IsChecked = true;

        if(configs.BuscaDeItens)
            radioBuscaDeItens.IsChecked = true;

    }


    private void DefineEscolhaNoDbDeRequisicaoEFormaDeLancamento(string? opcEscolhinda)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                switch (opcEscolhinda)
                {
                    case "Requisição alfanumérica":
                        db.configappgarcom.FirstOrDefault()!.RequisicaoAlfaNumerica = true;
                        db.configappgarcom.FirstOrDefault()!.RequisicaoNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.Comanda = false;
                        db.configappgarcom.FirstOrDefault()!.Mesa = false;
                        db.configappgarcom.FirstOrDefault()!.SemRequisicao = false;
                        break;
                    case "Requisição numérica":
                        db.configappgarcom.FirstOrDefault()!.RequisicaoAlfaNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.RequisicaoNumerica = true;
                        db.configappgarcom.FirstOrDefault()!.Comanda = false;
                        db.configappgarcom.FirstOrDefault()!.Mesa = false;
                        db.configappgarcom.FirstOrDefault()!.SemRequisicao = false;
                        break;
                    case "Comanda":
                        db.configappgarcom.FirstOrDefault()!.RequisicaoAlfaNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.RequisicaoNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.Comanda = true;
                        db.configappgarcom.FirstOrDefault()!.Mesa = false;
                        db.configappgarcom.FirstOrDefault()!.SemRequisicao = false;
                        break;
                    case "Mesas":
                        db.configappgarcom.FirstOrDefault()!.RequisicaoAlfaNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.RequisicaoNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.Comanda = false;
                        db.configappgarcom.FirstOrDefault()!.Mesa = true;
                        db.configappgarcom.FirstOrDefault()!.SemRequisicao = false;
                        break;
                    case "Sem Requisição":
                        db.configappgarcom.FirstOrDefault()!.RequisicaoAlfaNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.RequisicaoNumerica = false;
                        db.configappgarcom.FirstOrDefault()!.Comanda = false;
                        db.configappgarcom.FirstOrDefault()!.Mesa = false;
                        db.configappgarcom.FirstOrDefault()!.SemRequisicao = true;
                        break;
                    case "Lista de itens":
                        db.configappgarcom.FirstOrDefault()!.ListaDeItens = true;
                        db.configappgarcom.FirstOrDefault()!.ListaPorGrupo = false;
                        db.configappgarcom.FirstOrDefault()!.BuscaDeItens = false;
                        break;
                    case "Busca De itens":
                        db.configappgarcom.FirstOrDefault()!.ListaDeItens = false;
                        db.configappgarcom.FirstOrDefault()!.BuscaDeItens = true;
                        db.configappgarcom.FirstOrDefault()!.ListaPorGrupo = false;
                        break;
                    case "Lista de grupos":
                        db.configappgarcom.FirstOrDefault()!.ListaDeItens = false;
                        db.configappgarcom.FirstOrDefault()!.BuscaDeItens = false;
                        db.configappgarcom.FirstOrDefault()!.ListaPorGrupo = true;
                        break;
                }

                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}