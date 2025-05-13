using Controle_de_Medicamentos.ConsoleApp.PatientModule;
using Controle_de_Medicamentos.ConsoleApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Medicamentos.ConsoleApp.Controllers;


[Route("patients")]
public class PatientController : Controller
{
    [HttpGet("add")]
    public IActionResult ShowAddPatientForm()
    {
        return View("Add");
    }

    [HttpPost("add")]
    public IActionResult AddPatient(
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string suscard
    )
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient newPatient = new Patient(name, phoneNumber, suscard);

        patientRepo.Add(newPatient);

        ViewBag.Message = $"O registro \"{newPatient.Name}\" foi cadastrado com sucesso!";

        return View("Notification");
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult ShowEditPatientForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient newPatient = patientRepo.GetById(id);

        ViewBag.Patient = newPatient;

        return View("Edit");
    }

    [HttpPost("edit/{id:int}")]
    public IActionResult EditPatient(
        [FromRoute] int id,
        [FromForm] string name,
        [FromForm] string phoneNumber,
        [FromForm] string cnpj
    )
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient editedPatient = new Patient(name, phoneNumber, cnpj);

        patientRepo.Edit(id, editedPatient);

        ViewBag.Message = $"O registro \"{editedPatient.Name}\" foi editado com sucesso!";

        return View("Notification");
    }

    [HttpGet("remove/{id:int}")]
    public IActionResult ShowRemovePatientForm([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        Patient selectedPatient = patientRepo.GetById(id);

        ViewBag.Patient = selectedPatient;

        return View("Remove");
    }

    [HttpPost("remove/{id:int}")]
    public IActionResult RemovePatient([FromRoute] int id)
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        patientRepo.Remove(id);

        ViewBag.Message = $"O registro foi excluído com sucesso!";

        return View("Notification");
    }

    [HttpGet("show")]
    public IActionResult ShowPatients()
    {
        DataContext dataContext = new DataContext(true);
        IPatientRepository patientRepo = new PatientRepository(dataContext);

        List<Patient> patients = patientRepo.GetAll();

        ViewBag.Patients = patients;

        return View("Show");
    }


}
