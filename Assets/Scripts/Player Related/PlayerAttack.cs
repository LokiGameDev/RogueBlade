using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab,playerSword;
    private bool _canFireBullet,_canSwingSword;
    private Camera maincam;
    private Vector3 mousePos;
    private float rotY;
    void Start()
    {
        _canFireBullet=true;
        _canSwingSword=true;
        playerSword.SetActive(false);
        maincam = GameObject.Find("PlayerAttackCam").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        rotY = Mathf.Atan2(rotation.x,rotation.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,rotY,0);

        if(Input.GetMouseButtonDown(0))
        {
            PlayerShoot();
        }
        else if(Input.GetMouseButtonDown(1)){
            PlayerSwordAttack();
        }
        
    }

    private void PlayerShoot()
    {
        if(_canFireBullet)
        {
            Vector3 bulletSpawnLoc = transform.GetChild(0).gameObject.transform.position;

            GameObject bullet = Instantiate(bulletPrefab,bulletSpawnLoc,bulletPrefab.transform.rotation);

            bullet.transform.rotation = Quaternion.Euler(90,rotY,0);

            _canFireBullet=false;

            StartCoroutine(BulletFireCooldown());
        }
    }

    private void PlayerSwordAttack()
    {
        if(_canSwingSword)
        {

            playerSword.SetActive(true);

            _canSwingSword=false;

            StartCoroutine(SwordSwingCooldown());
        }
    }

    IEnumerator BulletFireCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _canFireBullet=true;
    }

    IEnumerator SwordSwingCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        playerSword.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        _canSwingSword=true;
    }
}
