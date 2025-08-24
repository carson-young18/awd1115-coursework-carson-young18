namespace ClassSchedule.Models
{
    // this interface provides a blueprint for a repository that can work with any class type (T) and perform
    // basic CRUD (Create, Read, Update, Delete) operations on objects of that type.
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);

        T? Get(int id);
        T? Get(QueryOptions<T> options);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
