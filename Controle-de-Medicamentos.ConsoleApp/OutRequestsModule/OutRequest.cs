using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;
public class OutRequest : BaseEntity<OutRequest>
{
    public DateTime Date { get; set; }
    public Patient Patient { get; set; }
    public MedicalPrescription MedicalPrescription { get; set; }
    public OutRequest() { }
    public OutRequest(Patient patient, MedicalPrescription medicalPrescription)
    {
        Date = DateTime.Now;
        Patient = patient;
        MedicalPrescription = medicalPrescription;
    }
    public override void UpdateEntity(OutRequest entity)
    {
        Patient = entity.Patient;
        MedicalPrescription = entity.MedicalPrescription;
    }
    public override string Validate()
    {
        string errors = "";
        if (Patient == null)
            errors += "O Campo \"Paciente\" é obrigatório\n";
        if (MedicalPrescription == null)
            errors += "O Campo \"Prescrição Médica\" é obrigatório\n";
        else if (!MedicalPrescription.IsValid())
            errors += "A prescrição médica é inválida\n";
        foreach (PrescriptionMedication pm in MedicalPrescription.Medications)
        {
            if (pm.Medication.Quantity < pm.Quantity)
                errors += "A quantidade requisitada de uma ou mais medicamentos excede a quantidade disponível de medicamentos\n";
        }
        return errors;
    }
}
