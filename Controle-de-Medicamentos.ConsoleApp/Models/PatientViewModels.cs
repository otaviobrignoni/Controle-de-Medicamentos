using System;
﻿using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.PatientModule;
namespace Controle_de_Medicamentos.ConsoleApp.Models;

public abstract class PatientFormViewModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string SUSCard { get; set; }
}

public class AddPatientViewModel : PatientFormViewModel
{
    public AddPatientViewModel() { }
    public AddPatientViewModel(string name, string phoneNumber, string susCard)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        SUSCard = susCard;
    }
}

public class EditPatientViewModel : PatientFormViewModel
{
    public Guid Id { get; set; }
    public EditPatientViewModel() { }
    public EditPatientViewModel(Guid id, string name, string phoneNumber, string susCard)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        SUSCard = susCard;
    }
}

public class RemovePatientViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RemovePatientViewModel() { }
    public RemovePatientViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class PatientDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string SUSCard { get; set; }
    public PatientDetailsViewModel(Guid id, string name, string phoneNumber, string susCard)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        SUSCard = susCard;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Name}, Telefone: {PhoneNumber}, Cartão SUS {SUSCard}";
    }
}

public class ShowPatientsViewModel
{
    public List<PatientDetailsViewModel> List { get; } = new();
    public ShowPatientsViewModel(List<Patient> patients)
    {
        foreach (Patient p in patients)
        {
            PatientDetailsViewModel detailsViewModel = p.ToDetailsViewModel();
            List.Add(detailsViewModel);
        }

    }
}
