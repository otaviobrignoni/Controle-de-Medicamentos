using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;
public class OutRequestScreen : BaseScreen<OutRequest>, ICrudScreen
{
    PatientScreen PatientScreen { get; set; }
    MedicalPrescriptionScreen MedicalPrescriptionScreen { get; set; }


    public OutRequestScreen(PatientScreen patientScreen, MedicalPrescriptionScreen medicalPrescriptionScreen, IOutRequestRepository outRequestRepository) : base(outRequestRepository, "Requisição de Saída")
    {
        PatientScreen = patientScreen;
        MedicalPrescriptionScreen = medicalPrescriptionScreen;
    }

    public override void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Requisições de Saída");
            Console.WriteLine();
            Write.InColor(" [1] - Nova Requisição de Saída", ConsoleColor.Cyan);
            Write.InColor(" [2] - Editar Requisição de Saída", ConsoleColor.Cyan);
            Write.InColor(" [3] - Excluir Requisição de Saída", ConsoleColor.Cyan);
            Write.InColor(" [4] - Visualizar Requisições de Saída", ConsoleColor.Cyan);
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
                default: Write.ShowInvalidOptionMessage(); break;
            }
        }
    }

    protected override OutRequest NewEntity()
    {
        Write.InColor(">> Digite a data da entrada", ConsoleColor.Yellow, true);
        DateTime date = Validator.GetValidDate();
        Write.InColor(">> Digite o ID do paciente desejado: ", ConsoleColor.Yellow, true);
        PatientScreen.ShowAll(false);
        int id1 = Validator.GetValidInt();
        Patient? patient = PatientScreen.FindRegister(id1) ? PatientScreen.Repository.GetById(id1) : null;
        Write.InColor(">> Digite o ID da prescrição médica desejada: ", ConsoleColor.Yellow, true);
        MedicalPrescriptionScreen.ShowAll(false);
        int id2 = Validator.GetValidInt();
        MedicalPrescription? medicalPrescription = MedicalPrescriptionScreen.FindRegister(id2) ? MedicalPrescriptionScreen.Repository.GetById(id2) : null;

        return new OutRequest(date, patient, medicalPrescription);
    }

    protected override void ShowTableHeader()
    {
        Console.WriteLine("");
    }

    protected override void ShowTableRow(OutRequest entity)
    {
        Console.WriteLine("");
    }
}
