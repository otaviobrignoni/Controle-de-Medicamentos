namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;

public interface IRepository<T> where T : BaseEntity<T>
{
    void Add(T newEntity);
    void Edit(int id, T editedEntity);
    void Remove(int id);
    List<T> GetAll();
    T? GetById(int id);
    bool IsEntityValid(T entity, out string errors);
    int Count();
}
