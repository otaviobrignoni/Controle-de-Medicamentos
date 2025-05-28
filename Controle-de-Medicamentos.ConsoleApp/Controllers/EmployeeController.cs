using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("employee")]
public class EmployeeController : Controller
{

    private readonly DataContext DataContext;
    private readonly IEmployeeRepository EmployeeRepo;
    public EmployeeController()
    {
        DataContext = new DataContext(true);
        EmployeeRepo = new EmployeeRepository(DataContext);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var addViewModel = new AddEmployeeViewModel();
        return View(addViewModel);
    }

    [HttpPost("add")]
    public IActionResult Add(AddEmployeeViewModel addViewModel)
    {

        var newEmployee = addViewModel.ToEntity();

        EmployeeRepo.Add(newEmployee);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Funcionário cadastrado!",
            $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id)
    {
        var selectedEmployee = EmployeeRepo.GetById(id);

        var editViewModel = new EditEmployeeViewModel(
            id,
            selectedEmployee.Name,
            selectedEmployee.PhoneNumber,
            selectedEmployee.CPF
        );

        return View(editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id, EditEmployeeViewModel editViewModel)
    {

        var editedEmployee = editViewModel.ToEntity();

        EmployeeRepo.Edit(id, editedEmployee);

        var notificationViewModel = new NotificationViewModel(
            "Funcionário editado!",
            $"O registro \"{editedEmployee.Name}\" foi editado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult Remove([FromRoute] int id)
    {
        var selectedEmployee = EmployeeRepo.GetById(id);

        var removeViewModel = new RemoveEmployeeViewModel(selectedEmployee.Id, selectedEmployee.Name);

        return View("Remove", removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveConfirmed([FromRoute] int id)
    {
        EmployeeRepo.Remove(id);

        var notificationViewModel = new NotificationViewModel(
            "Funcionário removido!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult Show()
    {
        var employees = EmployeeRepo.GetAll();
        var showViewModel = new ShowEmployeesViewModel(employees);

        return View("Show", showViewModel);
    }
}
