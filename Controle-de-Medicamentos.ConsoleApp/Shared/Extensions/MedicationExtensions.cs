using System;
﻿using Controle_de_Medicamentos.ConsoleApp.MedicationModule;
using Controle_de_Medicamentos.ConsoleApp.Models;
using Controle_de_Medicamentos.ConsoleApp.SupplierModule;

namespace Controle_de_Medicamentos.ConsoleApp.Extensions;

public static class MedicationExtensions
{
    public static Medication? ToEntity(this MedicationFormViewModel viewModel, List<Supplier> suppliers)
    {
        foreach (Supplier supplier in suppliers)
            if (supplier.Id == viewModel.SupplierId)
                return new Medication(viewModel.Name, viewModel.Description, viewModel.Quantity, supplier);
        return null;
    }

    public static MedicationDetailsViewModel ToDetailsViewModel(this Medication medication)
    {
        return new MedicationDetailsViewModel(medication.Id, medication.Name, medication.Description, medication.Quantity, medication.Supplier);
    }
}
