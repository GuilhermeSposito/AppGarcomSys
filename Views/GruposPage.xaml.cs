using AppGarcomSys.Context;
using AppGarcomSys.Models;
using AppGarcomSys.Repositorys;

namespace AppGarcomSys.Views;

public partial class GruposPage : ContentPage
{
    private readonly GruposRepository _gruposRepository;

    public GruposPage()
    {
        _gruposRepository = new GruposRepository();

        InitializeComponent();
    }


    protected async override void OnAppearing()
    {
        try
        {
            int ContagemColuna = 0;
            int ContagemDeLinhaReal = 0;

            if(AppState.GruposMemoria == null)
            {
                AppState.GruposMemoria = await _gruposRepository.GetGrupos();
            }

            foreach (var grupo in AppState.GruposMemoria!)
            {
                if (ContagemColuna > 1)
                    ContagemColuna = 0;

                var frame = new Frame
                {                 
                    Content = new Label { Text = grupo.Descricao, HorizontalOptions = LayoutOptions.Center, FontFamily = "OpenSansSemibold", FontSize = 17, TextColor = Color.Parse("#fff")},
                    BackgroundColor = Color.Parse("#024959"),
                    BorderColor = Colors.Black
                };

                var tapGestureRecognizer = new TapGestureRecognizer();

               
                tapGestureRecognizer.Tapped += (s, e) => {
                    Navigation.PushAsync(new NavigationPage(new ProdutosPage(grupo.Codigo)));
                };

                frame.GestureRecognizers.Add(tapGestureRecognizer);

                Grid.SetColumn(frame, ContagemColuna);
                Grid.SetRow(frame, ContagemDeLinhaReal); 

                GridDeGrupos.Children.Add(frame);


                if (ContagemColuna == 1)
                {
                   GridDeGrupos.RowDefinitions.Add(new RowDefinition(GridLength.Auto) { });

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

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

}

