﻿using System.Text.RegularExpressions;
using Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

namespace Controle_de_Medicamentos.ConsoleApp.MedicalPrescriptionModule;

public class MedicalPrescription : BaseEntity<MedicalPrescription>
{
    public string DoctorCRM { get; set; }
    public DateTime Date { get; set; }
    public List<PrescriptionMedication> Medications { get; set; }
    public string Status { get; set; } = "Aberta";

    public MedicalPrescription() { }

    public MedicalPrescription(string doctorCRM, List<PrescriptionMedication> medications)
    {
        DoctorCRM = doctorCRM;
        Date = DateTime.Now;
        Medications = medications;
    }

    public override void UpdateEntity(MedicalPrescription entity)
    {
        DoctorCRM = entity.DoctorCRM;
        Date = DateTime.Now;
        Medications = entity.Medications;
    }

    public override string Validate()
    {
        string errors = "";

        if (string.IsNullOrEmpty(DoctorCRM))
            errors += "O Campo 'CRM do médico' é obrigatório\n";

        if (!Regex.IsMatch(DoctorCRM, @"^\d{6}$"))
            errors += "O 'CRM do médico' deve conter 6 dígitos\n";

        if (Medications == null || Medications.Count == 0)
            errors += "A receita deve conter pelo menos um 'medicamento'\n";

        return errors;
    }
    public bool IsValid()
    {
        if (IsClosed() && IsExpired())
            return false;
        return true;
    }
    public void ClosePrescription()
    {
        Status = "Fechada";
    }
    public void SetExpired()
    {
        if (IsExpired())
            Status = "Expirada";
    }
    private bool IsExpired()
    {
        return (DateTime.Now - Date).TotalDays > 30;
    }
    private bool IsClosed()
    {
        return Status == "Fechada";
    }
}
