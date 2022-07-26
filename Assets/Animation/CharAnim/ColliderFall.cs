using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFall : MonoBehaviour
{
    public GameObject spawnPointPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            GameManager.instance.player.transform.position = spawnPointPlayer.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
