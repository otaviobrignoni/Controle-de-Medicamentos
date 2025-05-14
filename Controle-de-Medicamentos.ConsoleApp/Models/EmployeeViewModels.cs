using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
namespace Controle_de_Medicamentos.ConsoleApp.Models;

public abstract class EmployeeFormViewModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }
}

public class AddEmployeeViewModel : EmployeeFormViewModel
{
    public AddEmployeeViewModel() { }
    public AddEmployeeViewModel(string name, string phoneNumber, string cpf)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }
}

public class EditEmployeeViewModel : EmployeeFormViewModel
{
    public int Id { get; set; }
    public EditEmployeeViewModel() { }
    public EditEmployeeViewModel(int id, string name, string phoneNumber, string cpf)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }
}

public class RemoveEmployeeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RemoveEmployeeViewModel() { }
    public RemoveEmployeeViewModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class EmployeeDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }
    public EmployeeDetailsViewModel(int id, string name, string phoneNumber, string cpf)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Name}, Telefone: {PhoneNumber}, CPF {CPF}";
    }
}

public class ShowEmployeesViewModel
{
    public List<EmployeeDetailsViewModel> List { get; } = new();
    public ShowEmployeesViewModel(List<Employee> employees)
    {
        foreach (Employee e in employees)
            List.Add(new EmployeeDetailsViewModel(e.Id, e.Name, e.PhoneNumber, e.CPF));
    }
}
