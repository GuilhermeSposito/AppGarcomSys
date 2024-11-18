using AppGarcomSys.Models;
using System.Text.Json;

namespace AppGarcomSys.Views.Modais;

public partial class ModalDeIncrementos : ContentPage
{
    public string? CodigoDoProduto { get; set; }
    public List<Incremento> IncrementosSelecionados { get; set; }

    public ModalDeIncrementos(string? CodProduto)
    {
        CodigoDoProduto = CodProduto;

        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        try
        {
            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;

            List<IncrementoCardapio> incrementoCardapioFiltrado = AppState.IncrementoCardapioNaMemoria!.Where(x => x.CodCardapio == CodigoDoProduto).ToList();
            List<Incremento> IncrementosCarregados = AppState.IncrementosNaMemoria!.ToList();
            List<Incremento> IncrementosUltilizados = new List<Incremento>();

            foreach (var incremento in IncrementosCarregados)
            {
                if (incrementoCardapioFiltrado.Any(x => x.Incremento == incremento.Codigo))
                {
                    if (!IncrementosUltilizados.Any(y => y.Codigo == incremento.Codigo) && incremento.Tipo == "A")
                        IncrementosUltilizados.Add(new Incremento
                        {
                            Codigo = incremento.Codigo,
                            Descricao = incremento.Descricao,
                            Valor = incremento.Valor,
                            Tipo = incremento.Tipo,
                            VendaInternet = incremento.VendaInternet,
                            Quantidade = incremento.Quantidade
                        });

                }
            }

            foreach (var incremento in IncrementosUltilizados)
            {
                if (ContagemColuna > 1)
                    ContagemColuna = 0;

                VerticalStackLayout LayoutContainerDeIncremento = new VerticalStackLayout();
                HorizontalStackLayout LayoutDeQuantidadeDeIncrementos = new HorizontalStackLayout();
                LayoutDeQuantidadeDeIncrementos.Margin = new Thickness(0, 10, 0, 0);
                LayoutDeQuantidadeDeIncrementos.HorizontalOptions = LayoutOptions.Center;

                var BtnMais = new ImageButton { Source = "mais.png", WidthRequest = 40, HeightRequest = 40 };
                Entry InputDeQtdProduto = new Entry { IsReadOnly = true, Placeholder = incremento.Quantidade.ToString(), PlaceholderColor = Color.Parse("White"), Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
                var BtnMenos = new ImageButton { Source = "botaodemenos.png", WidthRequest = 40, HeightRequest = 40 };

                Label ValorDoIncremento = new Label { Text = incremento.Valor.ToString("C"), HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") , Margin = new Thickness(0,5,0,0)};

                LayoutDeQuantidadeDeIncrementos.Add(BtnMais);
                LayoutDeQuantidadeDeIncrementos.Add(InputDeQtdProduto);
                LayoutDeQuantidadeDeIncrementos.Add(BtnMenos);
             

                BtnMais.Clicked += (s, e) =>
                {
                    InputDeQtdProduto.Placeholder = (int.Parse(InputDeQtdProduto.Placeholder) + 1).ToString();
                    incremento.Quantidade = int.Parse(InputDeQtdProduto.Placeholder);
                };

                BtnMenos.Clicked += (s, e) =>
                {
                    if (incremento.Quantidade >= 1)
                    {
                        InputDeQtdProduto.Placeholder = (int.Parse(InputDeQtdProduto.Placeholder) - 1).ToString();
                        incremento.Quantidade = int.Parse(InputDeQtdProduto.Placeholder);
                    }
                };

                var frame = new Frame
                {
                    Content = LayoutContainerDeIncremento,
                    BackgroundColor = Color.Parse("#024959"),
                    BorderColor = Colors.Black
                };

                LayoutContainerDeIncremento.Add(new Label { Text = incremento.Descricao, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") });
                LayoutContainerDeIncremento.Add(LayoutDeQuantidadeDeIncrementos);
                LayoutContainerDeIncremento.Add(ValorDoIncremento);

                Grid.SetColumn(frame, ContagemColuna);
                Grid.SetRow(frame, ContagemDeLinhaReal);

                GridDeProdutos.Children.Add(frame);


                if (ContagemColuna == 1)
                {
                    GridDeProdutos.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });

                    ContagemDeLinhaReal++;
                }

                ContagemColuna++;
            }

            BtnConcluido.Clicked += async (s, e) =>
            {
                List<Incremento> incrementosSelecionados = IncrementosUltilizados.Where(x => x.Quantidade > 0).ToList();

                if (AppState.ProdutosCarrinho!.Any(x => x.Codigo == CodigoDoProduto))
                {
                    var produtoExistente = AppState.ProdutosCarrinho!.FirstOrDefault(x => x.Codigo == CodigoDoProduto);

                    if (produtoExistente != null)
                    {
                        foreach (var incrementoAtual in incrementosSelecionados)
                        {
                            if (!produtoExistente.incrementos.Any(x => x.Codigo == incrementoAtual.Codigo))
                            {
                                produtoExistente.incrementos.Add(incrementoAtual);
                            }
                            else
                            {
                                produtoExistente.incrementos.FirstOrDefault(x => x.Codigo == incrementoAtual.Codigo).Quantidade += incrementoAtual.Quantidade;
                            }
                        }
                    }
                }
            };

            base.OnAppearing();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
        }

    }

    private async void BtnConcluido_Clicked(object sender, EventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;

        await Task.Delay(30);


        await Navigation.PopAsync();
    }

    protected async override void OnDisappearing()
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
        ((FlyoutPage)App.Current.MainPage).IsPresented = false;

        await Task.Delay(30);

        base.OnDisappearing();
    }
}