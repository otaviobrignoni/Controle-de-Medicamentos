using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
public class EmployeeScreen : BaseScreen<Employee>, ICrudScreen
{
    public EmployeeScreen(IEmployeeRepository employeeRepository) : base(employeeRepository, "Funcionário")
    {
        Repository = employeeRepository;
    }

    public override void ShowMenu()
    {
        string[] options = new[]{"Cadastrar Funcionário", "Editar Funcionário", "Excluir Funcionário", "Visualizar Funcionários", "Voltar"};

        base.ShowMenu("Gerenciamento de Funcionários", options, ExecuteOption);
    }

    protected override Employee NewEntity()
    {
        Write.InColor("> Digite o nome do funcionário: ", ConsoleColor.Yellow, true);
        string name = Console.ReadLine().Trim().ToTitleCase();
        Write.InColor("> Digite o telefone do funcionário: ", ConsoleColor.Yellow, true);
        string phoneNumber = Console.ReadLine().Trim().ToTitleCase();
        Write.InColor("> Digite o CPF do funcionário: ", ConsoleColor.Yellow, true);
        string CPF = Console.ReadLine().Trim().ToTitleCase();

        return new Employee(name, phoneNumber, CPF);
    }

    public override string[] GetHeaders()
    {
        return new[] { "Id", "Nome", "Telefone", "CPF" };
    }
}
