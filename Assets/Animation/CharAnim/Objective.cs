using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objective : MonoBehaviour, IDataPersistence
{

    private GameManager manager;

    private DataPersistenceManager dpManager;

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool objActive;
    public TextMeshProUGUI objectiveText;
    private bool collision;
    public string mission;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        dpManager = DataPersistenceManager.instance;
        objActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == manager.player && !collision)
        {
            objActive = true;
            collision = true;
            this.gameObject.SetActive(false);
            objectiveText.text = mission;

            dpManager.SaveGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        data.objectiveTriggered.TryGetValue(id, out collision);
        if (collision)
        {
            this.gameObject.SetActive(false);
        }

        objectiveText.text = data.objectiveMission;
    }

    public void SaveData(GameData data)
    {
        if (data.objectiveTriggered.ContainsKey(id))
        {
            data.objectiveTriggered.Remove(id);
        }
        data.objectiveTriggered.Add(id, collision);

        data.objectiveMission = objectiveText.text;
    }
}
