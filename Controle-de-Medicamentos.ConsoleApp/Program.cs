namespace Controle_de_Medicamentos.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            WebApplication app = builder.Build();

            app.MapGet("/", HomePage);

            app.Run();
        }
        static Task HomePage(HttpContext context)
        {
            string html = File.ReadAllText("Html/home.html");

            return context.Response.WriteAsync(html);
        }
    }
}
