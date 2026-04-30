using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private TMP_Text waveText;

    [SerializeField] private AudioSource waveAudio;
    [SerializeField] private AudioClip waveStartClip;

    private int currentWave = 1;
    private int enemiesAlive;

    private Transform selectedSpawnPoint;


    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (enemiesAlive <= 0)
        {
            currentWave++;
            StartWave();
        }
    }

    private void StartWave()
    {
        FindFirstObjectByType<UIManager>().UpdateWave(currentWave);
        FindFirstObjectByType<UIManager>().ShowWaveText("Wave " + currentWave);

        enemiesAlive = 0;

        if (waveAudio != null && waveStartClip != null && currentWave != 1)
        {
            waveAudio.PlayOneShot(waveStartClip);
        }

        if (currentWave == 1)
        {
            SpawnBasicEnemies(1);
            SpawnFastEnemies(1);
        }
        else if (currentWave == 2)
        {
            SpawnBasicEnemies(5);
        } 
        else 
        {
            SpawnBasicEnemies(5);
            SpawnFastEnemies(2);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        /*
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            enemyPrefab,
            spawnPoints[randomIndex].position,
            Quaternion.identity
        );

        enemiesAlive++;
        */

        selectedSpawnPoint = null;

        while (selectedSpawnPoint == null)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform potentialSpawn = spawnPoints[randomIndex];

            float distanceToPlayer = Vector2.Distance(
                potentialSpawn.position,
                GameObject.FindGameObjectWithTag("Player").transform.position
            );

            if (distanceToPlayer > 3f)
            {
                selectedSpawnPoint = potentialSpawn;
            }
        }

        Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        enemiesAlive++;
    }

    private void SpawnBasicEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(basicEnemyPrefab);
        }
    }

    private void SpawnFastEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(fastEnemyPrefab);
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
}
