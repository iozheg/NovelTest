public interface IDataHandler<T> where T : new()
{
    T Load();
    void Save(T gameData);
}
