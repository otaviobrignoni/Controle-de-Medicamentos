using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;


namespace Controle_de_Medicamentos.ConsoleApp;

public class MainScreen
{
    // declaração de variáveis repositorio
    private string option;
    private DataContext context;
    private IMedicationRepository medicamentoRepository;
    private IEmployeeRepository employeeRepository;

    // declarar as Telas
    private MedicationScreen medicationScreen;
    private EmployeeScreen employeeScreen;

    public MainScreen()
    {
        // intanciar Repositorios de cada entidade e telas
        context = new DataContext(true);
        medicamentoRepository = new MedicationRepository(context);
        medicationScreen = new MedicationScreen(medicamentoRepository, "Medicamento");
        employeeRepository = new EmployeeRepository(context);
        employeeScreen = new EmployeeScreen(employeeRepository);
    }

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Write.Header("Controle de Medicamentos");
            Console.WriteLine();
            Write.WriteInColor(" [1] - Fornecedores", ConsoleColor.Cyan);
            Write.WriteInColor(" [2] - Pacientes", ConsoleColor.Cyan);
            Write.WriteInColor(" [3] - Funcionários", ConsoleColor.Cyan);
            Write.WriteInColor(" [4] - Medicamentos", ConsoleColor.Cyan);
            Write.WriteInColor(" [5] - Prescrições Médicas", ConsoleColor.Cyan);
            Write.WriteInColor(" [6] - Requisição de Entrada", ConsoleColor.Cyan);
            Write.WriteInColor(" [7] - Requisição de Saída", ConsoleColor.Cyan);
            Write.WriteInColor(" [8] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.WriteInColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            option = Console.ReadLine();

            switch (option)
            {
                case "1": return;
                case "2": return;
                case "3": employeeScreen.ShowMenu(); break;
                case "4": medicationScreen.ShowMenu(); break;
                case "5": return;
                case "6": return;
                case "7": return;
                case "8": ShowLeaveMessage(); return;
                default: Write.ShowInvalidOptionMessage(); ShowMainMenu(); break;
            }
        }
    }

    public void ShowLeaveMessage()
    {
        Console.Clear();
        Write.WriteInColor("╔══════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
        Write.WriteInColor("║ Obrigado por usar o Contole de Medicamentos! ║", ConsoleColor.DarkCyan);
        Write.WriteInColor("║              Até a próxima!                  ║", ConsoleColor.DarkCyan);
        Write.WriteInColor("╚══════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
    }

}
