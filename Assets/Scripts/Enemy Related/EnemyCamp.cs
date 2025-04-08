using System.Collections;
using UnityEngine;

public class EnemyCamp : MonoBehaviour
{
    private int _enemySpawnCount;
    public GameObject enemyPrefab;
    private Vector3 _spawnPos;
    private int _enemyInCamp;
    private bool _canSpawnEnemy;
    private Animator _campAnim;
    void Start()
    {
        _spawnPos = transform.position;
        _enemyInCamp = 0;
        _enemySpawnCount = 5;
        _canSpawnEnemy=false;
        _campAnim = GetComponent<Animator>();
        SpawnTheEnemyGroup();
    }

    void Update()
    {
        if(_enemyInCamp==0 && _canSpawnEnemy)
        {
            StartCoroutine(SpawnTheEnemyGroupDelay());
            _canSpawnEnemy=false;
        }
    }

    private void SpawnTheEnemyGroup()
    {
        _campAnim.SetTrigger("Open");
        StartCoroutine(SpawnEnemyInBetweenDelay(_enemySpawnCount));
    }

    private void SpawnEnemyOneByOne(int count)
    {
        count--;

        Vector3 ranSpawnLoc = transform.position + new Vector3(0,1.5f,0);

        GameObject enemy = Instantiate(enemyPrefab,ranSpawnLoc,enemyPrefab.transform.rotation);

        enemy.GetComponent<EnemyWithBound>().SetSpawnLocForEnemy(transform.position, GetComponent<EnemyCamp>());

        _enemyInCamp++;

        if(count<=0)
        {
            _canSpawnEnemy=true;
            _campAnim.SetTrigger("Close");
        }
        else{
            StartCoroutine(SpawnEnemyInBetweenDelay(count));
        }
    }

    IEnumerator SpawnEnemyInBetweenDelay(int count)
    {
        yield return new WaitForSeconds(2);
        SpawnEnemyOneByOne(count);
    }
    private Vector3 GenerateRandomSpawnLoc()
    {
        float xLoc = Random.Range(-10,10);
        float zLoc = Random.Range(-10,10);

        Vector3 randomLoc = new Vector3(xLoc,transform.position.y,zLoc) + transform.position;

        return randomLoc;
    }

    public void EnemyGotKilled()
    {
        _enemyInCamp--;
    }

    IEnumerator SpawnTheEnemyGroupDelay()
    {
        yield return new WaitForSeconds(5);
        SpawnTheEnemyGroup();
    }
}
