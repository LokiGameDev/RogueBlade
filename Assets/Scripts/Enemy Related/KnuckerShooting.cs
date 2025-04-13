using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private bool _canShootBullet,_isAttackingMode;
    void Start()
    {
        _canShootBullet=true;
        _isAttackingMode=false;
    }

    void Update()
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
