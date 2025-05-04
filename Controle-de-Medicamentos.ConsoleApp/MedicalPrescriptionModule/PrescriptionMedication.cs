using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

class PrescriptionMedication : BaseEntity<PrescriptionMedication>
{
    Medication Medication { get; set; }
    string Dosage { get; set; }
    string Period { get; set; }

    public PrescriptionMedication() { }

    public PrescriptionMedication(Medication medication, string dosage, string period)
    {
        Medication = medication;
        Dosage = dosage;
        Period = period;
    }

    public override void UpdateEntity(PrescriptionMedication entity)
    {
        Medication = entity.Medication;
        Dosage = entity.Dosage;
        Period = entity.Period;
    }

    public override string Validate()
    {
        string errors = "";

        if (Medication == null)
            errors += "O Campo 'Medicamento' é obrigatório\n";

        if (string.IsNullOrEmpty(Dosage))
            errors += "O Campo 'Dosagem' é obrigatório\n";

        if (!Regex.IsMatch(Dosage, @"^\s*(\d+(?:[.,]\d+)?)(\s+.+)$"))
            errors += "A Dosagem deve conter um número seguido de uma unidade de medida (ex: 500mg ou 2 gotas)\n";

        if (string.IsNullOrEmpty(Period))
            errors += "O Campo 'Período' é obrigatório\n";

        if (!Regex.IsMatch(Period, @"^\s*(\d+(?:[.,]\d+)?)(\s+.+)$"))
            errors += "O Período deve conter um número seguido de uma unidade de medida (ex: 2 vezes ao dia ou 1 vez por semana)\n";

        return errors;
    }
}
