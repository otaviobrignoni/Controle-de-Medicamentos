using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.MedicationModule;

public class MedicationScreen : BaseScreen<Medication>, ICrudScreen
{
    IMedicationRepository MedicationRepository;
    public MedicationScreen(IMedicationRepository repository) : base(repository, "Medicamento")
    {
        MedicationRepository = repository;
    }

    protected override Medication NewEntity()
    {
        Write.WriteInColor("> Digite o nome do medicamento: ", ConsoleColor.Yellow, true);
        string name = Console.ReadLine()!.Trim();
        name.ToTitleCase();

        Write.WriteInColor("> Digite a descrição do medicamento: ", ConsoleColor.Yellow, true);
        string description = Console.ReadLine()!.Trim();
        description.ToTitleCase();

        Write.WriteInColor("> Digite a quantidade do medicamento: ", ConsoleColor.Yellow, true);
        int quantity = Validator.GetValidInt();

        Write.WriteInColor("> Digite o fornecedor do medicamento: ", ConsoleColor.Yellow, true); 
        // finalizar após ter fornecedor
        return new Medication(name, description, quantity, null);
    }

    public override void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Write.Header("Gerenciamento de Medicamentos");
            Console.WriteLine();
            Write.WriteInColor(" [1] - Cadastrar Medicamento", ConsoleColor.Cyan);
            Write.WriteInColor(" [2] - Editar Medicamento", ConsoleColor.Cyan);
            Write.WriteInColor(" [3] - Excluir Medicamento", ConsoleColor.Cyan);
            Write.WriteInColor(" [4] - Visualizar Medicamento", ConsoleColor.Cyan);
            Write.WriteInColor(" [5] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.WriteInColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            string option = Console.ReadLine()!;

            switch (option)
            {
                case "1": Add(); break;
                case "2": Edit(); break;
                case "3": Remove(); break;
                case "4": ShowAll(true); break;
                case "5": return;
                default: Write.ShowInvalidOptionMessage(); break;
            }
        }
    }
    
    protected override void ShowTableHeader()
    {
        // O sistema deve destacar medicamentos com menos de 20 unidades como "em falta"
        throw new NotImplementedException();
    }

    protected override void ShowTableRow(Medication entity)
    {
        throw new NotImplementedException();
    }
}
