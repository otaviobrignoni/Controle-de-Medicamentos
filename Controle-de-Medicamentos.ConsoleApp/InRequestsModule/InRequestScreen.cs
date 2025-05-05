using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
public class InRequestScreen : BaseScreen<InRequest>, ICrudScreen
{
    MedicationScreen MedicationScreen { get; set; }
    EmployeeScreen EmployeeScreen { get; set; }

    public InRequestScreen(MedicationScreen medicationScreen, EmployeeScreen employeeScreen, IInRequestRepository inRequestRepository) : base(inRequestRepository, "Requisição de Entrada")
    {
        MedicationScreen = medicationScreen;
        EmployeeScreen = employeeScreen;
    }

    public override void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Requisições de Entrada");
            Console.WriteLine();
            Write.InColor(" [1] - Nova Requição de Entrada", ConsoleColor.Cyan);
            Write.InColor(" [2] - Editar Requição de Entrada", ConsoleColor.Cyan);
            Write.InColor(" [3] - Excluir Requição de Entrada", ConsoleColor.Cyan);
            Write.InColor(" [4] - Visualizar Requisições de Entrada", ConsoleColor.Cyan);
            Write.InColor(" [5] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": Edit(); break;
                case "3": Remove(); break;
                case "4": ShowAll(true,true); break;
                case "5": return;
                default: Write.ShowInvalidOptionMessage(); break;
            }
        }
    }

    protected override InRequest NewEntity()
    {
        Write.InColor(">> Digite a data da entrada", ConsoleColor.Yellow, true);
        DateTime date = Validator.GetValidDate();
        Write.InColor(">> Digite o ID do medicamento desejado: ", ConsoleColor.Yellow, true);
        MedicationScreen.ShowAll(false);
        int id1 = Validator.GetValidInt();
        Medication? medication = MedicationScreen.FindRegister(id1) ? MedicationScreen.Repository.GetById(id1) : null;
        Write.InColor(">> Digite o ID do funcionário desejado: ", ConsoleColor.Yellow, true);
        EmployeeScreen.ShowAll(false);
        int id2 = Validator.GetValidInt();
        Employee? employee = EmployeeScreen.FindRegister(id2) ? EmployeeScreen.Repository.GetById(id2) : null;
        Write.InColor(">> Digite a quantidade de medicamentos: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        return new InRequest(date, medication, employee, quantity);
    }

    protected override void ShowTableHeader()
    {
        Console.WriteLine("");
    }

    protected override void ShowTableRow(InRequest entity)
    {
        Console.WriteLine("");
    }

    protected override void ShowEndOfTable()
    {
        Console.WriteLine("");
    }
}
