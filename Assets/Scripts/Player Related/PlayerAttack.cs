using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab, playerSword, playerBody;
    private bool _canFireBullet, _canSwingSword;
    public int _gunAmmo;
    private Camera maincam;
    private Vector3 mousePos;
    private float rotY;
    public TextMeshProUGUI ammoText;
    private int _maxAmmo;
    public Animator playerAnimator;
    void Start()
    {
        _maxAmmo = 15;
        MaxAmmoCountChange(_maxAmmo);
        _gunAmmo = 0;
        _canFireBullet = true;
        _canSwingSword = true;
        playerSword.SetActive(false);
        maincam = GameObject.Find("PlayerAttackCam").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        rotY = Mathf.Atan2(rotation.x, rotation.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        playerBody.transform.rotation = Quaternion.Euler(0, rotY, 0);

        if (Input.GetMouseButtonDown(1))
        {
            PlayerShoot();
            playerAnimator.SetTrigger("Attack");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            PlayerSwordAttack();
            playerAnimator.SetTrigger("Attack");
        }

        ammoText.text = "" + _gunAmmo;

        if (Input.GetKeyDown(KeyCode.L))
        {
            MaxAmmoCountChange(_maxAmmo + 1);
        }

    }

    private void PlayerShoot()
    {
        if (_canFireBullet && _gunAmmo > 0)
        {
            Vector3 bulletSpawnLoc = transform.GetChild(0).gameObject.transform.position;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLoc, bulletPrefab.transform.rotation);

            bullet.transform.rotation = Quaternion.Euler(90, rotY, 0);

            _canFireBullet = false;

            _gunAmmo--;

            StartCoroutine(BulletFireCooldown());
        }
        else if (_canFireBullet && _gunAmmo <= 0)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().InsufficientAmmoInfo();
        }
    }

    private void PlayerSwordAttack()
    {
        if (_canSwingSword)
        {

            playerSword.SetActive(true);

            _canSwingSword = false;

            StartCoroutine(SwordSwingCooldown());
        }
    }

    IEnumerator BulletFireCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _canFireBullet = true;
    }

    IEnumerator SwordSwingCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        playerSword.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        _canSwingSword = true;
    }

    public void GunAmmoRefill(int amount)
    {
        _gunAmmo += amount;
    }

    private void MaxAmmoCountChange(int count)
    {
        _maxAmmo = count;
        GameObject.Find("Player").GetComponent<PlayerController>().MaxAmmoCountChange(_maxAmmo);
    }
}
