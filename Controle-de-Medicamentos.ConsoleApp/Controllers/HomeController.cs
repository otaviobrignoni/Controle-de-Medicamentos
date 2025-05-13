using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;


[Route("/")]
public class HomeController : Controller
{
    public IActionResult HomePage()
    {
        return View("HomePage");
    }
}
