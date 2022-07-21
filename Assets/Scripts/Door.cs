using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour//, IDataPersistence
{
    public Animator animator;

    private Inventory hasKeys;

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool hasOpened;

    // Start is called before the first frame update
    void Start()
    {
        hasKeys = FindObjectOfType<Inventory>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    
    {
        if (hasKeys.keys >= 1)
        {
            animator.SetBool("IsOpen", true);
            hasKeys.DoorOpened();
            hasOpened = true;
        }
        else return;
    }

    //public void LoadData(GameData data)
    //{
    //    data.doorsTriggered.TryGetValue(id, out hasOpened);
    //    if (hasOpened)
    //    {
    //        animator.SetBool("IsOpen", true);
    //    }
    //}

    //public void SaveData(GameData data)
    //{
    //    if (data.doorsTriggered.ContainsKey(id))
    //    {
    //        data.doorsTriggered.Remove(id);
    //    }
    //    data.doorsTriggered.Add(id, hasOpened);
    //}
}
