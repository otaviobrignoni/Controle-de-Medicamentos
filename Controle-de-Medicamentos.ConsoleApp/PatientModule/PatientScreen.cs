using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Utils;

namespace Controle_de_Medicamentos.ConsoleApp.PatientModule
{
    public class PatientScreen : BaseScreen<Patient>, ICrudScreen
    {
        public PatientScreen(IPatientRepository repository) : base(repository, "Paciente") {}

        public override void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Write.Header("Gerenciamento de Pacientes");
                Console.WriteLine();
                Write.InColor(" [1] - Cadastrar Paciente", ConsoleColor.Cyan);
                Write.InColor(" [2] - Editar Paciente", ConsoleColor.Cyan);
                Write.InColor(" [3] - Excluir Paciente", ConsoleColor.Cyan);
                Write.InColor(" [4] - Visualizar Paciente", ConsoleColor.Cyan);
                Write.InColor(" [5] - Sair", ConsoleColor.Cyan);
                Console.WriteLine();
                Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
                string option = Console.ReadLine()!;
                switch(option)
                {
                    case "1": Add(); break;
                    case "2": Edit(); break;
                    case "3": Remove(); break;
                    case "4": ShowAll(true,true); break;
                    case "5": return;
                    default: Write.ShowInvalidOptionMessage(); break;
                }
            }
        }

        protected override Patient NewEntity()
        {
            Console.WriteLine("> Digite o nome do paciente: ", ConsoleColor.Yellow, true);
            string name = Console.ReadLine()!.Trim().ToTitleCase();

            Console.WriteLine("> Digite o telefone do paciente: ", ConsoleColor.Yellow, true);
            string phone = Console.ReadLine()!.Trim().ToTitleCase();

            Console.WriteLine("> Digite o cartão do SUS do paciente: ", ConsoleColor.Yellow, true);
            string susCard = Console.ReadLine()!.Trim().ToTitleCase();

            return new Patient(name, phone, susCard);
        }

        protected override void ShowTableHeader()
        {
            Console.WriteLine("");
        }

        protected override void ShowTableRow(Patient entity)
        {
            Console.WriteLine("");
        }
    }
}
