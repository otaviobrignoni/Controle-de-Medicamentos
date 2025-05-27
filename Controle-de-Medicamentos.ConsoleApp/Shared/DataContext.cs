using System.Text.Json;
using System.Text.Json.Serialization;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
using Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.OutRequestsModule;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Shared;

public class DataContext
{
    public List<Medication> Medications { get; set; }
    public List<Employee> Employees { get; set; }
    public List<InRequest> InRequests { get; set; }
    public List<OutRequest> OutRequests { get; set; }
    public List<Supplier> Suppliers { get; set; }
    public List<Patient> Patients { get; set; }
    public List<MedicalPrescription> MedicalPrescriptions { get; set; }

    private string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ControleDeMedicamentos");
    private string arquiveName = "SaveData.json";

    public DataContext()
    {
        Medications = new List<Medication>();
        Employees = new List<Employee>();
        InRequests = new List<InRequest>();
        OutRequests = new List<OutRequest>();
        Suppliers = new List<Supplier>();
        Patients = new List<Patient>();
        MedicalPrescriptions = new List<MedicalPrescription>();
    }
    public DataContext(bool loadData) : this()
    {
        if (loadData)
            LoadData();
    }
    public void SaveData()
    {
        string fullPath = Path.Combine(savePath, arquiveName);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

        File.WriteAllText(fullPath, json);
    }
    private void LoadData()
    {
        string fullPath = Path.Combine(savePath, arquiveName);

        if (!File.Exists(fullPath)) return;

        string json = File.ReadAllText(fullPath);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        DataContext savedContext = JsonSerializer.Deserialize<DataContext>(json, jsonOptions)!;

        if (savedContext == null) return;

        Medications = savedContext.Medications;
        Employees = savedContext.Employees;
        InRequests = savedContext.InRequests;
        OutRequests = savedContext.OutRequests;
        Suppliers = savedContext.Suppliers;
        Patients = savedContext.Patients;
        MedicalPrescriptions = savedContext.MedicalPrescriptions;
    }
}


