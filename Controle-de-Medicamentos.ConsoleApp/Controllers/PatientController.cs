using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("patient")]
public class PatientController : Controller
{
    private readonly DataContext DataContext;
    private readonly IPatientRepository PatientRepo;

    public PatientController() 
    {
        DataContext = new DataContext(true);
        PatientRepo = new PatientRepository(DataContext);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var addViewModel = new AddPatientViewModel();
        return View(addViewModel);
    }

    [HttpPost("add")]
    public IActionResult Add(AddPatientViewModel addViewModel)
    {
        var newPatient = addViewModel.ToEntity();

        PatientRepo.Add(newPatient);

        var notificationViewModel = new NotificationViewModel(
            "Paciente cadastrado!",
            $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id)
    {
        var selectedPatient = PatientRepo.GetById(id);

        var editViewModel = new EditPatientViewModel(
            id,
            selectedPatient.Name,
            selectedPatient.PhoneNumber,
            selectedPatient.SUSCard
        );

        return View(editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult Edit([FromRoute] int id, EditPatientViewModel editViewModel)
    {
        var editedPatient = editViewModel.ToEntity();

        PatientRepo.Edit(id, editedPatient);

        var notificationViewModel = new NotificationViewModel(
            "Paciente editado!",
            $"O registro \"{editedPatient.Name}\" foi editado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult Remove([FromRoute] int id)
    {
        var selectedPatient = PatientRepo.GetById(id);

        var removeViewModel = new RemovePatientViewModel(selectedPatient.Id, selectedPatient.Name);

        return View(removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemoveConfirmed([FromRoute] int id)
    {
        PatientRepo.Remove(id);

        var notificationViewModel = new NotificationViewModel(
            "Paciente removido!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult Show()
    {
        var patients = PatientRepo.GetAll();

        var showViewModel = new ShowPatientsViewModel(patients);

        return View(showViewModel);
    }
}
