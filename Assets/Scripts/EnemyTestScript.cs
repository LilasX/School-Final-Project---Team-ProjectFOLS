using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    WaveSpawner waveSpawnerScript;
    public bool Alive = true;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnerScript = FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive == false)
        {
            waveSpawnerScript.EnemyCount(-1);
            gameObject.SetActive(false);
        }
    }
}
