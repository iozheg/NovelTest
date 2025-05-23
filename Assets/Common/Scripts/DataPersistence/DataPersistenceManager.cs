using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

public class DataPersistenceManager<T> where T : IDataStorage, new()
{
    private readonly List<IDataPersistence<T>> _dataPersistenceObjects = new();
    private readonly List<IDataPersistence<T>> _dataSources = new();

    private IDataHandler<T> _dataHandler;
    private bool _isSaving = false;

    public T Data { get; private set; } = new();

    public DataPersistenceManager() {}

    public void SetDataHandler(IDataHandler<T> dataHandler) {
        _dataHandler = dataHandler;
    }

    public void RegisterDataSource(IDataPersistence<T> dataSource) {
        if (!_dataSources.Contains(dataSource))
        {
            _dataSources.Add(dataSource);
            dataSource.SetDataManager(this);
        }
    }

    public void UnregisterDataSource(IDataPersistence<T> dataSource) {
        if (_dataSources.Contains(dataSource))
        {
            _dataSources.Remove(dataSource);
        }
    }

    public bool LoadGame() {
        if (_dataHandler == null) {
            Debug.LogWarning("No data handler!");
        }

        T gameData = _dataHandler.Load() ?? new();
        gameData.ApplyMigrations();

        Data = gameData;

        foreach (IDataPersistence<T> obj in _dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }

        foreach (IDataPersistence<T> dataSource in _dataSources)
        {
            dataSource.LoadData(gameData);
        }

        return true;
    }

    public void SaveGame() {
        if (!_isSaving)
        {
            DelayedSave().Forget();
        }
    }

    private async UniTaskVoid DelayedSave() {
        _isSaving = true;
        await UniTask.Delay(300);

        T gameData = Data ?? new();

        foreach (IDataPersistence<T> obj in _dataPersistenceObjects)
        {
            obj.SaveData(gameData);
        }

        foreach (IDataPersistence<T> dataSource in _dataSources)
        {
            dataSource.SaveData(gameData);
        }

        _dataHandler?.Save(gameData);
        Data = gameData;

        _isSaving = false;
    }

    //--------------------
    // Debug methods
    //--------------------
    public void ResetAllProgress_Debug() {
        Data = new();
        _dataHandler?.Save(Data);
        LoadGame();
    }
}
