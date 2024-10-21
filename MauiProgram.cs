using AppGarcomSys.Context;
using AppGarcomSys.Models;
using AppGarcomSys.Repositorys;
using AppGarcomSys.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppGarcomSys
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddDbContext<AppDbContext>(opc =>
            {

                var connectionString = $"Host=192.168.0.4;Port=5432;Database=integrador;Username=postgres;Password=69063360;";



                opc.UseNpgsql(connectionString);
            });



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
