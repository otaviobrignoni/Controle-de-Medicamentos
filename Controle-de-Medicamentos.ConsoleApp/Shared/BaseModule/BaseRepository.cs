namespace Controle_de_Medicamentos.ConsoleApp.Shared.BaseModule;
using System;

public abstract class BaseRepository<T> where T : BaseEntity<T>
{
    protected List<T> List = new List<T>();
    protected DataContext Context { get; set; }

    public BaseRepository(DataContext context)
    {
        Context = context;
        List = GetList();
    }
    public abstract List<T> GetList();
    public virtual void Add(T entity)
    {
        entity.Id = Guid.NewGuid();
        List.Add(entity);
        Context.SaveData();
    }
    public virtual void Remove(Guid id)
    {
        T entity = GetById(id);
        List.Remove(entity);
        Context.SaveData();
    }
    public virtual void Edit(Guid id, T editedEntity)
    {
        T entity = GetById(id);
        entity.UpdateEntity(editedEntity);
        Context.SaveData();
    }
    public virtual List<T> GetAll()
    {
        return List;
    }
    public T? GetById(Guid id)
    {
        return List.FirstOrDefault(targetEntity => targetEntity.Id == id);
    }
    public virtual bool IsEntityValid(T entity, out string errors)
    {
        errors = entity.Validate();
        if (string.IsNullOrEmpty(errors))
            return true;
        return false;
    }
    public int Count()
    {
        return List.Count;
    }
}
