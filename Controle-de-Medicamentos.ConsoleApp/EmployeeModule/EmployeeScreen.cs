using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
public class EmployeeScreen : BaseScreen<Employee>, ICrudScreen
{
    public EmployeeScreen(IEmployeeRepository employeeRepository) : base (employeeRepository, "Funcionário")
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
            Write.WriteInColor(" [1] - Cadastrar Funcionário", ConsoleColor.Cyan);
            Write.WriteInColor(" [2] - Editar Funcionário", ConsoleColor.Cyan);
            Write.WriteInColor(" [3] - Excluir Funcionário", ConsoleColor.Cyan);
            Write.WriteInColor(" [4] - Visualizar Funcionários", ConsoleColor.Cyan);
            Write.WriteInColor(" [5] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.WriteInColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": Edit(); break;
                case "3": Remove(); break;
                case "4": ShowAll(true); break;
                case "5": return;
                default: Write.ShowInvalidOptionMessage(); break;
            }
        }
    }

    protected override Employee NewEntity()
    {
        Write.WriteInColor("> Digite o nome do funcionário: ", ConsoleColor.Yellow, true);
        string name = Console.ReadLine().Trim().ToTitleCase();
        Write.WriteInColor("> Digite o telefone do funcionário: ", ConsoleColor.Yellow, true);
        string phoneNumber = Console.ReadLine().Trim().ToTitleCase();
        Write.WriteInColor("> Digite o CPF do funcionário: ", ConsoleColor.Yellow, true);
        string CPF = Console.ReadLine().Trim().ToTitleCase();
        
        return new Employee(name, phoneNumber, CPF);
    }

    protected override void ShowTableHeader()
    {
        throw new NotImplementedException();
    }

    protected override void ShowTableRow(Employee entity)
    {
        throw new NotImplementedException();
    }
}
