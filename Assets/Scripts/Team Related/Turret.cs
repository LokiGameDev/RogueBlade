using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject _enemyTarget;
    private bool _canShootBullet;
    void Start()
    {
        _canShootBullet=true;
    }


    void Update()
    {
        if(_enemyTarget!=null)
        {
            transform.GetChild(0).LookAt(_enemyTarget.transform.position);
            
            if(_canShootBullet)
            {
                ShootTheBullet(_enemyTarget.transform.position);
                _canShootBullet=false;
                StartCoroutine(BulletShootDelay());
            }
        }
    }

    private void ShootTheBullet(Vector3 target)
    {
        var rotation = Quaternion.Euler(90,bulletPrefab.transform.rotation.y,0);
        GameObject bullet = Instantiate(bulletPrefab,transform.position,rotation);
        bullet.transform.LookAt(target);
    }

    IEnumerator BulletShootDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _canShootBullet=true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(_enemyTarget==null)
            {
                _enemyTarget=other.gameObject;
            }
        }
    }
}
