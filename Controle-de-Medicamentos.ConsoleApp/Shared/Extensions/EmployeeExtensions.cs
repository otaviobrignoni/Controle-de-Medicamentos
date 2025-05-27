using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Models;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class EmployeeExtensions
{
    public static Employee ToEntity(this EmployeeFormViewModel viewModel)
    {
        return new Employee(viewModel.Name, viewModel.PhoneNumber, viewModel.CPF);
    }


    public static EmployeeDetailsViewModel ToDetailsViewModel(this Employee employee)
    {
        return new EmployeeDetailsViewModel(employee.Id, employee.Name, employee.PhoneNumber, employee.CPF);
    }
}

