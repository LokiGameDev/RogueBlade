using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerAttacker : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float _enemySpeed;
    [SerializeField]
    private bool _actionStarted;
    private Vector3 _targetPos;
    private bool _isWalking,_isAttacking;
    private List<System.Action> _enemyFunctions;
    void Start()
    {
        _player=GameObject.Find("Player");
        _actionStarted=false;
        _isWalking=false;
        _isAttacking=false;
        _enemyFunctions = new List<System.Action> {
            EnemyWalk,
            EnemyIdle
        };
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position , _player.transform.position);
        if(distance <= 10)
        {
            AttackThePlayer();
            _actionStarted=false;
            _isAttacking=true;
        }
        else{
            _isAttacking=false;
            if(!_actionStarted)
            {
                RandomAction();
                _actionStarted=true;
            }
        }

        if(_isWalking && !_isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos,Time.deltaTime*3f);

            if(Vector3.Distance(transform.position,_targetPos) < 1)
            {
                _isWalking=false;
                _actionStarted=false;
            }
        }
    }

    private void AttackThePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _enemySpeed*Time.deltaTime);
    }

    private void RandomAction()
    {
        int actionIndex = Random.Range(0,_enemyFunctions.Count);
        _enemyFunctions[actionIndex]();
    }

    private void EnemyWalk()
    {
        int ranIndex = Random.Range(0,4);

        float xLoc=0,zLoc=0;

        switch (ranIndex)
        {
            case 0:
                xLoc = Random.Range(5,10);
                zLoc = Random.Range(5,10);
                break;
            case 1:
                xLoc = Random.Range(-10,-5);
                zLoc = Random.Range(5,10);
                break;
            case 2:
                xLoc = Random.Range(5,10);
                zLoc = Random.Range(-10,-5);
                break;
            case 3:
                xLoc = Random.Range(-10,-5);
                zLoc = Random.Range(-10,-5);
                break;
        }

        _targetPos = new Vector3(transform.position.x+xLoc,transform.position.y,transform.position.z+zLoc);

        _isWalking=true;

    }

    private void EnemyIdle()
    {
        StartCoroutine(EnemyIdleWait());
    }

    IEnumerator EnemyIdleWait()
    {
        yield return new WaitForSeconds(Random.Range(3,5));
        _actionStarted=false;
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
