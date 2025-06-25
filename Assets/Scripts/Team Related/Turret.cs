using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject _enemyTarget;
    private bool _canShootBullet;
    private int rotationSpeed;
    public float angleThreshold = 1f;
    private bool hasLookedAtTarget = false;
    public GameObject bulletSpawnloc;

    void Start()
    {
        rotationSpeed = 5;
        _canShootBullet = true;
    }


    void Update()
    {
        if (_enemyTarget != null)
        {
            EnemyLooking();

            if (_canShootBullet && hasLookedAtTarget)
            {
                ShootTheBullet(_enemyTarget.transform.position);
                _canShootBullet = false;
                StartCoroutine(BulletShootDelay());
            }
        }
    }

    private void ShootTheBullet(Vector3 target)
    {
        var rotation = Quaternion.Euler(90, bulletPrefab.transform.rotation.y, 0);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnloc.transform.position, rotation);
        bullet.transform.LookAt(target);
    }

    IEnumerator BulletShootDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _canShootBullet = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_enemyTarget == null)
            {
                _enemyTarget = other.gameObject;
            }
        }
    }

    private void EnemyLooking()
    {
        Vector3 target = new Vector3(_enemyTarget.transform.position.x, 0 , _enemyTarget.transform.position.z);

        Vector3 direction = target - transform.GetChild(0).position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetRotation, rotationSpeed * Time.deltaTime);
        float angle = Quaternion.Angle(transform.GetChild(0).rotation, targetRotation);
        if (angle < angleThreshold && !hasLookedAtTarget)
        {
            hasLookedAtTarget = true;
        }
    }
}
