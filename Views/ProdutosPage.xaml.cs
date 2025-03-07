using AppGarcomSys.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace AppGarcomSys.Views;


public partial class ProdutosPage : ContentPage
{
    public string CodigoDoGrupo { get; set; }
    public int QtdDeProdutosFracionadosAdicionados = 0;
    public int QtdFracionado = 0;
    public Guid CodigoProdutoContruindo { get; set; } = Guid.Empty;

    public ProdutosPage(string codigoDoGrupo)
    {
        CodigoDoGrupo = codigoDoGrupo;


        InitializeComponent();
    }
    public enum Tamanhos
    {
        Grande,
        Medio,
        Pequeno,
        Unico
    }

    private void CarregaProdutosNaTela(List<Produto> produtosFiltrados)
    {
        try
        {
            GridDeProdutos.Children.Clear();
            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;

            foreach (var produto in produtosFiltrados)
            {
                if (ContagemColuna > 1)
                    ContagemColuna = 0;

                Grid GridDeInfosDoProduto = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) }
                },

                    RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(70, GridUnitType.Absolute) }
                }
                };


                var LabelNomeDoProduto = new Label { Text = produto.Descricao, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") };
                Grid.SetColumn(LabelNomeDoProduto, 0);
                Grid.SetRow(LabelNomeDoProduto, 0);
                GridDeInfosDoProduto.Add(LabelNomeDoProduto);

                List<double> precos = new List<double> { produto.Preco1, produto.Preco2, produto.Preco3 };
                int numeroDeLinhasDosPrecos = 1;
                int ContagemDeInsercaoDeValorNoGrid = 0;
                foreach (var preco in precos)
                {
                    var Tamanho = string.Empty;
                    if (produto.TamanhoUnico == "S")
                    {
                        Tamanho = "";
                    }
                    else
                    {
                        if (preco == produto.Preco1)
                            Tamanho = "P:";
                        else if (preco == produto.Preco2)
                            Tamanho = "M:";
                        else if (preco == produto.Preco3)
                            Tamanho = "G:";
                    }

                    if (preco > 0)
                    {

                        GridDeInfosDoProduto.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });
                        var LabelDePreco = new Label { Text = $"{Tamanho} {preco.ToString("c")}", HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff") };

                        Grid.SetColumn(LabelDePreco, 0);
                        Grid.SetRow(LabelDePreco, numeroDeLinhasDosPrecos);
                        GridDeInfosDoProduto.Add(LabelDePreco);
                        ContagemDeInsercaoDeValorNoGrid++;

                        if (produto.TamanhoUnico == "S" && ContagemDeInsercaoDeValorNoGrid > 0)
                            break;

                        numeroDeLinhasDosPrecos++;
                    }
                }

                var frame = new Frame
                {
                    Content = GridDeInfosDoProduto,
                    BackgroundColor = Color.Parse("#024959"),
                    BorderColor = Colors.Black
                };


                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    if (produto.Fracionado == "N" && QtdFracionado == 0)
                    {
                        if (AppState.ProdutosCarrinho!.Any(x => x.Codigo == produto.Codigo))
                        {
                            var produtoExistente = AppState.ProdutosCarrinho!.FirstOrDefault(x => x.Codigo == produto.Codigo && x.Codigo2 == produto.Codigo2 && x.Codigo3 == produto.Codigo3);

                            if (produtoExistente != null)
                            {
                                produtoExistente.Quantidade++;
                            }
                        }
                        else
                        {
                            var ProdutoAdicionado = new Produto
                            {
                                Codigo = produto.Codigo,
                                Descricao = produto.Descricao,
                                Grupo = produto.Grupo,
                                Preco1 = produto.Preco1,
                                Preco2 = produto.Preco2,
                                Preco3 = produto.Preco3,
                                OcultaTablet = produto.OcultaTablet,
                                Observacao = produto.Observacao,
                                Quantidade = produto.Quantidade,
                                TamanhoUnico = produto.TamanhoUnico
                            };

                            Tamanhos? TamanhoProduto = await GeraDisplayParaPerguntaDeTamanho(ProdutoAdicionado);

                            if (TamanhoProduto is not null)
                                ProdutoAdicionado.Tamanho = TamanhoProduto.ToString();


                            if (AppState.configuracaoDoApp.RequisicaoAlfaNumerica)
                            {
                                var requisicao = await DisplayPromptAsync("Digite o nome na requisi��o", null, "OK", null, maxLength: 10);
                                ProdutoAdicionado.Requisicao = $"{requisicao}";
                            }

                            if (AppState.configuracaoDoApp.RequisicaoNumerica)
                            {
                                var requisicao = await DisplayPromptAsync("Digite o n�mero da comanda", null, "OK", null, keyboard: Keyboard.Numeric, maxLength: 10);
                                ProdutoAdicionado.Requisicao = $"{requisicao}";
                            }


                            AppState.ProdutosCarrinho!.Add(ProdutoAdicionado);

                        }

                        Navigation.PopAsync();
                        ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
                    }
                    else
                    {
                        string UserOption = string.Empty;

                        if (QtdDeProdutosFracionadosAdicionados < 1)
                            UserOption = await DisplayActionSheet("Selecione a quantidade de fracionamento", "Cancelar", null, "Sabor inteiro", "1/2", "1/3");

                        if (UserOption == "1/2" || (QtdDeProdutosFracionadosAdicionados >= 1 && QtdFracionado == 2))
                        {
                            QtdDeProdutosFracionadosAdicionados++;
                            QtdFracionado = 2;

                            if (QtdDeProdutosFracionadosAdicionados == 1)
                            {
                                var ProdutoAdicionado = new Produto
                                {
                                    Codigo = produto.Codigo,
                                    Descricao = $"1/2 {produto.Descricao} ",
                                    Grupo = produto.Grupo,
                                    Preco1 = produto.Preco1,
                                    Preco2 = produto.Preco2,
                                    Preco3 = produto.Preco3,
                                    OcultaTablet = produto.OcultaTablet,
                                    Observacao = produto.Observacao,
                                    Quantidade = produto.Quantidade
                                };

                                AppState.ProdutosCarrinho!.Add(ProdutoAdicionado);

                                CodigoProdutoContruindo = ProdutoAdicionado.IdInterno;
                            }
                            else
                            {
                                var produtoExistente = AppState.ProdutosCarrinho!.FirstOrDefault(x => x.IdInterno == CodigoProdutoContruindo);

                                if (produtoExistente != null)
                                {
                                    produtoExistente.Descricao += $"1/2 {produto.Descricao}";
                                    produtoExistente.Codigo2 = produto.Codigo;

                                    if (produto.Preco1 > produtoExistente.Preco1)
                                        produtoExistente.Preco1 = produto.Preco1;

                                    if (produto.Preco2 > produtoExistente.Preco2)
                                        produtoExistente.Preco2 = produto.Preco2;

                                    if (produto.Preco3 > produtoExistente.Preco3)
                                        produtoExistente.Preco3 = produto.Preco3;

                                }

                                if (QtdDeProdutosFracionadosAdicionados == 2)
                                {
                                    Tamanhos? TamanhoProduto = await GeraDisplayParaPerguntaDeTamanho(produtoExistente!);

                                    if (TamanhoProduto is not null)
                                        produtoExistente!.Tamanho = TamanhoProduto.ToString();


                                    if (AppState.configuracaoDoApp.RequisicaoAlfaNumerica)
                                    {
                                        var requisicao = await DisplayPromptAsync("Digite o nome na requisi��o", null, "OK", null);
                                        produtoExistente!.Requisicao = requisicao;
                                    }

                                    if (AppState.configuracaoDoApp.RequisicaoNumerica)
                                    {
                                        var requisicao = await DisplayPromptAsync("Digite o n�mero da requisi�a�", null, "OK", null, keyboard: Keyboard.Numeric);
                                        produtoExistente!.Requisicao = requisicao;
                                    }


                                    await Navigation.PopAsync();
                                    ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
                                }

                            }




                        }
                        else if (UserOption == "1/3" || (QtdDeProdutosFracionadosAdicionados >= 1 && QtdFracionado == 3))
                        {
                            QtdDeProdutosFracionadosAdicionados++;
                            QtdFracionado = 3;

                            if (QtdDeProdutosFracionadosAdicionados == 1)
                            {
                                var ProdutoAdicionado = new Produto
                                {
                                    Codigo = produto.Codigo,
                                    Descricao = $"1/3 {produto.Descricao} ",
                                    Grupo = produto.Grupo,
                                    Preco1 = produto.Preco1,
                                    Preco2 = produto.Preco2,
                                    Preco3 = produto.Preco3,
                                    OcultaTablet = produto.OcultaTablet,
                                    Observacao = produto.Observacao,
                                    Quantidade = produto.Quantidade,
                                    TamanhoUnico = produto.TamanhoUnico
                                };

                                AppState.ProdutosCarrinho!.Add(ProdutoAdicionado);

                                CodigoProdutoContruindo = ProdutoAdicionado.IdInterno;
                            }
                            else
                            {
                                var produtoExistente = AppState.ProdutosCarrinho!.FirstOrDefault(x => x.IdInterno == CodigoProdutoContruindo);

                                if (produtoExistente != null)
                                {
                                    produtoExistente.Descricao += $" / 1/3 {produto.Descricao}";
                                    if (String.IsNullOrEmpty(produtoExistente.Codigo2) && QtdDeProdutosFracionadosAdicionados == 2)
                                        produtoExistente.Codigo2 = produto.Codigo;

                                    if (!String.IsNullOrEmpty(produtoExistente.Codigo2) && String.IsNullOrEmpty(produto.Codigo3) && QtdDeProdutosFracionadosAdicionados == 3)
                                        produtoExistente.Codigo3 = produto.Codigo;

                                    if (produto.Preco1 > produtoExistente.Preco1)
                                        produtoExistente.Preco1 = produto.Preco1;

                                    if (produto.Preco2 > produtoExistente.Preco2)
                                        produtoExistente.Preco2 = produto.Preco2;

                                    if (produto.Preco3 > produtoExistente.Preco3)
                                        produtoExistente.Preco3 = produto.Preco3;

                                }


                                if (QtdDeProdutosFracionadosAdicionados == 3)
                                {
                                    Tamanhos? TamanhoProduto = await GeraDisplayParaPerguntaDeTamanho(produtoExistente!);

                                    if (TamanhoProduto is not null)
                                        produtoExistente!.Tamanho = TamanhoProduto.ToString();

                                    if (AppState.configuracaoDoApp.RequisicaoAlfaNumerica)
                                    {
                                        var requisicao = await DisplayPromptAsync("Digite o nome na requisi��o", null, "OK", null);
                                        produtoExistente!.Requisicao = requisicao;
                                    }

                                    if (AppState.configuracaoDoApp.RequisicaoNumerica)
                                    {
                                        var requisicao = await DisplayPromptAsync("Digite o n�mero da requisi��o", null, "OK", null, keyboard: Keyboard.Numeric);
                                        produtoExistente!.Requisicao = requisicao;
                                    }

                                    await Navigation.PopAsync();
                                    ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());
                                }
                            }

                        }
                        else if (UserOption == "Sabor inteiro")
                        {
                            if (AppState.ProdutosCarrinho!.Any(x => x.Codigo == produto.Codigo))
                            {
                                var produtoExistente = AppState.ProdutosCarrinho!.FirstOrDefault(x => x.Codigo == produto.Codigo && x.Codigo2 == produto.Codigo2 && x.Codigo3 == produto.Codigo3);

                                if (produtoExistente != null)
                                {
                                    produtoExistente.Quantidade++;
                                }
                            }
                            else
                            {

                                var ProdutoAdicionado = new Produto
                                {
                                    Codigo = produto.Codigo,
                                    Descricao = produto.Descricao,
                                    Grupo = produto.Grupo,
                                    Preco1 = produto.Preco1,
                                    Preco2 = produto.Preco2,
                                    Preco3 = produto.Preco3,
                                    OcultaTablet = produto.OcultaTablet,
                                    Observacao = produto.Observacao,
                                    Quantidade = produto.Quantidade,
                                    TamanhoUnico = produto.TamanhoUnico
                                };

                                Tamanhos? TamanhoProduto = await GeraDisplayParaPerguntaDeTamanho(ProdutoAdicionado);

                                if (TamanhoProduto is not null)
                                    ProdutoAdicionado.Tamanho = TamanhoProduto.ToString();


                                if (AppState.configuracaoDoApp.RequisicaoAlfaNumerica)
                                {
                                    var requisicao = await DisplayPromptAsync("Digite o nome na requisi��o", null, "OK", null);
                                    ProdutoAdicionado!.Requisicao = requisicao;
                                }

                                if (AppState.configuracaoDoApp.RequisicaoNumerica)
                                {
                                    var requisicao = await DisplayPromptAsync("Digite o n�mero da requisi��o", null, "OK", null, keyboard: Keyboard.Numeric);
                                    ProdutoAdicionado!.Requisicao = requisicao;
                                }


                                AppState.ProdutosCarrinho!.Add(ProdutoAdicionado);

                            }


                            Navigation.PopAsync();
                            ((FlyoutPage)App.Current.MainPage).Detail = new NavigationPage(new Carrinho());

                        }
                    };
                };

                frame.GestureRecognizers.Add(tapGestureRecognizer);

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
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void GeraBuscaDeItensNaTela()
    {
        try
        {
            Frame FrameDeSerchBar = new Frame() { HeightRequest = 75, BackgroundColor = Color.Parse("#024959"), Margin = new Thickness(10, 10, 10, 0), BorderColor = Color.Parse("Black"), CornerRadius = 50 };
            SearchBar searchBar = new SearchBar { Placeholder = "Pesquisar Produtos", PlaceholderColor = Color.Parse("#fff"), TextColor = Color.Parse("#fff") };
            FrameDeSerchBar.Content = searchBar;
            LayoutDePesquisa.Children.Add(FrameDeSerchBar);

            searchBar.TextChanged += (s, e) =>
            {
                var texto = e.NewTextValue;

                var produtosProcurados = AppState.ProdutosMemoria!.Where(x => x.Descricao!.Contains(texto, StringComparison.OrdinalIgnoreCase) || x.Codigo!.Contains(texto, StringComparison.OrdinalIgnoreCase)).ToList();
                CarregaProdutosNaTela(produtosProcurados);

                if (String.IsNullOrEmpty(texto))
                    GridDeProdutos.Children.Clear();
            };


        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected async override void OnAppearing()
    {
        try
        {
            List<Produto> produtosFiltrados;


            if (CodigoDoGrupo is null)
                produtosFiltrados = AppState.ProdutosMemoria!;
            else
                produtosFiltrados = AppState.ProdutosMemoria!.Where(x => x.Grupo == CodigoDoGrupo).ToList();

            if (!AppState.configuracaoDoApp.BuscaDeItens)
                CarregaProdutosNaTela(produtosFiltrados);

            if (AppState.configuracaoDoApp.BuscaDeItens)
            {
                GeraBuscaDeItensNaTela();
            }

            base.OnAppearing();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "Ok");
            await Console.Out.WriteLineAsync(ex.ToString());
        }

    }

    public async Task<Tamanhos?> GeraDisplayParaPerguntaDeTamanho(Produto ProdutoAdicionado)
    {
        string UserOptionSize = string.Empty;

        double PrecoPequeno = ProdutoAdicionado.Preco1;
        double PrecoMedio = ProdutoAdicionado.Preco2;
        double PrecoGrande = ProdutoAdicionado.Preco3;


        if (ProdutoAdicionado.TamanhoUnico == "S")
            return Tamanhos.Unico;

        if (PrecoPequeno > 0 && PrecoMedio == 0 && PrecoGrande == 0)
            return Tamanhos.Unico;


        if (PrecoGrande > 0 && PrecoMedio > 0 && PrecoPequeno > 0)
        {
            UserOptionSize = await DisplayActionSheet($"Produto Montado tem tr�s tamanhos, qual voc� deseja ?", null, null, $"Grande {PrecoGrande.ToString("c")}", $"M�dio {PrecoMedio.ToString("c")}", $"Pequeno {PrecoPequeno.ToString("c")}");
        }
        else if (PrecoGrande > 0 && PrecoMedio > 0)
        {
            UserOptionSize = await DisplayActionSheet($"Produto Montado tem Dois tamanhos, qual voc� deseja ?", null, null, $"Grande {PrecoGrande.ToString("c")}", $"M�dio {PrecoMedio.ToString("c")}");
        }
        else if (PrecoGrande > 0 && PrecoPequeno > 0)
        {
            UserOptionSize = await DisplayActionSheet($"Produto Montado tem Dois tamanhos, qual voc� deseja ?", null, null, $"Grande {PrecoGrande.ToString("c")}", $"Pequeno {PrecoPequeno.ToString("c")}");
        }
        else if (PrecoPequeno > 0 && PrecoMedio > 0)
        {
            UserOptionSize = await DisplayActionSheet($"Produto Montado tem Dois tamanhos, qual voc� deseja ?", null, null, $"M�dio {PrecoMedio.ToString("c")}", $"Pequeno {PrecoPequeno.ToString("c")}");

        }

        if (UserOptionSize.Contains("Grande"))
            return Tamanhos.Grande;
        else if (UserOptionSize.Contains("M�dio"))
            return Tamanhos.Medio;
        else if (UserOptionSize.Contains("Pequeno"))
            return Tamanhos.Pequeno;
        else
            return Tamanhos.Grande;

    }
}