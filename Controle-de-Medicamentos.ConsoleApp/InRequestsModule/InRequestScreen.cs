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
        string[] options = new[]{"Nova Requisição de Entrada","Visualizar Requisições de Entrada", "Voltar" };

        base.ShowMenu("Gerenciamento de Requisições de Entrada", options, ExecuteOption);
    }

    protected override bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: ShowAll(true, true); break;
            case 2: return true;
            default: Write.ShowInvalidOption(); break;
        }
        return false;
    }

    protected override InRequest NewEntity()
    {
        MedicationScreen.ShowAll(false);
        Write.InColor(">> Digite o ID do medicamento desejado: ", ConsoleColor.Yellow, true);
        int id1 = Validator.GetValidInt();
        Medication? medication = MedicationScreen.FindRegister(id1) ? MedicationScreen.Repository.GetById(id1) : null;
        EmployeeScreen.ShowAll(false);
        Write.InColor(">> Digite o ID do funcionário desejado: ", ConsoleColor.Yellow, true);
        int id2 = Validator.GetValidInt();
        Employee? employee = EmployeeScreen.FindRegister(id2) ? EmployeeScreen.Repository.GetById(id2) : null;
        Write.InColor(">> Digite a quantidade de medicamentos: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        return new InRequest(medication, employee, quantity);
    }

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "Data", "Medicamento", "Funcionário", "Quantidade" };
    }
}
