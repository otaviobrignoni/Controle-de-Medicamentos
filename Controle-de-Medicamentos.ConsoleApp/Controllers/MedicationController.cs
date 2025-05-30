using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;
using Microsoft.AspNetCore.Mvc;
namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("medication")]
public class MedicationController : Controller
{
    private readonly DataContext DataContext;
    private readonly IMedicationRepository MedicationRepo;
    private readonly ISupplierRepository SupplierRepo;

    public MedicationController()
    {
        DataContext = new DataContext(true);
        MedicationRepo = new MedicationRepository(DataContext);
        SupplierRepo = new SupplierRepository(DataContext);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var addViewModel = new AddMedicationViewModel(SupplierRepo.GetAll());

        return View(addViewModel);
    }

    [HttpPost("add")]
    public IActionResult Add(AddMedicationViewModel addViewModel)
    {

        var medication = addViewModel.ToEntity(SupplierRepo.GetAll());

        MedicationRepo.Add(medication);

        var notificationViewModel = new NotificationViewModel("Medicação cadastrada!", $"O registro \"{medication.Name}\" foi cadastrado com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id)
    {

        var selectedMedication = MedicationRepo.GetById(id);

        var editViewModel = new EditMedicationViewModel(selectedMedication.Id, selectedMedication.Name, selectedMedication.Description, selectedMedication.Quantity, selectedMedication.Supplier.Id, SupplierRepo.GetAll());

        return View(editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id, EditMedicationViewModel editViewModel)
    {
        var editedMedication = editViewModel.ToEntity(SupplierRepo.GetAll());

        MedicationRepo.Edit(id, editedMedication);

        var notificationViewModel = new NotificationViewModel("Medicação editado!", $"O registro \"{editedMedication.Name}\" foi editado com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult Remove([FromRoute] int id)
    {
        var selectedMedication = MedicationRepo.GetById(id);
        var removeViewModel = new RemoveMedicationViewModel(selectedMedication.Id, selectedMedication.Name);
        return View(removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveConfirmed([FromRoute] int id)
    {
        MedicationRepo.Remove(id);

        var notificationViewModel = new NotificationViewModel("Medicamento removido!", "O registro foi excluído com sucesso!");

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult Show()
    {

        var showViewModel = new ShowMedicationsViewModel(MedicationRepo.GetAll());

        return View(showViewModel);
    }
}
