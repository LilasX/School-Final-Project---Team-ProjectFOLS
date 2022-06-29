using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFightingArea : MonoBehaviour
{

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
}
