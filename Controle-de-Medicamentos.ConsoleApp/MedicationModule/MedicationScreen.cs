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
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Medicamentos");
            Console.WriteLine();
            Write.InColor(" [1] - Cadastrar Medicamento", ConsoleColor.Cyan);
            Write.InColor(" [2] - Editar Medicamento", ConsoleColor.Cyan);
            Write.InColor(" [3] - Excluir Medicamento", ConsoleColor.Cyan);
            Write.InColor(" [4] - Visualizar Medicamento", ConsoleColor.Cyan);
            Write.InColor(" [5] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": Edit(); break;
                case "3": Remove(); break;
                case "4": ShowAll(true,true); break;
                case "5": return;
                default: Write.ShowInvalidOption(); break;
            }
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

    //protected override void PrintRow(Medication entity)
    //{
    //    Console.Write("│ {0, -2} │ {1, -20} │ {2, -30} │ {3, -10} │ {4, -25} │ ",
    //          entity.Id, entity.Name, entity.Description, entity.Quantity, entity.Supplier.Name);

    //    if (entity.IsLowStock())
    //    {
    //        Write.InColor("Em Falta".PadRight(18), ConsoleColor.Red, true);
    //    }
    //    else
    //    {
    //        Write.InColor("Ok".PadRight(18), ConsoleColor.Green, true);
    //    }

    //    Console.WriteLine("│");
    //}
}
