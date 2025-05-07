using System.Globalization;
using System.Text;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;
using CsvHelper.Configuration;
using CsvHelper;
using Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationScreen : BaseScreen<Medication>, ICrudScreen
{
    SupplierScreen SupplierScreen { get; set; }
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
        string[] options = new[] { "Cadastrar Medicamento", "Editar Medicamento", "Excluir Medicamento", "Visualizar Medicamento", "Exportar Dados no formato CSV", "Importar Dados no formato CSV", "Exportar Dados no formato PDF", "Voltar" };

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
            case 5: ImportMedications(); break;
            case 6: ExportMedicationsToPdf(); break;
            case 7: return true;
            default: Write.InvalidOption(); break;
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
        if (InRequestRepository.HasRequisitionsForMedication(medication))
        {
            Write.InColor($"\nO medicamento {medication.Name} não pode ser excluído, pois está vinculado a requisições.", ConsoleColor.Red);
            Write.Exit();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Exporta a lista de medicamentos para um arquivo CSV localizado na pasta de dados do usuário.
    /// </summary>
    /// <remarks>
    /// O arquivo é salvo com um nome único baseado na data e hora da exportação (ex: <c>medicamentos_20250506_230859.csv</c>). <br/>
    /// O conteúdo inclui informações básicas do medicamento e dados do fornecedor. <br/>
    /// O arquivo é armazenado em <c>%APPDATA%\ControleDeMedicamentos</c>.
    /// </remarks>
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
        Write.Loading();
        Write.InColor(">> (✓) Exportado arquivo com sucesso!", ConsoleColor.Green);
        Write.Exit();
    }

    public void ImportMedications()
    {
        Console.Clear();
        Write.InColor(">> Informe o caminho completo do arquivo CSV para importar: ", ConsoleColor.Yellow, true);
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Write.InColor(">> (X) Arquivo não encontrado!", ConsoleColor.Red);
            Write.Exit();
            return;
        }

        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Encoding = new UTF8Encoding(true),
            PrepareHeaderForMatch = args => args.Header.ToLower().Replace(" ", "").Replace("ç", "c"),
            MissingFieldFound = null,
            HeaderValidated = null
        };

        using var reader = new StreamReader(path, config.Encoding);
        using var csv = new CsvReader(reader, config);

        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            try
            {
                int id = csv.GetField<int>("id");
                string name = csv.GetField<string>("nome");
                string description = csv.GetField<string>("descrição");
                int quantity = csv.GetField<int>("quantidadeemestoque");
                string cnpj = csv.GetField<string>("cnpjdofornecedor");
                string supplierName = csv.GetField<string>("nomedofornecedor");
                string supplierPhone = csv.GetField<string>("telefonedofornecedor");

                if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 100)
                {
                    Write.InColor(">> (X) Nome inválido", ConsoleColor.Red);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(description) || description.Length < 5 || description.Length > 255)
                {
                    Write.InColor(">> (X) Descrição inválida", ConsoleColor.Red);
                    continue;
                }

                if (quantity < 0)
                {
                    Write.InColor(">> (X) Quantidade deve ser ≥ 0", ConsoleColor.Red);
                    continue;
                }

                if (cnpj.Length != 14)
                {
                    Write.InColor(">> (X) CNPJ inválido (14 dígitos)", ConsoleColor.Red);
                    continue;
                }

                var existingSupplier = SupplierScreen.Repository.GetAll().FirstOrDefault(s => s.CNPJ == cnpj);

                if (existingSupplier == null)
                {
                    existingSupplier = new Supplier(supplierName, supplierPhone, cnpj);
                    SupplierScreen.Repository.Add(existingSupplier);
                }
                else
                {
                    existingSupplier.UpdateEntity(new Supplier(supplierName, supplierPhone, cnpj));
                }

                Medication existing = Repository.GetById(id);
                var medication = new Medication(name, description, quantity, existingSupplier);

                string validationErrors = medication.Validate();
                if (!string.IsNullOrEmpty(validationErrors))
                {
                    Write.InColor($">> (X) Erro ao validar o medicamento: {validationErrors}", ConsoleColor.Red);
                    continue;
                }

                if (existing == null)
                    Repository.Add(medication);
                else
                    Repository.Edit(id, medication);

            }
            catch (Exception ex)
            {
                Write.InColor($">> Erro ao processar linha: {ex.Message}", ConsoleColor.Red);
            }
        }
        Write.Loading();
        Write.InColor(">> (✓) Importação concluída!", ConsoleColor.Green);
        Write.Exit();
    }

    public void ExportMedicationsToPdf()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"medicamentos_{timestamp}.pdf";
        string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ControleDeMedicamentos");

        if (!Directory.Exists(appDataFolder))
            Directory.CreateDirectory(appDataFolder);

        string filePath = Path.Combine(appDataFolder, fileName);
        List<Medication> medications = Repository.GetAll();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header()
                    .Text($"Lista de Medicamentos - {DateTime.Now:dd/MM/yyyy}")
                    .FontSize(20).Bold().AlignCenter();

                page.DefaultTextStyle(x => x.FontSize(10));
                page.Content().Table(table =>
                {
                    string[] headers = new[] { "Id", "Nome", "Descrição", "Quantidade", "CNPJ", "Fornecedor", "Telefone" };

                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(20);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.ConstantColumn(64);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                    });


                    table.Header(header =>
                    {
                        foreach (var h in headers)
                            header.Cell().Element(CellStyle).Text(h).Bold();
                    });

                    foreach (var m in medications)
                    {
                        var isCritical = m.Quantity < 5;
                        table.Cell().Element(CellStyle).Text(m.Id.ToString());
                        table.Cell().Element(CellStyle).Text(m.Name);
                        table.Cell().Element(CellStyle).Text(Hyphenate(m.Description, 25));
                        table.Cell().Element(CellStyle).Text(m.Quantity.ToString())
                            .FontColor(isCritical ? Colors.Red.Medium : Colors.Black);
                        table.Cell().Element(CellStyle).Text(m.Supplier.CNPJ);
                        table.Cell().Element(CellStyle).Text(Hyphenate(m.Supplier.Name, 20));
                        table.Cell().Element(CellStyle).Text(m.Supplier.PhoneNumber);
                    }

                    IContainer CellStyle(IContainer container) =>
                        container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm:ss} — Total: {medications.Count} registros");
                });
            });
        }).GeneratePdf(filePath);
        Console.Clear();
        Write.Loading();
        Write.InColor(">> (✓) PDF exportado com sucesso!", ConsoleColor.Green);
        Write.Exit();
    }

    private string Hyphenate(string text, int maxSegmentLength)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var result = new StringBuilder();
        int current = 0;
        while (current < text.Length)
        {
            int take = Math.Min(maxSegmentLength, text.Length - current);
            result.Append(text.Substring(current, take));

            current += take;

            if (current < text.Length)
                result.Append("-\n");
        }
        return result.ToString();
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
