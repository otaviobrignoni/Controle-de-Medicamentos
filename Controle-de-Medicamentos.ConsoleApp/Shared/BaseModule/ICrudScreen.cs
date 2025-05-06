namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public interface ICrudScreen
{
    void ShowMenu();
    void Add();
    void Edit();
    void Remove();
    void ShowAll(bool showExit, bool useClear = false);
    string[] GetHeaders();
}
