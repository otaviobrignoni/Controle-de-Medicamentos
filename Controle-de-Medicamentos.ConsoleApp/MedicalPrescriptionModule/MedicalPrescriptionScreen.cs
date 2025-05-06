using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescriptionScreen : BaseScreen<MedicalPrescription>, ICrudScreen
{
    MedicationScreen medicationScreen { get; set; }
    IMedicationRepository medicationRepository { get; set; }

    public MedicalPrescriptionScreen(IMedicalPrescriptionRepository repository, MedicationScreen medicationScreen, IMedicationRepository medicationRepository) : base(repository, "Prescrição Médica")
    {
        this.medicationScreen = medicationScreen;
        this.medicationRepository = medicationRepository;

    }
    
    public override void ShowMenu()
    {
        string[] options = new[] {"Cadastrar Prescrição Médica", "Gerar relatórios de Prescrições Médicas", "Voltar" };

        base.ShowMenu("Gerenciamento de Prescrições Médicas", options, ExecuteOption);
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

    public override void Add()
    {
        Console.Clear();
        if (!medicationScreen.ExistRegisters())
            return;
        base.Add();
    }

    protected override MedicalPrescription NewEntity()
    {
        Write.InColor("> Digite o CRM do médico (6 dígitos): ", ConsoleColor.Yellow, true);
        string doctorCRM = Console.ReadLine()!.Trim().ToTitleCase();

        Write.InColor("> Digite a quantidade de medicamentos na prescrição: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        List<PrescriptionMedication> medications = NewPrescriptionMedication(quantity);

        return new MedicalPrescription(doctorCRM, medications);
    }

    private List<PrescriptionMedication> NewPrescriptionMedication(int quantity)
    {
        var medications = new List<PrescriptionMedication>();
        for (int i = 0; i < quantity; i++)
        {
            while (true)
            {
                medicationScreen.ShowAll(false, false);
                Write.InColor($"> Digite o id do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                int medicationId = Validator.GetValidInt();
                Medication? medication = medicationRepository.GetById(medicationId);

                Write.InColor($"> Digite a dosagem do medicamento {i + 1} (Apenas o valor): ", ConsoleColor.Yellow, true);
                string dosage = Console.ReadLine()!.Trim().ToTitleCase();

                Write.InColor($"> Digite a quantidade do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                int medicationQuantity = Validator.GetValidInt();

                Write.InColor($"> Digite o período do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                string period = Console.ReadLine()!.Trim().ToTitleCase();

                PrescriptionMedication prescriptionMedication = new PrescriptionMedication(medication, dosage, medicationQuantity, period);

                if (!IsValid(prescriptionMedication))
                    continue;

                medications.Add(prescriptionMedication);
                Write.InColor($">> Medicamento n°{i + 1} adicionado com sucesso!", ConsoleColor.Green);
                Write.ShowExit();
                break;
            }
        }
        return medications;
    }

    public bool IsValid(PrescriptionMedication prescription)
    {
        string errors = prescription.Validate();
        if (string.IsNullOrEmpty(errors))
            return true;
        Write.InColor($">> (×) Erro ao cadastrar o Medicamento!", ConsoleColor.Red);
        Write.InColor(errors, ConsoleColor.Red);
        Write.ShowTryAgain();
        return false;
    }

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "CRM do Médico", "Data", "Qtd. Medicamentos", "Status" };
    }

    public override void PrintRow(string[] row, int[] widths)
    {
        Console.Write("│");
        for (int i = 0; i < row.Length; i++)
        {
            string cell = row[i];
            string padded = cell.PadRight(widths[i]);

            var originalColor = Console.ForegroundColor;

            if (cell == "Expirada")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (cell == "Fechada")
                Console.ForegroundColor = ConsoleColor.Green;
            else if (cell == "Aberta")
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write(" " + padded + " ");

            Console.ForegroundColor = originalColor;
            Console.Write("│");
        }
        Console.WriteLine();
    }
}

