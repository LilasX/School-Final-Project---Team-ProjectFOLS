using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DataPersistenceManager))]
class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataPersistenceManager dataPersistenceManager = (DataPersistenceManager)target;
        if (GUILayout.Button("Delete Save File"))
        {
            dataPersistenceManager.DeleteFile();
        }
    }
}

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager.");
            //Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        Debug.Log(Application.persistentDataPath);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No data found. Initializing data to defaults.");
            NewGame();
        }

        foreach(IDataPersistence dataPersistenceObjs in dataPersistenceObjects)
        {
            dataPersistenceObjs.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistenceObjs in dataPersistenceObjects)
        {
            dataPersistenceObjs.SaveData(gameData);
        }

        dataHandler.Save(gameData);
    }

    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void DeleteFile()
    {
        if(File.Exists(Application.persistentDataPath + "/data.game"))
        {
            File.Delete(Application.persistentDataPath + "/data.game");
            Debug.Log("Save File of the game has been deleted.");
        }
        else
        {
            Debug.Log("No Save File was found to be deleted.");
        }
    }
}
