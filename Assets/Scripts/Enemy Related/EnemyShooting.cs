using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject _player;
    private bool _canShootBullet,_isAttackingMode;
    void Start()
    {
        _canShootBullet=true;
        _isAttackingMode=false;
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if(distance<8)
        {
            _isAttackingMode=true;
            transform.LookAt(_player.transform);
            if(_canShootBullet)
            {
                _canShootBullet=false;
                StartCoroutine(EnemyShootCoolDown());
                ShootTheBullet(_player.transform.position);
            }
        }
        else
        {
            float distanceH = Vector3.Distance(transform.position, new Vector3(0,1.14f,0));

            if(distanceH<17 && GameManager.Instance._isPlayerHomeSafe)
            {
                _isAttackingMode=true;
                transform.LookAt(new Vector3(0,1.14f,0));
                if(_canShootBullet)
                {
                    _canShootBullet=false;
                    StartCoroutine(EnemyShootCoolDown());
                    ShootTheBullet(new Vector3(0,1.14f,0));
                }
            }
            else if(distanceH<5)
            {
                _isAttackingMode=true;
                transform.LookAt(new Vector3(0,1.14f,0));
                if(_canShootBullet)
                {
                    _canShootBullet=false;
                    StartCoroutine(EnemyShootCoolDown());
                    ShootTheBullet(new Vector3(0,1.14f,0));
                }
            }
            else
            {
                _isAttackingMode=false;
            }
        }
    }

    private void ShootTheBullet(Vector3 target)
    {
        var rotation = Quaternion.Euler(90,bulletPrefab.transform.rotation.y,0);
        GameObject bullet = Instantiate(bulletPrefab,transform.position,rotation);
        bullet.transform.LookAt(target);
    }

    public bool CheckTheAttackMode()
    {
        return _isAttackingMode;
    }

    IEnumerator EnemyShootCoolDown()
    {
        yield return new WaitForSeconds(2f);
        _canShootBullet=true;
    }
}
