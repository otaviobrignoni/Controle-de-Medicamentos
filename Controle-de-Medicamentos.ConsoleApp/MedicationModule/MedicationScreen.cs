using System.Globalization;
using System.Text;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;
using CsvHelper.Configuration;
using CsvHelper;
using Controle_de_Medicamentos.ConsoleApp.InRequestsModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationScreen : BaseScreen<Medication>, ICrudScreen
{
    SupplierScreen SupplierScreen {get; set; }
    ISupplierRepository SupplierRepository { get; set; }
    IInRequestRepository InRequestRepository { get; set; }

    public MedicationScreen(IMedicationRepository repository, SupplierScreen supplierScreen, ISupplierRepository supplierRepository, IInRequestRepository inRequestRepository) : base(repository, "Medicamento")
    {
        SupplierScreen = supplierScreen;
        SupplierRepository = supplierRepository;
        InRequestRepository = inRequestRepository;
    }

    public override void ShowMenu()
    {
        string[] options = new[]{"Cadastrar Medicamento", "Editar Medicamento", "Excluir Medicamento", "Visualizar Medicamento", "Exportar para Arquivo" ,"Voltar"};

        base.ShowMenu("Gerenciamento de Medicamentos", options, ExecuteOption);
    }

    protected override bool ExecuteOption(int indexSelected)
    {
        switch (indexSelected)
        {
            case 0: Add(); break;
            case 1: Edit(); break;
            case 2: Remove(); break;
            case 3: ShowAll(true, true); break;
            case 4: ExportMedications(); break;
            case 5: return true;
            default: Write.ShowInvalidOption(); break;
        }
        return false;
    }

    public override void Add()
    {
        Console.Clear();
        if (!SupplierScreen.ExistRegisters())
            return;

        base.Add();
    }

    protected override Medication NewEntity()
    {
        Write.InColor("> Digite o nome do medicamento: ", ConsoleColor.Yellow, true);
        string name = Console.ReadLine()!.Trim().ToTitleCase();

        Write.InColor("> Digite a descrição do medicamento: ", ConsoleColor.Yellow, true);
        string description = Console.ReadLine()!.Trim().ToTitleCase();

        Write.InColor("> Digite a quantidade do medicamento: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        SupplierScreen.ShowAll(false);
        Write.InColor("> Digite o ID do fornecedor do medicamento: ", ConsoleColor.Yellow, true);
        int idSuplier = Validator.GetValidInt();
        Supplier? supplier = SupplierRepository.GetById(idSuplier);  

        return new Medication(name, description, quantity, supplier);
    }

    public override bool CanRemove(int id)
    {
        Medication medication = Repository.GetById(id);
        if(InRequestRepository.HasRequisitionsForMedication(medication))
        {
            Write.InColor($"\nO medicamento {medication.Name} não pode ser excluído, pois está vinculado a requisições.", ConsoleColor.Red);
            Write.ShowExit();
            return false;
        }
        return true;
    }

    public void ExportMedications()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"medicamentos_{timestamp}.csv";
        string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ControleDeMedicamentos");

        if (!Directory.Exists(appDataFolder))
            Directory.CreateDirectory(appDataFolder);

        string filePath = Path.Combine(appDataFolder, fileName);

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Quote = '"',
            Encoding = new UTF8Encoding(true),
            ShouldQuote = args => args.FieldType == typeof(string)
        };

        StreamWriter writer = new StreamWriter(filePath, false, config.Encoding);
        CsvWriter csv = new CsvWriter(writer, config);

        csv.WriteField("Id");
        csv.WriteField("Nome");
        csv.WriteField("Descrição");
        csv.WriteField("Quantidade em Estoque");
        csv.WriteField("CNPJ do Fornecedor");
        csv.WriteField("Nome do Fornecedor");
        csv.WriteField("Telefone do Fornecedor");
        csv.NextRecord();

        foreach (Medication m in Repository.GetAll())
        {
            csv.WriteField(m.Id);
            csv.WriteField(m.Name);
            csv.WriteField(m.Description);
            csv.WriteField(m.Quantity);
            csv.WriteField(m.Supplier.CNPJ);
            csv.WriteField(m.Supplier.Name);
            csv.WriteField(m.Supplier.PhoneNumber);
            csv.NextRecord();
        }

        csv.Flush();
        writer.Close();


        Console.Clear();
        Write.InColor(">> (✓) Exportado arquivo com sucesso!", ConsoleColor.Green);
        Write.ShowExit();
    }

    public override void PrintRow(string[] row, int[] widths)
    {
        Console.Write("│");
        for (int i = 0; i < row.Length; i++)
        {
            string cell = row[i];
            string padded = cell.PadRight(widths[i]);


            var originalColor = Console.ForegroundColor;

            if (cell == "Em Falta")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (cell == "Ok")
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(" " + padded + " ");

            Console.ForegroundColor = originalColor;

            Console.Write("│");
        }
        Console.WriteLine();
    }

    public override string[] GetHeaders()
    {
        return new[] { "Id", "Nome", "Descrição", "Quantidade", "Fornecedor", "Status de Estoque" };
    }
}
