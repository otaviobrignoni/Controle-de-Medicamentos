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
        string[] options = new[] { "Nova Requisição de Saída", "Editar Requisição de Saída", "Excluir Requisição de Saída", "Visualizar Requisições de Saída", "Voltar" };

        base.ShowMenu("Gerenciamento de Requisições de Saída", options, ExecuteOption);
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

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "Data", "Paciente", "ID Prescrição" };
    }
}
