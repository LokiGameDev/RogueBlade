using UnityEngine;

public class SpawnManagerForWave : MonoBehaviour
{
    private int _levelDifficulty;
    private bool _isWaveOn;
    [SerializeField]
    private bool _isSpawnOn;
    public GameObject[] enemyPrefabs;

    void Start()
    {
        _levelDifficulty=0;
        _isWaveOn=true;
        _isSpawnOn=false;
    }

    void Update()
    {
        if(_isSpawnOn)
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

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(_isSpawnOn)
            {
                SpawnNeedToBePaused();
            }
            else{
                SpawnNeedToBeResumed();
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

        int ranIndex = Random.Range(0,4);

        float xLoc=0,zLoc=0;

        switch (ranIndex)
        {
            case 0:
                xLoc = Random.Range(50,150);
                zLoc = Random.Range(50,150);
                break;
            case 1:
                xLoc = Random.Range(-150,-50);
                zLoc = Random.Range(50,150);
                break;
            case 2:
                xLoc = Random.Range(50,150);
                zLoc = Random.Range(-150,-50);
                break;
            case 3:
                xLoc = Random.Range(-150,-50);
                zLoc = Random.Range(-150,-50);
                break;
        }

        return  new Vector3(xLoc,1.14f,zLoc);
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
