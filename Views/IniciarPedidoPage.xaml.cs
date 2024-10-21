using AppGarcomSys.Models;
using System.ComponentModel;

namespace AppGarcomSys.Views;

public partial class IniciarPedidoPage : ContentPage
{
    public IniciarPedidoPage()
    {
        InitializeComponent();
        var swipeLeftGesture = new SwipeGestureRecognizer
        {
            Direction = SwipeDirection.Left  // Definir a direção para a esquerda
        };

        // Associar o evento ao gesto
        swipeLeftGesture.Swiped += OnSwipeLeft;

        // Adicionar o gesto ao layout da página
        // container.GestureRecognizers.Add(swipeLeftGesture);
    }

    protected async override void OnAppearing()
    {
        try
        {
            AppState.CarregarContas();

            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;


            foreach (var mesa in AppState.MesasNaMemoria!)
            {
                if (ContagemColuna > 2)
                    ContagemColuna = 0;

                var frame = new Frame
                {
                    Content = new Label { Text = mesa.Codigo, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") },
                    BackgroundColor = Color.Parse("#024959"),
                    BorderColor = Colors.Black
                };

                var MesaAtualOcupada = AppState.ContasNaMemoria!.Any(x => x.Mesa == mesa.Codigo && x.Status != "P");

                if (MesaAtualOcupada)
                {
                    var MesaNoContas = AppState.ContasNaMemoria!.FirstOrDefault(x => x.Mesa == mesa.Codigo);

                    if (MesaNoContas is not null)
                    {
                        if (MesaNoContas.Status == "F")
                            frame.BackgroundColor = Color.Parse("Yellow");

                        if (MesaNoContas.Status == "A")
                            frame.BackgroundColor = Color.Parse("Red");

                    }
                }

                var tapGestureRecognizer = new TapGestureRecognizer();


                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    if (frame.BackgroundColor == Color.Parse("Red"))
                    {
                        var action = await DisplayActionSheet("Mesa Ocupada", "Cancelar", null, "Seguir com outro pedido para essa mesa", "Ver pedidos");

                        switch (action)
                        {
                            case "Seguir com outro pedido para essa mesa":
                                // Ação para desbloquear a mesa
                                AppState.NumeroDaMesa = int.Parse(mesa.Codigo!);

                                await Navigation.PushAsync(new NavigationPage(new GruposPage()));
                                break;
                            case "Ver pedidos":
                                // Ação para ver os pedidos da mesa
                                await DisplayAlert("Pedidos", "Paginda de ver pedidos ainda não foi implementada", "OK");
                                break;
                        }
                    }
                    else if (frame.BackgroundColor == Color.Parse("Yellow"))
                    {
                        await DisplayAlert("Mesa Fechada", "A mesa já esta fechada e esperando pagamento, não é possivel lançar um pedido nela no momento", "OK");
                    }
                    else
                    {
                        AppState.NumeroDaMesa = int.Parse(mesa.Codigo!);

                        if(!AppState.configuracaoDoApp.ListaPorGrupo)
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