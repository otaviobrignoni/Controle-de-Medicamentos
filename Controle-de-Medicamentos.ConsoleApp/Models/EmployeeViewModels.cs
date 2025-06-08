using System;
﻿using Controle_de_Medicamentos.ConsoleApp.EmployeeModule;
using Controle_de_Medicamentos.ConsoleApp.Extensions;
namespace Controle_de_Medicamentos.ConsoleApp.Models;

public abstract class EmployeeFormViewModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }
}

public class AddEmployeeViewModel : EmployeeFormViewModel
{
    public AddEmployeeViewModel() { }
    public AddEmployeeViewModel(string name, string phoneNumber, string cpf)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }
}

public class EditEmployeeViewModel : EmployeeFormViewModel
{
    public Guid Id { get; set; }
    public EditEmployeeViewModel() { }
    public EditEmployeeViewModel(Guid id, string name, string phoneNumber, string cpf)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }
}

public class RemoveEmployeeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RemoveEmployeeViewModel() { }
    public RemoveEmployeeViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class EmployeeDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CPF { get; set; }
    public EmployeeDetailsViewModel(Guid id, string name, string phoneNumber, string cpf)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CPF = cpf;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Name}, Telefone: {PhoneNumber}, CPF {CPF}";
    }
}

public class ShowEmployeesViewModel
{
    public List<EmployeeDetailsViewModel> List { get; } = new List<EmployeeDetailsViewModel>();
    public ShowEmployeesViewModel(List<Employee> employees)
    {
        foreach (Employee e in employees)
        {
            EmployeeDetailsViewModel detailsViewModel = e.ToDetailsViewModel();
            List.Add(detailsViewModel);
        }
    }
}
