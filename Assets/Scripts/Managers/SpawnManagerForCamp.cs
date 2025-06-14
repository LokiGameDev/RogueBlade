using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerForCamp : MonoBehaviour
{
    public GameObject[] campSpawnLocations;
    public GameObject parentForEnemyCamps;
    public GameObject enemySpawnCamp;

    void OnEnable()
    {
        GameManager.SpawnTheEnemies += SpawnEnemyCampWave;
    }

    void OnDisable()
    {
        GameManager.SpawnTheEnemies -= SpawnEnemyCampWave;
    }
    void Start()
    {

    }

    void SpawnEnemyCampWave()
    {
        int spawnCount = GameManager.Instance._level / 2;
        if (spawnCount < 1) spawnCount = 1;
        List<int> spawnLoc = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        for (int i = 0; i < spawnCount; i++)
        {
            int randCampLoc = spawnLoc[Random.Range(0, spawnLoc.Count - 1)];
            spawnLoc.Remove(randCampLoc);
            GameObject camp = Instantiate(enemySpawnCamp, campSpawnLocations[randCampLoc].transform.position, enemySpawnCamp.transform.rotation);
            camp.transform.SetParent(parentForEnemyCamps.transform);
        }
    }
}
