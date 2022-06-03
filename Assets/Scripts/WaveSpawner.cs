using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
        public enum SpawnState { Spawning, Waiting, Counting, Reward}
        
        [System.Serializable]
        public class Wave
        {
            public string name;
            public Transform[] enemies;
           // public int count; //number of enemies
            public float delay; //rate in which enemies spawn
        }

        public Wave[] waves;
        private int nextWave = 0;
        private int enemyNumber = 0;

        public Transform[] spawnPoints;

    public Transform enemySpawned;
        public Bounds boundBox; //Added. For BoundBox for WanderState and EscapeState to be Precise. Seng
        
        public Transform[] rewardsList;
    
        public Transform[] rewardSpawnPoints;
        public int randNum; 

        public float timeBetweenWaves = 5f;
        public float waveCountdown;

        private SpawnState state = SpawnState.Counting;

        private bool isAlive = true;
        private bool beginWaves = false;

        void Start()
        {
            waveCountdown = timeBetweenWaves;
        }

        void Update()
        {
        if (beginWaves == true)
            {
                if (state == SpawnState.Waiting)
                {
                    if (isAlive == false)
                    {
                        BeginNewWave();
                    }
                    else
                    {
                        return;
                    }
                }

                if (waveCountdown <= 0)
                {
                    if (state == SpawnState.Reward)
                    {
                        return;
                    }
                    else if (state != SpawnState.Spawning)
                    {
                        StartCoroutine(SpawnWave(waves[nextWave]));
                    }
                }
                else
                {
                    waveCountdown -= Time.deltaTime;
                }
            }
        }
        public void EnemyCount(int count)
        {
            enemyNumber += count;

            if (enemyNumber <= 0)
            {
                EnemyIsAlive();
            }
        }

        void EnemyIsAlive()
        {
            isAlive = false;
        }

        void BeginNewWave()
        {
            Debug.Log("Wave Completed");

            state = SpawnState.Counting;
            waveCountdown = timeBetweenWaves;
            isAlive = true;

            if(nextWave + 1 > waves.Length - 1)
            {
                state = SpawnState.Reward;
                Debug.Log("All waves complete! Collect your rewards!");
                
                SpawnRewards();
 
            }
            else
            {
                nextWave++;
            }
        }

        IEnumerator SpawnWave(Wave _wave)
        {
            Debug.Log("Spawning Wave: " + _wave.name);
            state = SpawnState.Spawning;

            for (int i = 0; i < _wave.enemies.Length; i++) 
            {
                SpawnEnemy(_wave.enemies[i]);
                EnemyCount(1);
                yield return new WaitForSeconds(_wave.delay);
            }

            state = SpawnState.Waiting;

            yield break;
        }

        void SpawnEnemy (Transform _enemy)
        {

            Debug.Log("Spawning Enemy: " + _enemy.name);
            
            if(spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points referenced");
            }

            randNum = Random.Range(0, spawnPoints.Length);
            Transform _sp = spawnPoints[randNum];
            enemySpawned = Instantiate(_enemy, _sp.position, _sp.rotation); 
            enemySpawned.gameObject.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox); //Added. To send BoundBox of Spawner to Enemy. Seng
            enemySpawned.gameObject.GetComponent<EnemyMelee>().waveSpawnerObject = gameObject; //Added by Seng
            Debug.Log("Sent BoundBox");
        }

        void SpawnRewards ()
        {

            if (rewardSpawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points referenced");
            }
            else
            {
                for (int i = 0; i < rewardsList.Length; i++)
                {
                    Transform _sp = rewardSpawnPoints[i];
                    Instantiate(rewardsList[i], _sp.position, _sp.rotation);
                }
                
            }
        }

    private void OnTriggerEnter(Collider other)
    {
        beginWaves = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }
}
