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
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Prescrições Médicas");
            Console.WriteLine();
            Write.InColor(" [1] - Cadastrar Prescrição Médica", ConsoleColor.Cyan);
            Write.InColor(" [2] - Gerar relatórios de Prescrições Médicas", ConsoleColor.Cyan);
            Write.InColor(" [3] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": ShowAll(true, true); break;
                case "3": return;
                default: Write.ShowInvalidOption(); break;
            }
        }
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

        Write.InColor("> Digite a data da prescrição (dd/MM/yyyy): ", ConsoleColor.Yellow, true);
        DateTime date = Validator.GetValidDate();
      
        Write.InColor("> Digite a quantidade de medicamentos na prescrição: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        List<PrescriptionMedication> medications = NewPrescriptionMedication(quantity);

        return new MedicalPrescription(doctorCRM, date, medications);
    }

    private List<PrescriptionMedication> NewPrescriptionMedication(int quantity )
    {
        var medications = new List<PrescriptionMedication>();
        for (int i = 0; i < quantity; i++)
        {
            while (true)
            {
                medicationScreen.ShowAll(false,true);
                Write.InColor($"> Digite o id do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                int medicationId = Validator.GetValidInt();
                Medication? medication = medicationRepository.GetById(medicationId);

                Write.InColor($"> Digite a dosagem do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
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
}
