//using Android.Runtime;
using AppGarcomSys.Context;
using AppGarcomSys.Models;
using AppGarcomSys.Views.Modais;
using Microsoft.EntityFrameworkCore;


namespace AppGarcomSys.Views;

public partial class PageVerPedidos : ContentPage
{
    public string? NumeroDaMesaOuComanda { get; set; } = String.Empty;

    public PageVerPedidos()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        await CarregaMesasOcupadasNaTela();

        base.OnAppearing();
    }

    private async Task CarregaMesasOcupadasNaTela()
    {
        try
        {
            await AppState.CarregarContas();

            await Task.Delay(2000);

            CarregandoIndicador.IsRunning = false;
            CarregandoIndicador.IsVisible = false;

            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;
            List<Mesa> mesasOuComandas = new List<Mesa>();

            List<Contas> ocupadas = AppState.ContasNaMemoria!.Where(x => x.Status != "P").OrderBy(x => int.TryParse(x.Mesa!, out int result)).ToList();

            foreach (var item in ocupadas)
            {
                if (!mesasOuComandas.Any(x => x.Codigo == item.Mesa!))
                {
                    if(AppState.MesasNaMemoria!.FirstOrDefault(x => x.Codigo == item.Mesa) is not null)
                        mesasOuComandas.Add(AppState.MesasNaMemoria!.FirstOrDefault(x => x.Codigo == item.Mesa)!);
                }
            }

            foreach (var mesa in mesasOuComandas!)
            {
                if (ContagemColuna > 2)
                    ContagemColuna = 0;

                var frame = new Frame();
                var LblNumeroDeMesa = new Label { Text = mesa.Codigo, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") };
                string? NomeClienteCasoForBalcao = " ";

                if (AppState.configuracaoDoApp.Mesa)
                {
                    frame.Content = LblNumeroDeMesa;
                    frame.BackgroundColor = Color.Parse("#3B8112");
                    frame.BorderColor = Colors.Black;
                }
                else
                {
                    bool NumeroComanda = int.TryParse(mesa.Cartao, out int result);
                    string lblNumeroComanda = " ";

                    if (NumeroComanda)
                    {
                        lblNumeroComanda = result.ToString();
                    }
                    else
                    {
                        lblNumeroComanda = mesa.Cartao!;
                    }

                    LblNumeroDeMesa.Text = lblNumeroComanda;

                    frame.Content = LblNumeroDeMesa;
                    frame.BackgroundColor = Color.Parse("#3B8112");
                    frame.BorderColor = Colors.Black;
                }


                var MesaAtualOcupada = AppState.ContasNaMemoria!.Any(x => x.Mesa == mesa.Codigo);

                if (MesaAtualOcupada)
                {
                    var MesaNoContas = AppState.ContasNaMemoria!.FirstOrDefault(x => x.Mesa == mesa.Codigo && x.Status != "P");

                    if (MesaNoContas is not null)
                    {
                        if (MesaNoContas.Mesa!.Contains("B"))
                        {
                            if (!String.IsNullOrEmpty(MesaNoContas.Cliente!.Trim()))
                            {
                                NomeClienteCasoForBalcao = MesaNoContas.Cliente;
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
                    string escolhaGarcom;

                    if (mesa.Codigo!.Contains("B") && frame.BackgroundColor != Color.Parse("Yellow"))
                    {
                        escolhaGarcom = await DisplayActionSheet("Forma de visualização", null, null, ["Resumida", "Completa", "Adicionar itens"]);
                    }
                    else
                    {
                        escolhaGarcom = await DisplayActionSheet("Forma de visualização", null, null, ["Resumida", "Completa"]);

                    }

                    switch (escolhaGarcom)
                    {
                        case "Completa":
                            NumeroDaMesaOuComanda = mesa.Codigo;
                            LayoutContainer.Children.Clear();
                            await ColocaItensDaMesaNaTela(mesa.Codigo);
                            break;
                        case "Resumida":
                            NumeroDaMesaOuComanda = mesa.Codigo;
                            LayoutContainer.Children.Clear();
                            await ColocaGruposDaMesaNaTela(mesa.Codigo);
                            break;
                        case "Adicionar itens":
                            ColocaInformacoesDeBalcao(mesa.Codigo, NomeCliente: NomeClienteCasoForBalcao);
                            if (!AppState.configuracaoDoApp.ListaPorGrupo)
                                await Navigation.PushAsync(new NavigationPage(new ProdutosPage(null)));
                            else
                                await Navigation.PushAsync(new NavigationPage(new GruposPage()));
                            break;
                        default:
                            break;
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

    private void ColocaInformacoesDeBalcao(string CodBalcao, string? NomeCliente)
    {
        AppState.EBalcao = true;
        AppState.BalcaoInfos.Repetido = true;
        AppState.BalcaoInfos.NomeCliente = NomeCliente;
        AppState.BalcaoInfos!.CodBalcao = CodBalcao;
    }

    private async Task ColocaGruposDaMesaNaTela(string? numeroDaMesaOuComanda)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Produto> ProdutosNaConta = new List<Produto>();
                var contas = await db.contas.Where(x => x.Mesa == numeroDaMesaOuComanda && x.Status != "P").ToListAsync();

                if (contas is null)
                    throw new Exception("Conta não encontrada");

                Frame FrameDeMesa = new Frame
                {
                    BackgroundColor = Color.Parse("#024959"),
                };
                VerticalStackLayout LayoutDeMesa = new VerticalStackLayout();
                Frame FrameDeNumeroDaMesaOuComanda = new Frame
                {
                    Content = new Label() { FontSize = 20, FontFamily = "OpenSansSemibold", HorizontalTextAlignment = TextAlignment.Center, Text = $"Mesa: {numeroDaMesaOuComanda}", TextColor = Color.Parse("#fff") },
                    BackgroundColor = Color.Parse("#7A7A7A"),
                    Margin = new Thickness(0, 0, 0, 15)
                };
                LayoutDeMesa.Children.Add(FrameDeNumeroDaMesaOuComanda);
                FrameDeMesa.Content = LayoutDeMesa;
                LayoutContainer.Children.Add(FrameDeMesa);

                await CriaFramesDeContasResumida(contas, new VerticalStackLayout());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");
        }
    }

    private async Task ColocaItensDaMesaNaTela(string? numeroDaMesaOuComanda)
    {
        try
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Produto> ProdutosNaConta = new List<Produto>();
                var contas = await db.contas.Where(x => x.Mesa == numeroDaMesaOuComanda && x.Status != "P").ToListAsync();

                if (contas is null)
                    throw new Exception("Conta não encontrada");

                Frame FrameDeMesa = new Frame
                {
                    BackgroundColor = Color.Parse("#024959"),
                };
                VerticalStackLayout LayoutDeMesa = new VerticalStackLayout();
                Frame FrameDeNumeroDaMesaOuComanda = new Frame
                {
                    Content = new Label() { FontSize = 20, FontFamily = "OpenSansSemibold", HorizontalTextAlignment = TextAlignment.Center, Text = $"Mesa: {numeroDaMesaOuComanda}", TextColor = Color.Parse("#fff") },
                    BackgroundColor = Color.Parse("#7A7A7A"),
                    Margin = new Thickness(0, 0, 0, 15)
                };
                LayoutDeMesa.Children.Add(FrameDeNumeroDaMesaOuComanda);
                FrameDeMesa.Content = LayoutDeMesa;
                LayoutContainer.Children.Add(FrameDeMesa);

                CriaFramesDeContas(contas, new VerticalStackLayout());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    public List<Frame> CriaFramesDeContas(List<Contas>? Contas, VerticalStackLayout LayoutDeProdutosNoCarrinho)
    {
        List<Frame> frames = new List<Frame>();

        foreach (var conta in Contas!)
        {
            Frame FrameDeContainer = new Frame
            {
                BackgroundColor = Color.Parse("#024959"),
                CornerRadius = 10,
                Margin = new Thickness(0, 5, 0, 0),
                HasShadow = true,
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

            if (!String.IsNullOrEmpty(conta.Requisicao) && conta.Requisicao != " ")
            {
                Label LblDeNomeDoProduto = new Label { Text = $"Pedido Por: {conta.Requisicao}", HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };

                Grid.SetColumn(LblDeNomeDoProduto, 0);
                Grid.SetRow(LblDeNomeDoProduto, 3);

                gridContainer.Children.Add(LblDeNomeDoProduto);
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



            if (conta.Descarda!.Length > 25)
            {
                var NomeDoProduto = String.Empty;


                Label LblDeNomeDoProduto = new Label { VerticalTextAlignment = TextAlignment.Center, Text = conta.Descarda, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                LayoutDeNomeDoProduto.Children.Add(LblDeNomeDoProduto);

                for (int i = 2; i < conta.Descarda.Length; i++)
                {
                    var currentHeight = gridContainer.RowDefinitions[0].Height.Value;
                    gridContainer.RowDefinitions[0].Height = new GridLength(currentHeight + 2, GridUnitType.Absolute);
                    if (conta.Descarda.Contains("1/3"))
                        FrameDeNomeDoProduto.HeightRequest += 2;
                    else if (conta.Descarda.Contains("1/2"))
                        FrameDeNomeDoProduto.HeightRequest += 2.1;
                    else
                        FrameDeNomeDoProduto.HeightRequest += 3;
                }

            }
            else
            {
                Label LblDeNomeDoProduto = new Label { Text = conta.Descarda, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
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
            Entry InputDeQtdProduto = new Entry { IsReadOnly = true, Placeholder = conta.Qtdade.ToString(), PlaceholderColor = Color.Parse("#fff"), Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
            VerticalStackLayout LayoutDeQtdDoProduto = new VerticalStackLayout
            {
                Children =
                {
                    InputDeQtdProduto,
                    new Label { HorizontalTextAlignment = TextAlignment.Center ,Text = "Quantidade", TextColor = Color.Parse("#fff") }
                },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 0, 10)
            };


            Grid.SetColumn(LayoutDeQtdDoProduto, 0);
            Grid.SetColumnSpan(LayoutDeQtdDoProduto, 4);
            GridDeOpcoesDoProduto.Children.Add(LayoutDeQtdDoProduto);
            //-----------------------------------------------------------------------------------


            Grid.SetColumn(GridDeOpcoesDoProduto, 0);
            Grid.SetRow(GridDeOpcoesDoProduto, 1);
            gridContainer.Children.Add(GridDeOpcoesDoProduto);

            List<string?> TodasObsDaConta = new List<string?>() { conta.Obs1, conta.Obs2, conta.Obs3, conta.Obs4, conta.Obs5, conta.Obs6, conta.Obs7, conta.Obs8, conta.Obs9, conta.Obs10, conta.Obs11, conta.Obs12, conta.Obs13, conta.Obs14, conta.Obs15 };
            int linhaInicial = 4;
            int linhaAtual = linhaInicial;
            foreach (var obs in TodasObsDaConta)
            {
                if (!String.IsNullOrEmpty(obs) && obs != " ")
                {
                    gridContainer.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });

                    Frame frameDeIncrementos = new Frame
                    {
                        BackgroundColor = Color.Parse("#027373"),
                        CornerRadius = 10,
                        Margin = new Thickness(0, 5, 0, 0),
                        HasShadow = true
                    };

                    Label LblDeIncremento = new Label { Text = obs, FontSize = 15, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                    HorizontalStackLayout LayoutDeIncrementos = new HorizontalStackLayout
                    {
                        Children =
                         {
                             LblDeIncremento
                         },
                        Spacing = 150
                    };

                    frameDeIncrementos.Content = LayoutDeIncrementos;

                    Grid.SetRow(frameDeIncrementos, linhaAtual);
                    gridContainer.Children.Add(frameDeIncrementos);


                }
                linhaAtual++;
            }

            LayoutDeProdutosNoCarrinho.Add(FrameDeContainer);

            frames.Add(FrameDeContainer);

            LayoutContainer.Add(FrameDeContainer);
        }

        return frames;
    }

    public async Task<List<Frame>> CriaFramesDeContasResumida(List<Contas>? Contas, VerticalStackLayout LayoutDeProdutosNoCarrinho)
    {
        List<Frame> frames = new List<Frame>();
        List<Grupo> grupos = new List<Grupo>();

        try
        {

            foreach (var conta in Contas!)
            {

                var Produto = AppState.ProdutosMemoria!.FirstOrDefault(x => x.Codigo == conta.CodCarda1);
                if (Produto is not null)
                {
                    Produto!.Quantidade = conta.Qtdade;

                    var grupo = AppState.GruposMemoria!.FirstOrDefault(x => x.Codigo == Produto!.Grupo);

                    if (grupo is not null)
                    {
                        if (!grupos.Any(x => x.Codigo == grupo!.Codigo))
                        {
                            grupo!.Qtdade = 1 * Produto!.Quantidade;
                            grupos.Add(grupo!);
                        }
                        else
                        {
                            var grupoAtual = grupos.FirstOrDefault(x => x.Codigo == grupo!.Codigo);
                            grupoAtual!.Qtdade += 1 * Produto!.Quantidade;
                        }
                    }
                    else
                    {
                        if (!grupos.Any(x => x.Descricao == "Sem Grupo"))
                        {
                            grupos.Add(new Grupo { Codigo = "0000", Descricao = "Sem Grupo", Qtdade = 1 * Produto!.Quantidade });
                        }
                        else
                        {
                            var grupoAtual = grupos.FirstOrDefault(x => x.Descricao == "Sem Grupo");
                            grupoAtual!.Qtdade += 1 * Produto!.Quantidade;
                        }
                    }
                }

            }

            foreach (var grupo in grupos!)
            {
                Frame FrameDeContainer = new Frame
                {
                    BackgroundColor = Color.Parse("#024959"),
                    CornerRadius = 10,
                    Margin = new Thickness(0, 5, 0, 0),
                    HasShadow = true,
                };

                Grid gridContainer = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection{new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star)}
                },

                    RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(70, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(70, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(10, GridUnitType.Auto) }
                }

                };


                FrameDeContainer.Content = gridContainer;

                Frame FrameDeNomeDoProduto = new Frame
                {
                    BackgroundColor = Color.Parse("#027373")
                };

                VerticalStackLayout LayoutDeNomeDoProduto = new VerticalStackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };



                Label LblDeNomeDoProduto = new Label { Text = grupo.Descricao!, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center, FontFamily = "OpenSansSemibold", TextColor = Color.Parse("#fff") };
                LayoutDeNomeDoProduto.Children.Add(LblDeNomeDoProduto);

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
                Entry InputDeQtdProduto = new Entry { IsReadOnly = true, Placeholder = grupo.Qtdade.ToString(), PlaceholderColor = Color.Parse("#fff"), Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
                VerticalStackLayout LayoutDeQtdDoProduto = new VerticalStackLayout
                {
                    Children =
                {
                    InputDeQtdProduto,
                    new Label { HorizontalTextAlignment = TextAlignment.Center ,Text = "Quantidade", TextColor = Color.Parse("#fff") }
                },
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(0, 0, 0, 10)
                };


                Grid.SetColumn(LayoutDeQtdDoProduto, 0);
                Grid.SetColumnSpan(LayoutDeQtdDoProduto, 4);
                GridDeOpcoesDoProduto.Children.Add(LayoutDeQtdDoProduto);
                //-----------------------------------------------------------------------------------


                Grid.SetColumn(GridDeOpcoesDoProduto, 0);
                Grid.SetRow(GridDeOpcoesDoProduto, 1);
                gridContainer.Children.Add(GridDeOpcoesDoProduto);


                LayoutDeProdutosNoCarrinho.Add(FrameDeContainer);

                frames.Add(FrameDeContainer);

                LayoutContainer.Add(FrameDeContainer);
            }

            return frames;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }

        return frames;
    }
}