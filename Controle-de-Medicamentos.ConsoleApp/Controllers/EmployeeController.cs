using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("employees")]
public class EmployeeController : Controller
{
    [HttpGet("add")]
    public IActionResult ShowAddEmployeeForm()
    {
        return View("Add");
    }

    [HttpPost("add")]
    public IActionResult AddEmployee(
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string cpf
    )
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee newEmployee = new Employee(name, phoneNumber, cpf);

        employeeRepo.Add(newEmployee);

        ViewBag.Message = $"O registro \"{newEmployee.Name}\" foi cadastrado com sucesso!";

        return View("Notification");
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditEmployeeForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee newEmployee = employeeRepo.GetById(id);

        ViewBag.Employee = newEmployee;

        return View("Edit");
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditEmployee(
        [FromRoute] int id,
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string cpf
    )
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee editedSupplier = new Employee(name, phoneNumber, cpf);

        employeeRepo.Edit(id, editedSupplier);

        ViewBag.Message = $"O registro \"{editedSupplier.Name}\" foi editado com sucesso!";

        return View("Notification");
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemoveEmployeeForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        Employee selectedEmployee = employeeRepo.GetById(id);

        ViewBag.Employee = selectedEmployee;

        return View("Remove");
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveEmployee([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        employeeRepo.Remove(id);

        ViewBag.Message = $"O registro foi excluído com sucesso!";

        return View("Notification");
    }

    [HttpGet("show")]
    public IActionResult ShowEmployees()
    {
        DataContext dataContext = new DataContext(true);
        IEmployeeRepository employeeRepo = new EmployeeRepository(dataContext);

        List<Employee> Employees = employeeRepo.GetAll();

        ViewBag.Employees = Employees;

        return View("Show");
    }
}
