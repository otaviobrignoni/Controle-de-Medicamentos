using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.Utils;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;

namespace Controle_de_Medicamentos.ConsoleApp;

public class MainScreen
{
    private DataContext context;
    private IMedicationRepository medicationRepository;
    private IEmployeeRepository employeeRepository;
    private IInRequestRepository inRequestRepository;
    private IOutRequestRepository outRequestRepository;
    private ISupplierRepository suplierRepository; 
    private IPatientRepository patientRepository;
    private IMedicalPrescriptionRepository medicalPrescriptionRepository;


    private MedicationScreen medicationScreen;
    private EmployeeScreen employeeScreen;
    private InRequestScreen inRequestScreen;
    private OutRequestScreen outRequestScreen;
    private SupplierScreen supplierScreen;
    private PatientScreen patientScreen;
    private MedicalPrescriptionScreen medicalPrescriptionScreen;

    public MainScreen()
    {
        context = new DataContext(true);

        medicationRepository = new MedicationRepository(context);
        employeeRepository = new EmployeeRepository(context);
        inRequestRepository = new InRequestRepository(context);
        outRequestRepository = new OutRequestRepository(context);
        suplierRepository = new SupplierRepository(context);
        patientRepository = new PatientRepository(context);
        medicalPrescriptionRepository = new MedicalPrescriptionRepository(context);

        patientScreen = new PatientScreen(patientRepository);
        supplierScreen = new SupplierScreen(suplierRepository);
        employeeScreen = new EmployeeScreen(employeeRepository);
        medicationScreen = new MedicationScreen(medicationRepository, supplierScreen, suplierRepository);
        inRequestScreen = new InRequestScreen(medicationScreen, employeeScreen, inRequestRepository);
        medicalPrescriptionScreen = new MedicalPrescriptionScreen(medicalPrescriptionRepository, medicationScreen, medicationRepository);
        outRequestScreen = new OutRequestScreen(patientScreen, medicalPrescriptionScreen, outRequestRepository);
    }

    public void ShowMainMenu()
    {
        string[] opcoes = new[]
        {"Fornecedores", "Pacientes", "Funcionários", "Medicamentos", "Prescrições Médicas", "Requisição de Entrada", "Requisição de Saída", "Sair"};

        int indiceSelecionado = 0;
        ConsoleKey tecla;

        do
        {
            Console.Clear();
            Write.Header("Controle de Medicamentos");
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
                case ConsoleKey.UpArrow:indiceSelecionado = (indiceSelecionado == 0) ? opcoes.Length - 1 : indiceSelecionado - 1; break;

                case ConsoleKey.DownArrow: indiceSelecionado = (indiceSelecionado + 1) % opcoes.Length; break;

                case ConsoleKey.Enter: ExecutarOpcao(indiceSelecionado); break;
            }
        } while (tecla != ConsoleKey.Escape);
    }

    private void ExecutarOpcao(int indice)
    {
        switch (indice)
        {
            case 0: supplierScreen.ShowMenu(); break;
            case 1: patientScreen.ShowMenu(); break;
            case 2: employeeScreen.ShowMenu(); break;
            case 3: medicationScreen.ShowMenu(); break;
            case 4: medicalPrescriptionScreen.ShowMenu(); break;
            case 5: inRequestScreen.ShowMenu(); break;
            case 6: outRequestScreen.ShowMenu(); break;
            case 7: ShowLeaveMessage(); Environment.Exit(0); break;
            default: Write.ShowInvalidOption(); break;
        }
    }

    public void ShowLeaveMessage()
    {
        Console.Clear();
        Write.InColor("╔══════════════════════════════════════════════╗", ConsoleColor.DarkCyan);
        Write.InColor("║ Obrigado por usar o Contole de Medicamentos! ║", ConsoleColor.DarkCyan);
        Write.InColor("║              Até a próxima!                  ║", ConsoleColor.DarkCyan);
        Write.InColor("╚══════════════════════════════════════════════╝", ConsoleColor.DarkCyan);
    }
}
