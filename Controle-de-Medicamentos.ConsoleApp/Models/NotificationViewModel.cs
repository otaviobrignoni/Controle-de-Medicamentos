namespace Controle_de_Medicamentos.ConsoleApp.Models;

public class NotificationViewModel
{
    public string Title { get; set; }
    public string Message { get; set; }

    public NotificationViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
