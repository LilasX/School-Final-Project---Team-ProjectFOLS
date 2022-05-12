using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
        public enum SpawnState { Spawning, Waiting, Counting}

        [System.Serializable]
        public class Wave
        {
            public string name;
            public Transform enemy;
            public int count; //number of enemies
            public float delay; //rate in which enemies spawn
        }

        public Wave[] waves;
        private int nextWave = 0;
        private int enemyNumber = 0;

        public float timeBetweenWaves = 5f;
        public float waveCountdown;

        private SpawnState state = SpawnState.Counting;

        private bool isAlive = true;

        void Start()
        {
            waveCountdown = timeBetweenWaves;
        }

        void Update()
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
                if (state != SpawnState.Spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
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
                nextWave = 0;

                Debug.Log("All waves complete! Looping");
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

            for (int i = 0; i < _wave.count; i++) 
            {
                SpawnEnemy(_wave.enemy);
                EnemyCount(1);
                yield return new WaitForSeconds(_wave.delay);
            }

            state = SpawnState.Waiting;

            yield break;
        }

        void SpawnEnemy (Transform _enemy)
        {
            Debug.Log("Spawning Enemy: " + _enemy.name);
            Instantiate(_enemy, transform.position, transform.rotation); 
        }
    

}
