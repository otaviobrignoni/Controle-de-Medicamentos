using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("employee")]
public class EmployeeController : Controller
{
    [HttpGet("add")]
    public IActionResult ShowAddEmployeeForm()
    {
        AddEmployeeViewModel addViewModel = new AddEmployeeViewModel();
        return View("Add", addViewModel);
    }

    [HttpPost("add")]
    public IActionResult AddEmployee(AddEmployeeViewModel addViewModel)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee newEmployee = addViewModel.ToEntity();

        employeeRepo.Add(newEmployee);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Funcionário cadastrado!",
            $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditEmployeeForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee selectedEmployee = employeeRepo.GetById(id);

        EditEmployeeViewModel editViewModel = new EditEmployeeViewModel(
            id,
            selectedEmployee.Name,
            selectedEmployee.PhoneNumber,
            selectedEmployee.CPF
        );

        return View("Edit", editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditEmployee([FromRoute] int id, EditEmployeeViewModel editViewModel)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee editedEmployee = editViewModel.ToEntity();

        employeeRepo.Edit(id, editedEmployee);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Funcionário editado!",
            $"O registro \"{editedEmployee.Name}\" foi editado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemoveEmployeeForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee selectedEmployee = employeeRepo.GetById(id);

        RemoveEmployeeViewModel removeViewModel = new RemoveEmployeeViewModel(selectedEmployee.Id, selectedEmployee.Name);

        return View("Remove", removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveEmployee([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        employeeRepo.Remove(id);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Funcionário removido!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult ShowEmployees()
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        List<Employee> employees = employeeRepo.GetAll();

        ShowEmployeesViewModel showViewModel = new ShowEmployeesViewModel(employees);

        return View("Show", showViewModel);
    }
}
