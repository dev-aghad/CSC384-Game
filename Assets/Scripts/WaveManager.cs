using System.Threading;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text basicEnemyText;
    [SerializeField] private TMP_Text fastEnemyText;

    [SerializeField] private AudioSource waveAudio;
    [SerializeField] private AudioClip waveStartClip;

    private int currentWave = 1;
    private int enemiesAlive;
    private int basicEnemiesAlive;
    private int fastEnemiesAlive;

    private Transform selectedSpawnPoint;
    private int lastSpawnIndex = -1;


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

        enemiesAlive = 0;

        if (waveAudio != null && waveStartClip != null && currentWave != 1)
        {
            waveAudio.PlayOneShot(waveStartClip);
        }

        string nextWaveInfo = "";

        if (currentWave == 1)
        {
            SpawnBasicEnemies(1);
            nextWaveInfo = "Next: 3 Basic";
        }
        else if (currentWave == 2)
        {
            SpawnBasicEnemies(3);
            nextWaveInfo = "Next: 3 Basic, 2 Fast";
        } 
        else 
        {
            SpawnBasicEnemies(3);
            SpawnFastEnemies(2);
            nextWaveInfo = "Next: 3 Basic, 2 Fast";
        }

        string waveDisplay = "Wave " + currentWave + "\n" + nextWaveInfo;
        FindFirstObjectByType<UIManager>().ShowWaveText(waveDisplay);

        UpdateUI();
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
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            }
            while (randomIndex == lastSpawnIndex && spawnPoints.Length > 1);

            lastSpawnIndex = randomIndex;

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

        // Had to convert Vector2 to Vector3 to use with Instantiate
        Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            Quaternion.identity
        );

        enemiesAlive++;
    }

    private void SpawnBasicEnemies(int amount)
    {
        basicEnemiesAlive = amount;

        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(basicEnemyPrefab);
        }
    }

    private void SpawnFastEnemies(int amount)
    {
        fastEnemiesAlive = amount;

        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(fastEnemyPrefab);
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }

    public void BasicEnemyDied()
    {
        basicEnemiesAlive--;
        UpdateUI();
    }

    public void FastEnemyDied()
    {
        fastEnemiesAlive--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (basicEnemyText != null)
        {
            basicEnemyText.text = basicEnemiesAlive.ToString();
        }

        if (fastEnemyText != null)
        {
            fastEnemyText.text = fastEnemiesAlive.ToString();
        }
    }
}
