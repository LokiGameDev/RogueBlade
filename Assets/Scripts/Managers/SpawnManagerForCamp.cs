using UnityEngine;

public class SpawnManagerForCamp : MonoBehaviour
{
    public GameObject[] campSpawnLocations;
    public GameObject parentForEnemyCamps;
    public GameObject enemySpawnCamp;

    void OnEnable()
    {
        GameManager.SpawnTheEnemies+=SpawnEnemyCampWave;
    }

    void OnDisable()
    {
        GameManager.SpawnTheEnemies-=SpawnEnemyCampWave;
    }
    void Start()
    {
        
    }

    void Update()
    {
       
    }

    void SpawnEnemyCampWave()
    {
        int spawnCount = GameManager.Instance._level / 2;
        if (spawnCount < 1) spawnCount = 1;
        for (int i = 0; i < spawnCount; i++)
        {
            int randCampLoc = Random.Range(0, campSpawnLocations.Length - 1);
            GameObject camp = Instantiate(enemySpawnCamp, campSpawnLocations[randCampLoc].transform.position, enemySpawnCamp.transform.rotation);
            camp.transform.SetParent(parentForEnemyCamps.transform);
        }
    }
}
