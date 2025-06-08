using System;
﻿using Controle_de_Medicamentos.ConsoleApp.Extensions;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Models;

public abstract class SupplierFormViewModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CNPJ { get; set; }
}
public class AddSupplierViewModel : SupplierFormViewModel
{
    public AddSupplierViewModel() { }

    public AddSupplierViewModel(string name, string phoneNumber, string cnpj) : this()
    {
        Name = name;
        PhoneNumber = phoneNumber;
        CNPJ = cnpj;
    }
}
public class EditSupplierViewModel : SupplierFormViewModel
{
    public Guid Id { get; set; }

    public EditSupplierViewModel() { }
    public EditSupplierViewModel(Guid id, string name, string phoneNumber, string cnpj) : this()
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CNPJ = cnpj;
    }
}
public class RemoveSupplierViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public RemoveSupplierViewModel() { }
    public RemoveSupplierViewModel(Guid id, string name) : this()
    {
        {
            Id = id;
            Name = name;
        }
    }
}
public class ShowSuppliersViewModel
{
    public List<SupplierDetailsViewModel> List { get; } = new List<SupplierDetailsViewModel>();

    public ShowSuppliersViewModel(List<Supplier> suppliers)
    {
        foreach (Supplier s in suppliers)
        {
            SupplierDetailsViewModel detailViewModel = s.ToDetailsViewModel();
            List.Add(detailViewModel);
        }
    }
}
public class SupplierDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string CNPJ { get; set; }
    public SupplierDetailsViewModel(Guid id, string name, string phoneNumber, string cnpj)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        CNPJ = cnpj;
    }
    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Name}, Telefone: {PhoneNumber}, CNPJ {CNPJ}";
    }
}
