using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;
public class OutRequest : BaseEntity<OutRequest>, ITableConvertible
{
    public DateTime Date { get; set; }
    public Patient Patient { get; set; }
    public MedicalPrescription MedicalPrescription { get; set; }
    public OutRequest() { }

    public OutRequest(DateTime date, Patient patient, MedicalPrescription medicalPrescription)
    {
        Date = date;
        Patient = patient;
        MedicalPrescription = medicalPrescription;
    }

    public override void UpdateEntity(OutRequest entity)
    {
        Date = entity.Date;
        Patient = entity.Patient;
        MedicalPrescription = entity.MedicalPrescription;
    }

    public string[] ToLineStrings()
    {
        return new string[] { Id.ToString(), Date.ToString("dd/MM/yyyy"), Patient.Name, MedicalPrescription.Id.ToString() };
    }

    public override string Validate()
    {
        string errors = "";

        if (Date < DateTime.Now)
            errors += "Não é possível entregar medicamentos no passado";
        if (Patient == null)
            errors += "O Campo \"Paciente\" é obrigatório";
        if (MedicalPrescription == null)
            errors += "O Campo \"Prescrição Médica\" é obrigatório";
        if (!MedicalPrescription.IsValid())
            errors += "A prescrição médica é inválida";
        
        return errors;
    }
}
