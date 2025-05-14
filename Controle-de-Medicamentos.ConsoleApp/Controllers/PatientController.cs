using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;

[Route("patients")]
public class PatientController : Controller
{
    [HttpGet("add")]
    public IActionResult ShowAddPatientForm()
    {
        AddPatientViewModel addViewModel = new AddPatientViewModel();
        return View("Add", addViewModel);
    }

    [HttpPost("add")]
    public IActionResult AddPatient(AddPatientViewModel addViewModel)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient newPatient = addViewModel.ToEntity();

        patientRepo.Add(newPatient);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Paciente cadastrado!",
            $"O registro \"{addViewModel.Name}\" foi cadastrado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditPatientForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient selectedPatient = patientRepo.GetById(id);

        EditPatientViewModel editViewModel = new EditPatientViewModel(
            id,
            selectedPatient.Name,
            selectedPatient.PhoneNumber,
            selectedPatient.SUSCard
        );

        return View("Edit", editViewModel);
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditPatient([FromRoute] int id, EditPatientViewModel editViewModel)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient editedPatient = editViewModel.ToEntity();

        patientRepo.Edit(id, editedPatient);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Paciente editado!",
            $"O registro \"{editedPatient.Name}\" foi editado com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemovePatientForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient selectedPatient = patientRepo.GetById(id);

        RemovePatientViewModel removeViewModel = new RemovePatientViewModel(selectedPatient.Id, selectedPatient.Name);

        return View("Remove", removeViewModel);
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemovePatient([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        patientRepo.Remove(id);

        NotificationViewModel notificationViewModel = new NotificationViewModel(
            "Paciente removido!",
            "O registro foi excluído com sucesso!"
        );

        return View("Notification", notificationViewModel);
    }

    [HttpGet("show")]
    public IActionResult ShowPatients()
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        List<Patient> patients = patientRepo.GetAll();

        ShowPatientsViewModel showViewModel = new ShowPatientsViewModel(patients);

        return View("Show", showViewModel);
    }
}
