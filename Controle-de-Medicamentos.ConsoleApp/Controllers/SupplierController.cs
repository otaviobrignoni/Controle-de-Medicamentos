using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
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
        AddSupplierViewModel addViewModel = new AddSupplierViewModel();
        return View("Add", addViewModel);
    }

    [HttpPost("add")]
    public IActionResult AddSupplier(AddSupplierViewModel addViewModel)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier newSupplier = addViewModel.ToEntity();

        supplierRepo.Add(newSupplier);

        NotificationViewModel notificationViewModel = new NotificationViewModel("Fornecedor cadastrado!", $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditSupplierForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier selectedSupplier = supplierRepo.GetById(id);

        EditSupplierViewModel editViewModel = new EditSupplierViewModel(
            id,
            selectedSupplier.Name,
            selectedSupplier.PhoneNumber,
            selectedSupplier.CNPJ
        );

        return View("Edit", editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditSupplier([FromRoute] int id, EditSupplierViewModel editViewModel)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier editedSupplier = editViewModel.ToEntity();

        supplierRepo.Edit(id, editedSupplier);

        NotificationViewModel notificationViewModel = new NotificationViewModel("Fornecedor editado!", $"O registro \"{editedSupplier.Name}\" foi editado com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemoveSupplierForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        Supplier selectedSupplier = supplierRepo.GetById(id);

        RemoveSupplierViewModel removeViewModel = new RemoveSupplierViewModel(selectedSupplier.Id, selectedSupplier.Name);

        return View("Remove", removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveSupplier([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        supplierRepo.Remove(id);

        NotificationViewModel notificationViewModel = new NotificationViewModel("Fornecedor removido!", $"O registro foi excluído com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult ShowSuppliers()
    {
        DataContext dataContext = new DataContext(true);
        ISupplierRepository supplierRepo = new SupplierRepository(dataContext);

        List<Supplier> suppliers = supplierRepo.GetAll();

        ShowSuppliersViewModel showViewModel = new ShowSuppliersViewModel(suppliers);

        return View("Show", showViewModel);
    }
}
