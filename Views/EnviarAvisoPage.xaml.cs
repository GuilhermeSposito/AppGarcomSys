using AppGarcomSys.Context;
using AppGarcomSys.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AppGarcomSys.Views;

public partial class EnviarAvisoPage : ContentPage
{
    private int ApoioFrameDeAvisoDiverso { get; set; } = 0;
    private int ApoioFrameDeFecharMesa { get; set; } = 0;
    ActivityIndicator ActivityIndicator1 = new ActivityIndicator { IsRunning = true, IsVisible = true };

    public EnviarAvisoPage()
    {
        InitializeComponent();
    }

    private void BtnDown_Clicked(object sender, EventArgs e)
    {
        if (ApoioFrameDeAvisoDiverso == 0)
        {
            EntryDeAviso.IsVisible = true;
            BtnEnviarAvisoDiverso.IsVisible = true;
            BtnDown.Source = "fechar.png";

            BtnDown.WidthRequest = 70;
            BtnDown.HeightRequest = 70;

            ApoioFrameDeAvisoDiverso = 1;
        }
        else
        {
            EntryDeAviso.IsVisible = false;
            BtnEnviarAvisoDiverso.IsVisible = false;
            BtnDown.Source = "down.png";

            BtnDown.WidthRequest = 100;
            BtnDown.HeightRequest = 100;

            ApoioFrameDeAvisoDiverso = 0;
        }
    }

    private async void BtnDownFecharMesa_Clicked(object sender, EventArgs e)
    {
        if (ApoioFrameDeFecharMesa == 0)
        {
            LayoutDeMesasAbertas.Add(ActivityIndicator1);
            LayoutDeMesasAbertas.IsVisible = true;

            await CarregaMesasOcupadasNaTela();
            BtnDownFecharMesa.Source = "fechar.png";

            BtnDownFecharMesa.WidthRequest = 70;
            BtnDownFecharMesa.HeightRequest = 70;

 
            ApoioFrameDeFecharMesa = 1;
        }
        else
        {

            BtnDownFecharMesa.Source = "down.png";
            LayoutDeMesasAbertas.Remove(ActivityIndicator1);
            ActivityIndicator1.IsRunning = true;
            ActivityIndicator1.IsVisible = true;
            LayoutDeMesasAbertas.IsVisible = false;
            GridDeMesas.Children.Clear();

            BtnDownFecharMesa.WidthRequest = 100;
            BtnDownFecharMesa.HeightRequest = 100;

            ApoioFrameDeFecharMesa = 0;
        }
    }

