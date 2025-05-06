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
        string[] opcoes = new[]{"Nova Requisição de Entrada","Visualizar Requisições de Entrada", "Voltar" };

        int indiceSelecionado = 0;
        ConsoleKey tecla;

        do
        {
            Console.Clear();
            Write.Header("Gerenciamento de Requisições de Entrada");
            Console.WriteLine();

            for (int i = 0; i < opcoes.Length; i++)
            {
                if (i == indiceSelecionado)
                    Write.InColor($"-> {opcoes[i]}", ConsoleColor.Green);
                else
                    Console.WriteLine($"   {opcoes[i]}");
            }

            tecla = Console.ReadKey(true).Key;

            switch (tecla)
            {
                case ConsoleKey.UpArrow: indiceSelecionado = (indiceSelecionado == 0) ? opcoes.Length - 1 : indiceSelecionado - 1; break;

                case ConsoleKey.DownArrow: indiceSelecionado = (indiceSelecionado + 1) % opcoes.Length; break;

                case ConsoleKey.Enter: ExecutarOpcao(indiceSelecionado);
                    if (indiceSelecionado == 2) return; break;

                case ConsoleKey.Escape: return;
            }
        } while (true);
    }

    private void ExecutarOpcao(int indice)
    {
        switch (indice)
        {
            case 0: Add(); break;
            case 1: ShowAll(true, true); break;
            case 2: break;
            default: Write.ShowInvalidOption(); break;
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

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "Data", "Medicamento", "Funcionário", "Quantidade" };
    }
}
