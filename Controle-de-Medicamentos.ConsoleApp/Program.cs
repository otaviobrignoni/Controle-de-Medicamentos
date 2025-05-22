namespace Controle_de_Medicamentos.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();

        }
    }
}
