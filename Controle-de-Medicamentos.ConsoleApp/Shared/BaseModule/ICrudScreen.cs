namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public interface ICrudScreen
{
    protected abstract void ShowMenu();
    protected abstract void Add();
    protected abstract void Edit();
    protected abstract void Remove();
    protected abstract void ShowAll(bool showExit, bool useClear = false);
}
