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

    public override void Add()
    {
        Console.Clear();
        if (!PatientScreen.ExistRegisters())
            return;

        if (!MedicalPrescriptionScreen.ExistRegisters())
            return;

        Write.Header($" Registrando {EntityName}");
        OutRequest newEntity = NewEntity();

        if (!IsValid(newEntity))
            return;

        Repository.Add(newEntity);

        foreach (PrescriptionMedication pm in newEntity.MedicalPrescription.Medications)
        {
            if (pm.Medication.IsStockLow())
                Write.InColor($">> (!) ALERTA: a medicação {pm.Medication.Name} está entrando em falta!", ConsoleColor.DarkYellow);
        }

        newEntity.MedicalPrescription.ClosePrescription();

        Write.InColor($">> (✓) {EntityName} registrado com sucesso!", ConsoleColor.Green);
        Write.Exit();
    }

    public override void ShowMenu()
    {
        string[] options = new[] { "Nova Requisição de Saída", "Visualizar Requisições de Saída", "Visualizar Requisições de Saída de um paciente", "Voltar" };

        base.ShowMenu("Gerenciamento de Requisições de Saída", options, ExecuteOption);
    }

    protected override bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: ShowAll(true, true); break;
            case 2: ShowAllPerPatient(); break;
            case 3: return true;
            default: Write.InvalidOption(); break;
        }
        return false;
    }

    protected override OutRequest NewEntity()
    {
        PatientScreen.ShowAll(false);
        Write.InColor(">> Digite o ID do paciente desejado: ", ConsoleColor.Yellow, true);
        int id1 = Validator.GetValidInt();
        Patient? patient = PatientScreen.FindRegister(id1) ? PatientScreen.Repository.GetById(id1) : null;
        MedicalPrescriptionScreen.ShowAll(false);
        Write.InColor(">> Digite o ID da prescrição médica desejada: ", ConsoleColor.Yellow, true);
        int id2 = Validator.GetValidInt();
        MedicalPrescription? medicalPrescription = MedicalPrescriptionScreen.FindRegister(id2) ? MedicalPrescriptionScreen.Repository.GetById(id2) : null;

        return new OutRequest(patient, medicalPrescription);
    }

    public void ShowAllPerPatient()
    {
        Console.Clear();
        if (!MedicalPrescriptionScreen.ExistRegisters())
            return;
        PatientScreen.ShowAll(false);
        Write.InColor(">> Digite o ID do paciente desejado: ", ConsoleColor.Yellow, true);
        int id = Validator.GetValidInt();
        Patient? patient = PatientScreen.FindRegister(id) ? PatientScreen.Repository.GetById(id) : null;
        Console.Clear();

        if (patient == null)
        {
            Write.InColor(">> (X) Paciente Inválido!", ConsoleColor.Red);
            Write.Exit();
            return;
        }

        Write.CustomHeader($" Listando Requisições de Saida do Paciente ID {patient.Id}", 52);

        if (!ExistRegisters())
            return;

        string[] headers = GetHeaders();

        List<OutRequest> entities = Repository.GetAll().Where(or => or.Patient.Id == id).ToList();
        List<string[]> lines = new List<string[]>();
        foreach (OutRequest entity in entities)
        {
            ITableConvertible convertible = entity;
            if (convertible != null)
                lines.Add(convertible.ToLineStrings());
        }

        int[] widths = new int[headers.Length];
        for (int column = 0; column < headers.Length; column++)
        {
            int maxLength = headers[column].Length;
            foreach (string[] line in lines)
            {
                int length = line[column].Length;
                if (length > maxLength)
                    maxLength = length;
            }
            widths[column] = maxLength;
        }

        PrintTopBorder(widths);
        PrintRow(headers, widths);
        PrintSeparator(widths);

        foreach (string[] line in lines)
            PrintRow(line, widths);

        PrintBottomBorder(widths);

        Write.Exit();
    }

    public override string[] GetHeaders()
    {
        return new string[] { "Id", "Data", "Paciente", "ID Prescrição" };
    }
}
