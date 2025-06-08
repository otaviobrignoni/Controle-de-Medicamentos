using System;
﻿using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;


[Route("supplier")]
public class SupplierController : Controller
{
    private readonly DataContext DataContext;
    private readonly ISupplierRepository SupplierRepo;

    public SupplierController()
    {
        DataContext = new DataContext(true);
        SupplierRepo = new SupplierRepository(DataContext);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var addViewModel = new AddSupplierViewModel();
        return View(addViewModel);
    }

    [HttpPost("add")]
    public IActionResult Add(AddSupplierViewModel addViewModel)
    {
        var newSupplier = addViewModel.ToEntity();

        SupplierRepo.Add(newSupplier);

        var notificationViewModel = new NotificationViewModel("Fornecedor cadastrado!", $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!");

        return View("Notification", notificationViewModel);
    }

[HttpGet("edit/{id:guid}")]
public IActionResult Edit([FromRoute] Guid id)
    {
        var selectedSupplier = SupplierRepo.GetById(id);

        var editViewModel = new EditSupplierViewModel(
            id,
            selectedSupplier.Name,
            selectedSupplier.PhoneNumber,
            selectedSupplier.CNPJ
        );

        return View(editViewModel);
    }

[HttpPost("edit/{id:guid}")]
public IActionResult Edit([FromRoute] Guid id, EditSupplierViewModel editViewModel)
    {
        var editedSupplier = editViewModel.ToEntity();

        SupplierRepo.Edit(id, editedSupplier);

        var notificationViewModel = new NotificationViewModel("Fornecedor editado!", $"O registro \"{editedSupplier.Name}\" foi editado com sucesso!");

        return View("Notification", notificationViewModel);
    }

[HttpGet("remove/{id:guid}")]
public IActionResult Remove([FromRoute] Guid id)
    {
        var selectedSupplier = SupplierRepo.GetById(id);

        var removeViewModel = new RemoveSupplierViewModel(selectedSupplier.Id, selectedSupplier.Name);

        return View(removeViewModel);
    }

[HttpPost("remove/{id:guid}")]
public IActionResult RemoveConfirmed([FromRoute] Guid id)
    {
        SupplierRepo.Remove(id);

        var notificationViewModel = new NotificationViewModel("Fornecedor removido!", $"O registro foi excluído com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult Show()
    {
        var suppliers = SupplierRepo.GetAll();

        var showViewModel = new ShowSuppliersViewModel(suppliers);

        return View(showViewModel);
    }
}
