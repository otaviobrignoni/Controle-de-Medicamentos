using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.InRequestsModule;
public class InRequest : BaseEntity<InRequest>
{
    public DateTime Date { get; set; }
    public Medication Medication { get; set; }
    public Employee Employee { get; set; }
    public int Quantity { get; set; }
    public InRequest() { }
    public InRequest(DateTime date, Medication medication, Employee employee, int quantity)
    {
        Date = date;
        Medication = medication;
        Employee = employee;
        Quantity = quantity;
    }

    public override void UpdateEntity(InRequest entity)
    {
        Date = entity.Date;
        Medication = entity.Medication;
        Employee = entity.Employee;
        Quantity = entity.Quantity;
    }

    public override string Validate()
    {
        string errors = "";

        if (Date < DateTime.Now)
            errors += "Não é possível entregar medicamentos no passado";
        if (Medication == null)
            errors += "O Campo \"Medicamento\" é obrigatório";
        if (Employee == null)
            errors += "O Campo \"Funcionário\" é obrigatório";
        if (Quantity <= 0)
            errors += "A quantidade deve ser maior que zero";
        return errors;
    }
}
