using AppGarcomSys.Repositorys;
using AppGarcomSys.Context;
using Microsoft.EntityFrameworkCore;
using AppGarcomSys.Models;
using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.Json;
using AppGarcomSys.Views.Modais;
using static System.Net.Mime.MediaTypeNames;

namespace AppGarcomSys.Views;

public partial class Carrinho : ContentPage
{
    public event PropertyChangedEventHandler PropertyChanged;
    private Entry entryDeMesa = new Entry() { ReturnType = ReturnType.Done, MaxLength = 4, TextColor = Color.Parse("#fff"), HorizontalTextAlignment = TextAlignment.Center, Keyboard = Keyboard.Numeric, ClearButtonVisibility = ClearButtonVisibility.WhileEditing };
    private Entry entryDeComanda = new Entry() { IsVisible = false, ReturnType = ReturnType.Done, MaxLength = 6, TextColor = Color.Parse("#fff"), HorizontalTextAlignment = TextAlignment.Center, ClearButtonVisibility = ClearButtonVisibility.WhileEditing };

    private Label lblMesa = new Label() { Text = "Mesa", TextColor = Color.Parse("#fff"), HorizontalTextAlignment = TextAlignment.Center };
    private Label lblComanda = new Label() { Text = "Comanda", TextColor = Color.Parse("#fff"), HorizontalTextAlignment = TextAlignment.Center };
    public Carrinho()
    {
        try
        {
            InitializeComponent();

            var swipeLeftGesture = new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Right  // Definir a direção para a esquerda
            };

            // Associar o evento ao gesto
            swipeLeftGesture.Swiped += OnSwipedRigth!;

            // Adicionar o gesto ao layout da página
            container.GestureRecognizers.Add(swipeLeftGesture);
            FrameDePedidoAberto.GestureRecognizers.Add(swipeLeftGesture);
            FrameDeProdutosNoCarrinho.GestureRecognizers.Add(swipeLeftGesture);

            entryDeMesa.TextChanged += OnTextChenged!;
            entryDeComanda.TextChanged += OnTextChenged!;

        }
        catch (Exception ex)
        {

            DisplayAlert("Error", ex.ToString(), "Cancelar");
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

    private void OnTextChenged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry; // Identifica qual Entry disparou o evento.

        if (entry == entryDeComanda && entry.IsVisible)
        {
            AppState.NumeroDaComanda = entry.Text.PadLeft(6, '0');
        }
        else if (entry == entryDeMesa && entry.IsVisible)
        {
            AppState.NumeroDaMesa = int.Parse(entry.Text);
        }
    }

    protected async override void OnAppearing()
    {
        try
        {
            if (AppState.configuracaoDoApp.Comanda)
                entryDeComanda.IsVisible = true;


            if (!entryDeComanda.IsVisible)
            {
                lblComanda.IsVisible = false;
                Grid.SetColumnSpan(lblMesa, 2);
                Grid.SetColumnSpan(entryDeMesa, 2);
            }
            else
            {
                lblMesa.IsVisible = false;
                entryDeMesa.IsVisible = false;
                Grid.SetColumnSpan(entryDeComanda, 2);
                Grid.SetColumnSpan(lblComanda, 2);
            }

            Grid.SetRow(entryDeMesa, 0);
            Grid.SetColumn(entryDeMesa, 0);
            Grid.SetColumn(lblMesa, 0);
            Grid.SetRow(lblMesa, 1);
            if (!GridDeMesaEComanda.Children.Contains(entryDeMesa))
            {
                GridDeMesaEComanda.Children.Add(entryDeMesa);
                GridDeMesaEComanda.Children.Add(lblMesa);
            }

            Grid.SetRow(entryDeComanda, 0);
            Grid.SetColumn(entryDeComanda, 0);
            Grid.SetColumn(lblComanda, 0);
            Grid.SetRow(lblComanda, 1);
            if (!GridDeMesaEComanda.Children.Contains(entryDeComanda))
            {
                GridDeMesaEComanda.Children.Add(entryDeComanda);
                GridDeMesaEComanda.Children.Add(lblComanda);
            }

            await AppState.CarregarConfigs()!;

            if (!AppState.Autorizado)
            {
                await Navigation.PushModalAsync(new Login());

                await AppState.CarregarGarcons();
                await AppState.CarregarGrupos();
                await AppState.CarregarProdutos();
                await AppState.CarregaConfigsDeParametros();
                await AppState.CarregarMesas();
                await AppState.CarregarIncremento();
                await AppState.CarregarIncrementosCardapio();
                await AppState.CarregarContas();
            }
            else
            {

                if (AppState.GarconLogado is not null)
                {
                    LblNomeGarcomPedidoAberto.Text = AppState.GarconLogado.Nome;
                    LblNomeGarcom.Text = AppState.GarconLogado.Nome;
                }

                AppState.CarregarContas(); //Não desejamos esperar para não atrapalhar a execução do programa

                if (!AppState.ProdutosCarrinho!.Any())
                {
                    FrameDePedidoAberto.IsVisible = false;
                    FrameDePedidoAindaNAberto.IsVisible = true;
                    borderDeNomeDoGarcomFixo.IsVisible = false;
                    await Task.Delay(100);
                }
                else
                {
                    if (entryDeComanda.IsVisible)
                    {
                        entryDeComanda.Text = AppState.NumeroDaComanda!.PadLeft(6, '0');
                    }
                    else
                    {
                        entryDeMesa.Text = AppState.NumeroDaMesa.ToString().PadLeft(4, '0');
                    }

                    FrameDePedidoAberto.IsVisible = true;
                    FrameDePedidoAindaNAberto.IsVisible = false;
                    FrameDeProdutosNoCarrinho.IsVisible = true;

                    LayoutDeProdutosNoCarrinho.Clear();
                    CriaFramesDeProdutos(AppState.ProdutosCarrinho, LayoutDeProdutosNoCarrinho);

                    await Task.Delay(10);

                }
            }
            this.ForceLayout();
            base.OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");
            AppState.HabilitarOpcDesenvolvedor = true;
        }

    }

