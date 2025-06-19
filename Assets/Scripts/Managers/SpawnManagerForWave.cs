using UnityEngine;

public class SpawnManagerForWave : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefabs;
    public GameObject parentForEnemies;
    private int enemyLvlBuff;

    void OnEnable()
    {
        GameManager.SpawnTheEnemies += SpawnEnemyWave;
    }

    void OnDisable()
    {
        GameManager.SpawnTheEnemies -= SpawnEnemyWave;
    }
    void Start()
    {
        enemyLvlBuff = 1;
    }

    private void SpawnEnemyWave()
    {
        if (GameManager.Instance._level > 9) enemyLvlBuff = 0;
        else enemyLvlBuff = 1;

        
        
        for (int i = 0; i < GameManager.Instance._level; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randSpawnLoc = GenerateRandomSpawnLoc();

        int enemyIndex = Random.Range(0, enemyPrefabs.Length-enemyLvlBuff);

        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], randSpawnLoc, enemyPrefabs[enemyIndex].transform.rotation);

        enemy.transform.SetParent(parentForEnemies.transform);
    }

    private Vector3 GenerateRandomSpawnLoc()
    {

        int ranIndex = Random.Range(0, 4);

        float xLoc = 0, zLoc = 0;

        switch (ranIndex)
        {
            case 0:
                xLoc = Random.Range(50, 150);
                zLoc = Random.Range(50, 150);
                break;
            case 1:
                xLoc = Random.Range(-150, -50);
                zLoc = Random.Range(50, 150);
                break;
            case 2:
                xLoc = Random.Range(50, 150);
                zLoc = Random.Range(-150, -50);
                break;
            case 3:
                xLoc = Random.Range(-150, -50);
                zLoc = Random.Range(-150, -50);
                break;
        }

        return new Vector3(xLoc, 1.14f, zLoc);
    }
}
