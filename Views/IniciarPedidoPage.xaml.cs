using AppGarcomSys.Models;
using System.ComponentModel;
using System.Linq;

namespace AppGarcomSys.Views;

public partial class IniciarPedidoPage : ContentPage
{
    public IniciarPedidoPage()
    {
        InitializeComponent();
        var swipeLeftGesture = new SwipeGestureRecognizer
        {
            Direction = SwipeDirection.Left  
        };
        swipeLeftGesture.Swiped += OnSwipeLeft;

    }

    protected async override void OnAppearing()
    {
        try
        {
            AppState.CarregarContas();

            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;
            List<Mesa> mesasOuComandas = new List<Mesa>();


            if (AppState.configuracaoDoApp.Comanda)
            {
                var ocupadas = AppState.ContasNaMemoria!.Where(x => x.Status != "P").ToList();

                foreach (var item in ocupadas)
                {
                    if (!mesasOuComandas.Any(x => x.Codigo == item.Mesa))
                        mesasOuComandas.Add(AppState.MesasNaMemoria!.FirstOrDefault(x => x.Codigo == item.Mesa)!);

                    mesasOuComandas.OrderBy(x => int.Parse(x.Codigo!));
                }

                var frameComanda = new Frame
                {
                    Content = new Label { Text = "Novo", HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") },
                    BackgroundColor = Color.Parse("#3B8112"),
                    BorderColor = Colors.Black
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    AppState.NumeroDaComanda = "000000";

                    if (!AppState.configuracaoDoApp.ListaPorGrupo)
                        await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
                    else
                        await Navigation.PushAsync(new NavigationPage(new GruposPage()));
                };
                frameComanda.GestureRecognizers.Add(tapGestureRecognizer);

                Grid.SetColumn(frameComanda, ContagemColuna);
                Grid.SetRow(frameComanda, ContagemDeLinhaReal);

                GridDeMesas.Children.Add(frameComanda);

                ContagemColuna++;
            }
            else
            {
                mesasOuComandas = AppState.MesasNaMemoria!;
            }


            foreach (var mesa in mesasOuComandas!)
            {
                if (ContagemColuna > 2)
                    ContagemColuna = 0;

                var frame = new Frame();
                var LblNumeroDeMesa = new Label { Text = mesa.Codigo, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") };
               

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
                

                var MesaAtualOcupada = AppState.ContasNaMemoria!.Any(x => x.Mesa == mesa.Codigo );

                if (MesaAtualOcupada)
                {
                    var MesaNoContas = AppState.ContasNaMemoria!.FirstOrDefault(x => x.Mesa == mesa.Codigo && x.Status != "P");

                    if (MesaNoContas is not null)
                    {
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
                    string MesaOuComanda = "Comanda";

                    if (AppState.configuracaoDoApp.Mesa)
                        MesaOuComanda = "Mesa";

                    if (frame.BackgroundColor == Color.Parse("Red"))
                    {
                        var action = await DisplayActionSheet($"{MesaOuComanda} Ocupada", "Cancelar", null, $"Seguir com outro pedido para essa {MesaOuComanda}", "Ver pedidos");

                        switch (action)
                        {
                            case "Seguir com outro pedido para essa Mesa":
                                // Ação para desbloquear a mesa
                                AppState.NumeroDaMesa = int.Parse(mesa.Codigo!);

                                if (!AppState.configuracaoDoApp.ListaPorGrupo)
                                    await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
                                else
                                    await Navigation.PushAsync(new NavigationPage(new GruposPage()));
                                break;
                            case "Seguir com outro pedido para essa Comanda":
                                // Ação para desbloquear a mesa
                                AppState.NumeroDaComanda = mesa.Cartao;

                                if (!AppState.configuracaoDoApp.ListaPorGrupo)
                                    await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
                                else
                                    await Navigation.PushAsync(new NavigationPage(new GruposPage()));
                                break;
                            case "Ver pedidos":
                                // Ação para ver os pedidos da mesa
                                ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new PageVerPedidos());
                                ((FlyoutPage)App.Current.MainPage).IsPresented = false;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (frame.BackgroundColor == Color.Parse("Yellow"))
                    {
                        await DisplayAlert($"{MesaOuComanda} Fechada", $"A {MesaOuComanda} já esta fechada e esperando pagamento, não é possivel lançar um pedido nela no momento", "OK");
                    }
                    else
                    {
                        if (AppState.configuracaoDoApp.Mesa)
                            AppState.NumeroDaMesa = int.Parse(mesa.Codigo!);
                        else
                            AppState.NumeroDaComanda = mesa.Codigo;

                        if (!AppState.configuracaoDoApp.ListaPorGrupo)
                            await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
                        else
                            await Navigation.PushAsync(new NavigationPage(new GruposPage()));
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

    private async void OnSwipeLeft(object sender, SwipedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
    }
}