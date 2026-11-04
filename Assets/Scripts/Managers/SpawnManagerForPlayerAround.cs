using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerForPlayerAround : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private int _levelDifficulty;
    private bool _isWaveOn;
    [SerializeField]
    private bool _isSpawnOn;
    private float _spawnRange;
    public GameObject[] enemyPrefabs;
    [SerializeField]
    private bool _isWaveTypeLevel;

    void Start()
    {
        _levelDifficulty=0;
        _spawnRange=10;
        _isWaveOn=true;
        _isSpawnOn=true;
        _isWaveTypeLevel=false;
    }

    private void OnEnable()
    {
        inputReader.OnPlayerSpawnState += HandleSpawnState;
    }

    private void OnDisable()
    {
        inputReader.OnPlayerSpawnState -= HandleSpawnState;
    }

    private void HandleSpawnState()
    {
        if(_isSpawnOn)
        {
            SpawnNeedToBePaused();
        }
        else{
            SpawnNeedToBeResumed();
        }
    }

    void Update()
    {
        if(_isSpawnOn && _isWaveTypeLevel)
        {
            if(!_isWaveOn)
            {
                SpawnEnemyWave();
                _isWaveOn=true;
            }
            else{
                AllEnemyClearCheck();
            }
        }

        
    }

    private void SpawnEnemyWave()
    {
        for(int i=0;i<_levelDifficulty*1.3f;i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randSpawnLoc = GenerateRandomSpawnLoc();
        
        int enemyIndex = Random.Range(0,enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyIndex],randSpawnLoc,enemyPrefabs[enemyIndex].transform.rotation);
    }

    private Vector3 GenerateRandomSpawnLoc()
    {

        float xLoc = Random.Range(GetPlayerPosition().x-_spawnRange,GetPlayerPosition().x+_spawnRange);
        float zLoc = Random.Range(GetPlayerPosition().z-_spawnRange,GetPlayerPosition().z+_spawnRange);

        return  new Vector3(xLoc,2,zLoc);
    }


    private Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.Find("Player");

        return player.transform.position;
    }
    private void AllEnemyClearCheck()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length==0)
        {
            _isWaveOn=false;
            _levelDifficulty++;
            UIManager.Instance.LevelNumberChange(_levelDifficulty);
        }
    }

    private void SpawnNeedToBePaused()
    {
        _isSpawnOn=false;
    }

    private void SpawnNeedToBeResumed()
    {
        _isSpawnOn=true;
    }
}
