using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class PrescriptionMedication : BaseEntity<PrescriptionMedication>
{
    public Medication Medication { get; set; }
    public string Dosage { get; set; }
    public int Quantity { get; set; }
    public string Period { get; set; }

    public PrescriptionMedication() { }

    public PrescriptionMedication(Medication medication, string dosage, int quantity, string period)
    {
        Medication = medication;
        Quantity = quantity;
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

        if (!Regex.IsMatch(Dosage, @"^(?!0+(,0+)?$)\d+(,\d+)?$"))
            errors += "A 'Dosagem' deve ser um número inteiro maior que zero\n";

        if (Quantity <= 0)
            errors += "A 'quantidade' de comprimidos deve ser maior que zero\n";

        if (string.IsNullOrEmpty(Period))
            errors += "O Campo 'Período' é obrigatório\n";

        if (!Regex.IsMatch(Period, @"^\s*(\d+(?:[.,]\d+)?)(\s+.+)$"))
            errors += "O 'Período' deve conter um número seguido de uma unidade de medida (ex: 2 vezes ao dia ou 1 vez por semana)\n";

        return errors;
    }
}
