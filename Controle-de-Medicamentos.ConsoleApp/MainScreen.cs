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
    private string option;
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
        while (true)
        {
            Console.Clear();
            Write.Header("Controle de Medicamentos");
            Console.WriteLine();
            Write.InColor(" [1] - Fornecedores", ConsoleColor.Cyan);
            Write.InColor(" [2] - Pacientes", ConsoleColor.Cyan);
            Write.InColor(" [3] - Funcionários", ConsoleColor.Cyan);
            Write.InColor(" [4] - Medicamentos", ConsoleColor.Cyan);
            Write.InColor(" [5] - Prescrições Médicas", ConsoleColor.Cyan);
            Write.InColor(" [6] - Requisição de Entrada", ConsoleColor.Cyan);
            Write.InColor(" [7] - Requisição de Saída", ConsoleColor.Cyan);
            Write.InColor(" [8] - Sair", ConsoleColor.Cyan);
            Console.WriteLine();
            Write.InColor(">> Digite a opção desejada: ", ConsoleColor.Yellow, true);
            option = Console.ReadLine()!;

            switch (option)
            {
                case "1": supplierScreen.ShowMenu(); break;
                case "2": patientScreen.ShowMenu(); break;
                case "3": employeeScreen.ShowMenu(); break;
                case "4": medicationScreen.ShowMenu(); break;
                case "5": medicalPrescriptionScreen.ShowMenu(); break;
                case "6": inRequestScreen.ShowMenu(); break;
                case "7": outRequestScreen.ShowMenu(); break;
                case "8": ShowLeaveMessage(); return;
                default: Write.ShowInvalidOptionMessage(); ShowMainMenu(); break;
            }
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