    private async Task CarregaMesasOcupadasNaTela()
    {
        try
        {
            await AppState.CarregarContas();
            await Task.Delay(2000);

            ActivityIndicator1.IsRunning = false;
            ActivityIndicator1.IsVisible = false;

            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;
            List<Mesa> mesasOuComandas = new List<Mesa>();

            List<Contas> ocupadas = AppState.ContasNaMemoria!.Where(x => x.Status != "P" && x.Status != "F").OrderBy(x => int.TryParse(x.Mesa!, out int result)).ToList();

            foreach (var item in ocupadas)
            {
                if (!mesasOuComandas.Any(x => x.Codigo == item.Mesa!))
                    mesasOuComandas.Add(AppState.MesasNaMemoria!.FirstOrDefault(x => x.Codigo == item.Mesa)!);
            }

            foreach (var mesa in mesasOuComandas!)
            {
                if (ContagemColuna > 2)
                    ContagemColuna = 0;

                string? NumeroMesa = mesa.Codigo;


                var frame = new Frame();
                var LblNumeroDeMesa = new Label { Text = NumeroMesa, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") };


                if (AppState.configuracaoDoApp.Mesa)
                {
                    frame.Content = LblNumeroDeMesa;
                    frame.BackgroundColor = Color.Parse("#3B8112");
                    frame.BorderColor = Colors.Black;
                }
                else
                {
                    string NumeroComanda = Convert.ToInt32(mesa.Cartao).ToString();
                    LblNumeroDeMesa.Text = NumeroComanda;

                    frame.Content = LblNumeroDeMesa;
                    frame.BackgroundColor = Color.Parse("#3B8112");
                    frame.BorderColor = Colors.Black;
                }


                var MesaAtualOcupada = AppState.ContasNaMemoria!.Any(x => x.Mesa == mesa.Codigo && x.Status != "P");

                if (MesaAtualOcupada)
                {
                    var MesaNoContas = AppState.ContasNaMemoria!.FirstOrDefault(x => x.Mesa == mesa.Codigo && x.Status != "P");

                    if (MesaNoContas is not null)
                    {
                        if (MesaNoContas.Mesa!.Contains("B"))
                        {
                            if (!String.IsNullOrEmpty(MesaNoContas.Cliente!.Trim()))
                            {
                                LblNumeroDeMesa.Text += $" / {MesaNoContas.Cliente}";
                            }
                        }

                        if (MesaNoContas.Status == "F")
                        {
                            frame.BackgroundColor = Color.Parse("Yellow");
                            LblNumeroDeMesa.TextColor = Color.Parse("#000");
                        }

                        if (MesaNoContas.Status == "A")
                            frame.BackgroundColor = Color.Parse("Red");

                    }
                }

                var tapGestureRecognizer = new TapGestureRecognizer();


                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    var Resp = await DisplayActionSheet("Você tem certeza que deseja fechar essa mesa ?", "Cancelar", null, "Sim", "Não");

                    if (Resp == "Sim")
                    {
                        await EnviaAvisoDeFecharMesa(mesa.Codigo!);

                        await DisplayAlert("Aviso", "Aviso de fechamento enviado com sucesso", "Ok");

                        BtnDownFecharMesa_Clicked(new object(), new EventArgs());
                    }
                };

                frame.GestureRecognizers.Add(tapGestureRecognizer);

                Grid.SetColumn(frame, ContagemColuna);
                Grid.SetRow(frame, ContagemDeLinhaReal);

                GridDeMesas.Children.Add(frame);


                if (ContagemColuna == 2)
                {
                    GridDeMesas.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });

                    ContagemDeLinhaReal++;
                }

                ContagemColuna++;
            }

            base.OnAppearing();

        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }
    }

    private async Task EnviaAvisoDeFecharMesa(string numeroOuComanda)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                ClsSuporteDeFechamentoDeMesa clsSuporteDeFechamentoDeMesa = new ClsSuporteDeFechamentoDeMesa()
                {
                    NumeroMesaOuComanda = numeroOuComanda
                };

                Setup? setup = await db.setup.FirstOrDefaultAsync();
                if (setup is not null)
                {
                    if (setup.CouvertHoje)
                    {
                        string QtdPessoas = await DisplayPromptAsync("Couvert", "Informe a quantidade de pessoas", "Ok", null, "Quantidade de pessoaas", 100, keyboard: Keyboard.Numeric, "0");

                        clsSuporteDeFechamentoDeMesa.ValorCouvert = setup.CouvertValor * Convert.ToSingle(QtdPessoas);
                    }
                }

                string? Json = JsonSerializer.Serialize(clsSuporteDeFechamentoDeMesa);
                db.apoioappgarcom.Add(new ApoioAppGarcom()
                {
                    PedidoJson = Json,
                    Processado = false,
                    Tipo = "FECHAMENTO"
                });

                await db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }
    }

    private async void BtnEnviarAvisoDiverso_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.apoioappgarcom.Add(new ApoioAppGarcom()
                {
                    PedidoJson = $"Aviso de {AppState.GarconLogado!.Nome}:\n\n {EntryDeAviso.Text}",
                    Processado = false,
                    Tipo = "AVISO"
                });

                await db.SaveChangesAsync();

                EntryDeAviso.Text = string.Empty;
                BtnDown_Clicked(new object(), new EventArgs());
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }

    }
}