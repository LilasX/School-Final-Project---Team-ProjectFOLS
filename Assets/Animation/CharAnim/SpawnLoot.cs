using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    public List<GameObject> loot = new List<GameObject>();
    public int minRange = 5;
    public int maxRange = 10;

    private Transform spawnPos;
    private bool collected;
    public bool spawned;

    private void OnValidate()
    {
        if(minRange > maxRange)
        {
            maxRange = minRange + 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(spawned && !collected)
        {
            spawned = false;
            TakeLoot();
        }
    }

    public void TakeLoot()
    {
        collected = true;
        int num = Random.Range(minRange, maxRange);
        StartCoroutine(LootSpawning(num));
    }

    IEnumerator LootSpawning(int number)
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i< number; i++)
        {
            GameObject tempLoot = Instantiate(loot[0]);
            tempLoot.transform.position = this.spawnPos.position;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
