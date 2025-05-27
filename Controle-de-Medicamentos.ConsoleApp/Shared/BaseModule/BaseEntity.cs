namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public abstract class BaseEntity<T>
{
    public int Id { get; set; }
    public abstract void UpdateEntity(T entity);
    public abstract string Validate();
}
