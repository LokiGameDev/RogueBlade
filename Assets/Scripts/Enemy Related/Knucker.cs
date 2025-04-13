using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knucker : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed;
    [SerializeField]
    private bool _isWalking,_isfollowing,_actionStarted;
    private List<System.Action> _enemyFunctions;
    private Vector3 _targetPos;
    private bool _isAttacking;
    private bool isFirstAction;
    void Start()
    {
        _actionStarted=false;
        _isWalking=false;
        _isfollowing=false;
        _isAttacking=false;
        isFirstAction=true;
        _enemyFunctions = new List<System.Action> {
            EnemyWalk,
        };
    }
    void Update()
    {
        _isAttacking=GetComponent<KnuckerShooting>().CheckTheAttackMode();

        if(!_isAttacking)
        {
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

            if(_isWalking && !_isfollowing)
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
}
