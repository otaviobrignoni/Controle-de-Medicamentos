using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionsModule;

public class MedicalPrescription : BaseEntity<MedicalPrescription>
{
    //Campos obrigatórios
    //○ CRM do médico(6 dígitos)
    //○ Data(válida)
    //○ Lista de medicamentos(com dosagem e período) // perguntar quantos medicamentos vai adicionar 

    public string DoctorCRM { get; set; }
    public DateTime Date { get; set; }
    public List<PrescriptionMedication> Medications { get; set; }

    public MedicalPrescription() {}

    public MedicalPrescription(string doctorCRM, DateTime date, List<PrescriptionMedication> medications)
    {
        DoctorCRM = doctorCRM;
        Date = date;
        Medications = medications;
    }

    public override void UpdateEntity(MedicalPrescription entity)
    {
        DoctorCRM = entity.DoctorCRM;
        Date = entity.Date;
        Medications = entity.Medications;
    }

    public override string Validate()
    {
        string errors = "";

        if (string.IsNullOrEmpty(DoctorCRM))
            errors += "O Campo 'CRM do médico' é obrigatório\n";

        if (!Regex.IsMatch(DoctorCRM, @"^\d{6}$"))
            errors += "O CRM do médico deve conter 6 dígitos\n";

        if (Date <= DateTime.Now)
            errors += "A data da receita deve ser uma data futura\n";

        if (Medications == null || Medications.Count == 0)
            errors += "A receita deve conter pelo menos um medicamento\n";

        return errors;
    }

    /// <summary>
    /// Verifica se a prescrição médica é válida conforme as regras de negócio definidas.
    /// Atualmente considera válida se tiver sido emitida há no máximo 30 dias,
    /// mas o método pode ser expandido para incluir outras validações.
    /// </summary>
    /// <returns>
    /// Retorna <c>true</c> se a prescrição atender aos critérios de validade; caso contrário, <c>false</c>.
    /// </returns>
    public bool IsValid()
    {
        return (DateTime.Now - Date).TotalDays <= 30;
    }
}
