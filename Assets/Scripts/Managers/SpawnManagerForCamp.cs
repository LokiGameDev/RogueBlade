using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerForCamp : MonoBehaviour
{
    public GameObject[] campSpawnLocations;
    private int _waveLevel;
    public GameObject enemySpawnCamp;
    private bool _canSpawnCampWave;
    void Start()
    {
        _waveLevel=1;
        _canSpawnCampWave=true;
    }

    void Update()
    {
        isSpawnCampsCleared();
        if(Input.GetKeyDown(KeyCode.E) && GameManager.Instance._playerIsHome)
        {
            TriggerForEnemyCampWaveSpawn();
        }
    }

    void SpawnEnemyCampWave()
    {
        for(int i=0;i<_waveLevel;i++)
        {
            int randCampLoc = Random.Range(0,campSpawnLocations.Length-1);
            Instantiate(enemySpawnCamp,campSpawnLocations[randCampLoc].transform.position,enemySpawnCamp.transform.rotation);
        }
    }

    void isSpawnCampsCleared()
    {
        bool check = GameObject.FindGameObjectsWithTag("EnemyCamp").Length==0;
        if(check)
        {
            _canSpawnCampWave=true;
        }
    }

    private void TriggerForEnemyCampWaveSpawn()
    {
        if(_canSpawnCampWave)
        {
            SpawnEnemyCampWave();
            _canSpawnCampWave=false;
            _waveLevel++;
        }
    }
}
