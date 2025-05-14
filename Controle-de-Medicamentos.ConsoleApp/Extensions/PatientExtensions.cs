using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class PatientExtensions
{
    public static Patient ToEntity(this PatientFormViewModel viewModel)
        => new Patient(viewModel.Name, viewModel.PhoneNumber, viewModel.SUSCard);

    public static PatientDetailsViewModel ToDetailsViewModel(this Patient patient)
        => new PatientDetailsViewModel(patient.Id, patient.Name, patient.PhoneNumber, patient.SUSCard);
}