    private async void OnSwipedRigth(object sender, SwipedEventArgs e)
    {
        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new IniciarPedidoPage());
    }

    private async void BtnAdicionarProduto_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!AppState.configuracaoDoApp.ListaPorGrupo)
                await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
            else
                await Navigation.PushAsync(new NavigationPage(new GruposPage()));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");
        }

    }

    public List<Frame> CriaFramesDeProdutos(List<Produto>? produtos, VerticalStackLayout LayoutDeProdutosNoCarrinho)
    {
        List<Frame> frames = new List<Frame>();

        var swipeLeftGesture = new SwipeGestureRecognizer
        {
            Direction = SwipeDirection.Right  // Definir a direção para a esquerda
        };

        // Associar o evento ao gesto
        swipeLeftGesture.Swiped += OnSwipedRigth!;

        foreach (var produto in produtos)
        {
            Frame FrameDeContainer = new Frame
            {
                BackgroundColor = Color.Parse("#7A7A7A"),
                CornerRadius = 10,
                Margin = new Thickness(0, 5, 0, 0),
                HasShadow = true
            };

            Grid gridContainer = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) }
                },

                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(70, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(70, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Auto) }
                }

            };

            if (!String.IsNullOrEmpty(produto.Requisicao))
            {
                Label LbLRequsicaoProduto = new Label { Text = $"Requisição: {produto.Requisicao}", HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };

                Grid.SetColumn(LbLRequsicaoProduto, 0);
                Grid.SetRow(LbLRequsicaoProduto, 3);

                gridContainer.Children.Add(LbLRequsicaoProduto);
            }

            FrameDeContainer.Content = gridContainer;

            Frame FrameDeNomeDoProduto = new Frame
            {
                BackgroundColor = Color.Parse("#027373")
            };

            VerticalStackLayout LayoutDeNomeDoProduto = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };


            if (produto.Descricao!.Length > 25)
            {
                var NomeDoProduto = String.Empty;


                Label LblDeNomeDoProduto = new Label { Text = produto.Descricao, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                LayoutDeNomeDoProduto.Children.Add(LblDeNomeDoProduto);

                for (int i = 2; i < produto.Descricao.Length; i++)
                {
                    var currentHeight = gridContainer.RowDefinitions[0].Height.Value;
                    gridContainer.RowDefinitions[0].Height = new GridLength(currentHeight + 2, GridUnitType.Absolute);
                    if (produto.Descricao.Contains("1/3"))
                    {
                        if (produto.Descricao.Length <= 32)
                            FrameDeNomeDoProduto.HeightRequest += 3.5;
                        else
                            FrameDeNomeDoProduto.HeightRequest += 2;
                    }
                    else if (produto.Descricao.Contains("1/2"))
                    {
                        if (produto.Descricao.Length <= 32)
                            FrameDeNomeDoProduto.HeightRequest += 3.5;
                        else
                            FrameDeNomeDoProduto.HeightRequest += 2.5;
                    }
                    else
                        FrameDeNomeDoProduto.HeightRequest += 3;
                }

            }
            else
            {
                Label LblDeNomeDoProduto = new Label { Text = produto.Descricao, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                LayoutDeNomeDoProduto.Children.Add(LblDeNomeDoProduto);
            }


            FrameDeNomeDoProduto.Content = LayoutDeNomeDoProduto;
            Grid.SetColumn(FrameDeNomeDoProduto, 0);
            Grid.SetRow(FrameDeNomeDoProduto, 0);
            gridContainer.Children.Add(FrameDeNomeDoProduto);

            Grid GridDeOpcoesDoProduto = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) }
                 },

                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Star) }
                },
                Margin = new Thickness(0, 10, 0, 0)
            };

            //Input para quantidade do produto
            //-----------------------------------------------------------------------------------
            Entry InputDeQtdProduto = new Entry { Placeholder = produto.Quantidade.ToString(), PlaceholderColor = Color.Parse("Black"), Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
            VerticalStackLayout LayoutDeQtdDoProduto = new VerticalStackLayout
            {
                Children =
                {
                    InputDeQtdProduto,
                    new Label { HorizontalTextAlignment = TextAlignment.Center ,Text = "Quantidade", TextColor = Color.Parse("#000") }
                }
            };

            InputDeQtdProduto.Completed += (s, e) =>
            {
                if (int.TryParse(InputDeQtdProduto.Text, out int qtd))
                {
                    produto.Quantidade = qtd;
                }

                InputDeQtdProduto.Text = "";
                InputDeQtdProduto.Placeholder = produto.Quantidade.ToString();

            };

            Grid.SetColumn(LayoutDeQtdDoProduto, 0);
            GridDeOpcoesDoProduto.Children.Add(LayoutDeQtdDoProduto);
            //-----------------------------------------------------------------------------------


            //Botão de incrementos e decrementos
            //-----------------------------------------------------------------------------------
            var BtnMais = new ImageButton { Source = "mais.png", WidthRequest = 40, HeightRequest = 40 };
            var BtnMenos = new ImageButton { Source = "botaodemenos.png", WidthRequest = 40, HeightRequest = 40 };
            HorizontalStackLayout LayoutDeButtonsIncrementos = new HorizontalStackLayout
            {
                Children =
                {
                 BtnMais,
                 BtnMenos
                },
                Margin = new Thickness(15, 0, 0, 0),
                Spacing = 5
            };

            BtnMais.Clicked += async (s, e) =>
            {
                BtnMais.IsEnabled = false;
                BtnMenos.IsEnabled = false;
                await Navigation.PushAsync(new NavigationPage(new ModalDeIncrementos(produto.Codigo)));

            };

            BtnMenos.Clicked += async (s, e) =>
            {
                BtnMenos.IsEnabled = false;
                BtnMais.IsEnabled = false;
                await Navigation.PushAsync(new NavigationPage(new ModalDeDecremento(produto.Codigo)));
            };

            Grid.SetColumn(LayoutDeButtonsIncrementos, 1);
            GridDeOpcoesDoProduto.Children.Add(LayoutDeButtonsIncrementos);
            //-----------------------------------------------------------------------------------


            //Botão para adicionar observação ao produto
            //-----------------------------------------------------------------------------------
            var bTnObs = new ImageButton { Source = "lapis.png", WidthRequest = 40, HeightRequest = 40 };
            VerticalStackLayout LayoutDeButtonObs = new VerticalStackLayout
            {
                Children =
                {
                    bTnObs
                }
            };
            Label LbDeObsDoProduto = new Label() { HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
            VerticalStackLayout LayoutDeLabelDeObs = new VerticalStackLayout
            {
                Children =
                        {
                           LbDeObsDoProduto
                        },
                Margin = new Thickness(0, 10, 0, 0)
            };
            Grid.SetRow(LayoutDeLabelDeObs, 2);
            gridContainer.Children.Add(LayoutDeLabelDeObs);

            bTnObs.Clicked += async (s, e) =>
            {
                string resultado = await DisplayPromptAsync("Observação do produto", null, maxLength: 80, keyboard: Keyboard.Text, initialValue: produto.Observacao);

                produto.Observacao = resultado;

                if (!String.IsNullOrEmpty(produto.Observacao))
                {
                    LbDeObsDoProduto.Text = $"Obs: {produto.Observacao}";

                }

            };
            if (!String.IsNullOrEmpty(produto.Observacao))
            {
                LbDeObsDoProduto.Text = $"Obs: {produto.Observacao}";


            }

            Grid.SetColumn(LayoutDeButtonObs, 2);
            GridDeOpcoesDoProduto.Children.Add(LayoutDeButtonObs);
            //-----------------------------------------------------------------------------------



            //Botão para excluir o produto do carrinho
            //-----------------------------------------------------------------------------------
            ImageButton BtnDeExcluirProduto = new ImageButton { Source = "lixo.png", WidthRequest = 40, HeightRequest = 40 };
            VerticalStackLayout LayoutDeButtonExcluiItem = new VerticalStackLayout
            {
                Children =
                {
                    BtnDeExcluirProduto
                }
            };
            BtnDeExcluirProduto.Clicked += async (s, e) =>
            {
                var action = await DisplayActionSheet("Você deseja mesmo excluir esse produto do carrinho?", "Cancelar", null, "Sim", "Não");

                if (action == "Sim")
                {
                    LayoutDeProdutosNoCarrinho.Children.Remove(FrameDeContainer);
                    AppState.ProdutosCarrinho!.Remove(produto);
                }
            };
            //-----------------------------------------------------------------------------------


            Grid.SetColumn(LayoutDeButtonExcluiItem, 3);
            GridDeOpcoesDoProduto.Children.Add(LayoutDeButtonExcluiItem);

            Grid.SetColumn(GridDeOpcoesDoProduto, 0);
            Grid.SetRow(GridDeOpcoesDoProduto, 1);
            gridContainer.Children.Add(GridDeOpcoesDoProduto);

            if (produto.incrementos.Count > 0)
            {
                int linhaInicial = 4;
                int linhaAtual = linhaInicial;

                foreach (var incremento in produto.incrementos)
                {
                    gridContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });

                    Frame frameDeIncrementos = new Frame
                    {
                        BackgroundColor = Color.Parse("#027373"),
                        CornerRadius = 10,
                        Margin = new Thickness(0, 5, 0, 0),
                        HasShadow = true
                    };

                    Label LblDeIncremento = new Label { Text = incremento.Descricao, FontSize = 15, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                    Label LblDeIncrementoQtd = new Label { Text = incremento.Quantidade.ToString(), FontSize = 15, HorizontalTextAlignment = TextAlignment.End, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                    HorizontalStackLayout LayoutDeIncrementos = new HorizontalStackLayout
                    {
                        Children =
                        {
                            LblDeIncremento,
                            LblDeIncrementoQtd
                        },
                        Spacing = 150
                    };

                    frameDeIncrementos.Content = LayoutDeIncrementos;

                    Grid.SetRow(frameDeIncrementos, linhaAtual);
                    gridContainer.Children.Add(frameDeIncrementos);

                    linhaAtual++;
                }
            }


            LayoutDeProdutosNoCarrinho.Add(FrameDeContainer);

            frames.Add(FrameDeContainer);
        }

        return frames;
    }



    private void BtnMais_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void InputDeQtdProduto_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void BtnAbrirPedido_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new IniciarPedidoPage()));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void BtnCancelarPedido_Clicked(object sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Você deseja mesmo cancelar esse pedido?", "Cancelar", null, "Sim", "Não");

        if (action == "Sim")
        {
            AppState.ProdutosCarrinho!.Clear();


            ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
        }

    }

    private async void BtnFecharPedido_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (AppState.configuracaoDoApp.Mesa)
                {
                    if (AppState.NumeroDaMesa == 0)
                    {
                        throw new Exception("Informe o número da mesa");
                    }
                }
                else
                {
                    if (AppState.NumeroDaComanda is null || AppState.NumeroDaComanda == "000000")
                    {
                        throw new Exception("Informe o número da comanda");
                    }

                }

                if (AppState.ProdutosCarrinho!.Count == 0)
                {
                    throw new Exception("Adicione produtos ao carrinho para poder enviar o pedido!");
                }

                var Pedido = new Pedido
                {
                    Mesa = AppState.NumeroDaMesa.ToString().PadLeft(4, '0'),   
                    Comanda = AppState.NumeroDaComanda!.PadLeft(6, '0'),
                    GarcomResponsavel = AppState.GarconLogado!.Codigo,
                    produtos = AppState.ProdutosCarrinho!
                };

                string? PedidoJson = JsonSerializer.Serialize(Pedido);

                var NovoPedido = new ApoioAppGarcom()
                {
                    PedidoJson = PedidoJson,
                    Processado = false,
                    Tipo = "pedido"
                };

                //await DisplayAlert("Pedido", CarrinhoJson, "Ok");

                await db.apoioappgarcom.AddAsync(NovoPedido);
                await db.SaveChangesAsync();

                await Navigation.PushModalAsync(new NavigationPage(new ModalDePedidoEnviado(NovoPedido.Id)));


                ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Pedido", ex.Message, "Ok");

        }
    }

    private async void BtnVerPedidos_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new PageVerPedidos()));
    }
}