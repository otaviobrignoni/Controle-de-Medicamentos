using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionsModule;
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
            Write.InColor(" [2] - Grar relatórios de Prescrições Médicas", ConsoleColor.Cyan);
            Write.InColor(" [3] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": ShowAll(true, true); break;
                case "3": return;
                default: Write.ShowInvalidOptionMessage(); break;
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

        List<PrescriptionMedication> medications = NewPrescriptionMedication();

        return new MedicalPrescription(doctorCRM, date, medications);
    }

    private List<PrescriptionMedication> NewPrescriptionMedication()
    {
        List<PrescriptionMedication> medications = new List<PrescriptionMedication>();
        Write.InColor("> Digite a quantidade de medicamentos na prescrição: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        for (int i = 0; i < quantity; i++)
        {
            while (true)
            {
                medicationScreen.ShowAll(false,true);
                Write.InColor($"> Digite o id do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                int medicationId = Validator.GetValidInt();
                Medication? medication = medicationScreen.FindRegister(medicationId) ? medicationRepository.GetById(medicationId) : null;

                Write.InColor($"> Digite a dosagem do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                string dosage = Console.ReadLine()!.Trim().ToTitleCase();

                Write.InColor($"> Digite a quantidade do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                int medicationQuantity = Validator.GetValidInt();

                Write.InColor($"> Digite o período do medicamento {i + 1}: ", ConsoleColor.Yellow, true);
                string period = Console.ReadLine()!.Trim().ToTitleCase();

                PrescriptionMedication prescriptionMedication = new PrescriptionMedication(medication, dosage, medicationQuantity, period);
                
                if (!IsAPresciptionMedicationValid(prescriptionMedication))
                {
                    Write.InColor(">> Medicamento inválido, Pressione Enter para tentar novamente!", ConsoleColor.Red, true);
                    Console.ReadKey();
                    continue;
                }
                string alert = prescriptionMedication.ExceededLimits();
                Console.WriteLine(alert);

                medications.Add(prescriptionMedication);
                Write.InColor($">> Medicamento {i + 1} adicionado com sucesso!", ConsoleColor.Green);
                Write.InColor(">> Pressione Enter para continuar!", ConsoleColor.DarkYellow, true);
                Console.ReadKey();
                break;
            }
        }
        return medications;
    }

    public bool IsAPresciptionMedicationValid(PrescriptionMedication prescription)
    {
        string errors = prescription.Validate();
        if (string.IsNullOrEmpty(errors))
            return true;
        Write.InColor(errors, ConsoleColor.Red);
        return false;
    }

    protected override void ShowTableHeader()
    {
        Console.WriteLine("");
    }

    protected override void ShowTableRow(MedicalPrescription entity)
    {
        Console.WriteLine("");
    }
}
