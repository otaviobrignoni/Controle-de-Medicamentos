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
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Funcionários");
            Console.WriteLine();
            Write.InColor(" [1] - Cadastrar Funcionário", ConsoleColor.Cyan);
            Write.InColor(" [2] - Editar Funcionário", ConsoleColor.Cyan);
            Write.InColor(" [3] - Excluir Funcionário", ConsoleColor.Cyan);
            Write.InColor(" [4] - Visualizar Funcionários", ConsoleColor.Cyan);
            Write.InColor(" [5] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": Edit(); break;
                case "3": Remove(); break;
                case "4": ShowAll(true, true); break;
                case "5": return;
                default: Write.ShowInvalidOption(); break;
            }
        }
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
