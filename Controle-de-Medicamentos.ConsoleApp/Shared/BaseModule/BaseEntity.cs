namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using System;

public abstract class BaseEntity<T>
{
    public Guid Id { get; set; }
    public abstract void UpdateEntity(T entity);
    public abstract string Validate();
}
