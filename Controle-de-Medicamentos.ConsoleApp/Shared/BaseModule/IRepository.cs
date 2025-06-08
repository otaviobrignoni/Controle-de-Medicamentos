namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using System;

public interface IRepository<T> where T : BaseEntity<T>
{
    void Add(T newEntity);
    void Edit(Guid id, T editedEntity);
    void Remove(Guid id);
    List<T> GetAll();
    T? GetById(Guid id);
    bool IsEntityValid(T entity, out string errors);
    int Count();
}
