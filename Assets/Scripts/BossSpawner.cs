using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public PoolingManager poolingManager;

    public GameObject bossSpawner;
    public GameObject chestSpawner;

    public GameObject boss_Warrior;
    public GameObject boss_Shaman;
    public GameObject boss_Golem;

    public GameObject bossInstant;
    public GameObject bossPooled;

    public GameObject treasureChest;
    public GameObject chestInstant;

    public Bounds boundBox;

    public bool isPooling = false;
    public bool isWarrior = false;
    public bool isShaman = false;
    public bool isGolem = false;


    public void SpawnBoss()
    {
        if (!isPooling)
        {
            if (isWarrior)
            {
                bossInstant = Instantiate(boss_Warrior, bossSpawner.transform.position, bossSpawner.transform.rotation);
            }
            else if (isShaman)
            {
                bossInstant = Instantiate(boss_Shaman, bossSpawner.transform.position, bossSpawner.transform.rotation);
            }
            else if (isGolem)
            {
                //bossInstant = Instantiate(boss_Golem, spawner.transform.position, spawner.transform.rotation);
            }
            bossInstant.gameObject.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox); 
            bossInstant.gameObject.GetComponent<EnemyMain>().waveSpawnerObject = gameObject; 
        }
        else if (isPooling)
        {
            if (isWarrior)
            {
                bossPooled = poolingManager.callGoblinWarrior();
            }
            else if (isShaman)
            {
                bossPooled = poolingManager.callGoblinWarrior();
            }
            else if (isGolem)
            {
                //bossPooled = poolingManager.callGolem();
            }
            bossPooled.transform.position = bossSpawner.transform.position;
            bossPooled.transform.rotation = bossSpawner.transform.rotation;
            bossPooled.GetComponent<EnemyMain>().InitializeEnemy();
            bossPooled.GetComponent<EnemyMain>().waveSpawnerObject = gameObject;
            bossPooled.GetComponent<EnemyBehaviour>().InitializeBehaviour();
            bossPooled.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox);
            bossPooled.SetActive(true);
        }
    }

    public void SpawnTreasureChest()
    {
        chestInstant = Instantiate(treasureChest, chestSpawner.transform.position, chestSpawner.transform.rotation);
        chestInstant.GetComponent<LootBox>().Boss1Defeated = true;
        //Want to Introduce PoolingSystem to TreasureChest
    }

    // Start is called before the first frame update
    void Start()
    {
        poolingManager = PoolingManager.instance;
        SpawnBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }
}
