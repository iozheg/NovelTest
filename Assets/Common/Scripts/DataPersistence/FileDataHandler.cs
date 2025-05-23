using System.IO;

using UnityEngine;

public class FileDataHandler<T> : IDataHandler<T> where T : new()
{
    private readonly string _dataDirPath = "";
    private readonly string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName) {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        Debug.Log($"FileDataHandler created with path: {_dataDirPath} and file name: {_dataFileName}");
    }

    public T Load() {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        T loadedGameData = new ();

        if (File.Exists(fullPath)) {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new (fullPath, FileMode.Open)) {
                    using (StreamReader reader = new (stream)) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedGameData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Can't load file: {fullPath}\n{e}");
            }
        }

        return loadedGameData;
    }

    public void Save(T gameData) {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new (fullPath, FileMode.Create)) {
                using (StreamWriter writer = new (stream)) {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Can't save file: {fullPath}\n{e}");
        }
    }
}
