using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;


[Route("suppliers")]
public class SupplierController : Controller
{
    [HttpGet("add")]
    public IActionResult ShowAddSupplierForm()
    {
        return View("Add");
    }

    [HttpPost("add")]
    public IActionResult AddSupplier(
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string cnpj
    )
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier newSupplier = new Supplier(name, phoneNumber, cnpj);

        supplierRepo.Add(newSupplier);

        ViewBag.Message = $"O registro \"{newSupplier.Name}\" foi cadastrado com sucesso!";

        return View("Notification");
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditSupplierForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier newSupplier = supplierRepo.GetById(id);

        ViewBag.Supplier = newSupplier;

        return View("Edit");
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditSupplier(
        [FromRoute] int id,
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string cnpj
    )
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier editedSupplier = new Supplier(name, phoneNumber, cnpj);

        supplierRepo.Edit(id, editedSupplier);

        ViewBag.Message = $"O registro \"{editedSupplier.Name}\" foi editado com sucesso!";

        return View("Notification");
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemoveSupplierForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier selectedSupplier = supplierRepo.GetById(id);

        ViewBag.Supplier = selectedSupplier;

        return View("Remove");
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveSupplier([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        supplierRepo.Remove(id);

        ViewBag.Message = $"O registro foi excluído com sucesso!";

        return View("Notification");
    }

    [HttpGet("show")]
    public IActionResult ShowSuppliers()
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        List<Supplier> suppliers = supplierRepo.GetAll();

        ViewBag.Suppliers = suppliers;

        return View("Show");
    }
}
