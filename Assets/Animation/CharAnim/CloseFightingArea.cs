using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFightingArea : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private GameManager manager;

    public GameObject spawnPointPlayer;

    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject == manager.player)
        {
            StartCoroutine(ColliderOn());
        }
    }

    IEnumerator ColliderOn()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        manager.player.transform.position = spawnPointPlayer.transform.position;
        triggered = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            data.collidersFight.TryGetValue(id, out triggered);
            if (triggered)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void SaveData(GameData data)
    {
        if (data.collidersFight.ContainsKey(id))
        {
            data.collidersFight.Remove(id);
        }
        data.collidersFight.Add(id, triggered);
    }
}
