using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprouter : MonoBehaviour
{
     private GameObject _player;
    [SerializeField]
    private float _enemySpeed;
    [SerializeField]
    private bool _isWalking,_isfollowing,_actionStarted;
    private List<System.Action> _enemyFunctions;
    private Vector3 _targetPos;
    private bool _isAttacking;
    private bool isFirstAction,spawnTimeStarted,spawnTheGroup,isSpawning;
    public GameObject _miniDronePrefab;
    void Start()
    {
        _player=GameObject.Find("Player");
        _actionStarted=false;
        _isWalking=false;
        _isfollowing=false;
        _isAttacking=false;
        isFirstAction=true;
        spawnTimeStarted=false;
        spawnTheGroup=false;
        isSpawning=false;
        _enemyFunctions = new List<System.Action> {
            EnemyWalk,
            EnemyIdle
        };
    }
    void Update()
    {
        _isAttacking=GetComponent<EnemyShooting>().CheckTheAttackMode();

        if(!_isAttacking)
        {
            float distance = Vector3.Distance(transform.position , _player.transform.position);

            if(distance <= 10)
            {
                AttackThePlayer();
                _actionStarted=false;
                _isfollowing=true;
            }
            else{
                float distanceH = Vector3.Distance(transform.position , new Vector3(0,1.14f,0));

                if(distanceH < 17 && GameManager.Instance._isPlayerHomeSafe)
                {
                    _isWalking=false;
                }
                else if(distanceH<5)
                {
                    _isWalking=false;
                }
                else
                {
                    _isWalking=true;
                    _isfollowing=false;
                    if(!_actionStarted)
                    {
                        RandomAction();
                        _actionStarted=true;
                    }
                }
            }

            if(_isWalking && !_isfollowing)
            {
                if(!spawnTimeStarted)
                {
                    isSpawning=false;
                    StartCoroutine(EnemySpawnWait());
                    spawnTimeStarted=true;
                }
                else if(spawnTheGroup)
                {
                    if(!isSpawning && !_isAttacking)
                    {
                        StartCoroutine(EnemySpawningDelay());
                        Instantiate(_miniDronePrefab,transform.position,_miniDronePrefab.transform.rotation);
                        isSpawning=true;
                    }
                    
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, _targetPos,Time.deltaTime*3f);
                    transform.LookAt(_targetPos);
                    
                    if(Vector3.Distance(transform.position,_targetPos) < 1)
                    {
                        _isWalking=false;
                        _actionStarted=false;
                    }
                }
            }
        }
    }

    private void AttackThePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _enemySpeed*Time.deltaTime);
        transform.LookAt(_player.transform);
    }

    private void RandomAction()
    {
        int actionIndex;
        if(isFirstAction)
        {
            actionIndex = 0;
            isFirstAction=false;
        }
        else{
            actionIndex = Random.Range(0,_enemyFunctions.Count);
        }
        _enemyFunctions[actionIndex]();
    }

    private void EnemyWalk()
    {
        float xLoc=0,zLoc=0;

        _targetPos = new Vector3(xLoc,transform.position.y,zLoc);

        _isWalking=true;
    }

    private void EnemyIdle()
    {
        StartCoroutine(EnemyIdleWait());
    }

    IEnumerator EnemyIdleWait()
    {
        yield return new WaitForSeconds(Random.Range(2,3));
        _actionStarted=false;
    }

    IEnumerator EnemySpawnWait()
    {
        yield return new WaitForSeconds(3);
        spawnTheGroup=true;
    }

    IEnumerator EnemySpawningDelay()
    {
        yield return new WaitForSeconds(3);
        spawnTimeStarted=false;
        spawnTheGroup=false;
    }
    public void GotHitByBullet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            GotHitByBullet();
        }
    }
}
