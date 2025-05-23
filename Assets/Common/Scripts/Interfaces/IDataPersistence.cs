public interface IDataPersistence<T> where T : IDataStorage, new()
{
    void SetDataManager(DataPersistenceManager<T> dataManager);
    void LoadData(T gameData);
    void SaveData(T gameData);
}
