using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationScreen : BaseScreen<Medication>, ICrudScreen
{
    SupplierScreen SupplierScreen {get; set; }
    ISupplierRepository supplierRepository { get; set; }

    public MedicationScreen(IMedicationRepository repository, SupplierScreen supplierScreen, ISupplierRepository supplierRepository) : base(repository, "Medicamento")
    {
        this.SupplierScreen = supplierScreen;
        this.supplierRepository = supplierRepository;
    }

    public override void ShowMenu()
    {
        string[] opcoes = new[]{"Cadastrar Medicamento", "Editar Medicamento", "Excluir Medicamento", "Visualizar Medicamento", "Voltar"};

        int indiceSelecionado = 0;
        ConsoleKey tecla;

        do
        {
            Console.Clear();
            Write.Header("Gerenciamento de Medicamentos");
            Console.WriteLine();

            for (int i = 0; i < opcoes.Length; i++)
            {
                if (i == indiceSelecionado)
                    Write.InColor($"-> {opcoes[i]}", ConsoleColor.Green);
                else
                    Console.WriteLine($"   {opcoes[i]}");
            }

            tecla = Console.ReadKey(true).Key;

            switch (tecla)
            {
                case ConsoleKey.UpArrow: indiceSelecionado = (indiceSelecionado == 0) ? opcoes.Length - 1 : indiceSelecionado - 1; break;

                case ConsoleKey.DownArrow: indiceSelecionado = (indiceSelecionado + 1) % opcoes.Length; break;

                case ConsoleKey.Enter: ExecutarOpcao(indiceSelecionado);
                    if (indiceSelecionado == 4) return; break;

                case ConsoleKey.Escape: return;
            }
        } while (true);
    }

    private void ExecutarOpcao(int indice)
    {
        switch (indice)
        {
            case 0: Add(); break;
            case 1: Edit(); break;
            case 2: Remove(); break;
            case 3: ShowAll(true, true); break;
            case 4: break;
            default: Write.ShowInvalidOption(); break;
        }
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
        Supplier? supplier = supplierRepository.GetById(idSuplier);  

        return new Medication(name, description, quantity, supplier);
    }

    public override string[] GetHeaders()
    {
        return new[] { "Id", "Nome", "Descrição", "Quantidade", "Fornecedor", "Status de Estoque" };
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
}
