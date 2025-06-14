using UnityEngine;

public class PlayerHome : MonoBehaviour
{
    [SerializeField]
    private bool _playerIsHome;
    private int _homeHealth;
    private GameObject player;
    public GameObject ammoShopInstruct;
    void Start()
    {
        _playerIsHome = false;
        _homeHealth = 5;
        ammoShopInstruct.SetActive(false);
        UIManager.Instance.HomeLivesUpdate(_homeHealth);
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (_playerIsHome && Input.GetKeyDown(KeyCode.Tab))
        {
            player.GetComponent<PlayerController>().GunAmmoFillShop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsHome = true;
            ammoShopInstruct.SetActive(true);
            GameManager.Instance.PlayerHomeStatus(_playerIsHome);
            UIManager.Instance.EnemySpawnInstructionStatus(_playerIsHome);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsHome = false;
            ammoShopInstruct.SetActive(false);
            GameManager.Instance.PlayerHomeStatus(_playerIsHome);
            UIManager.Instance.EnemySpawnInstructionStatus(_playerIsHome);
        }
    }

    public void GotHitByBullet()
    {
        _homeHealth--;
        UIManager.Instance.HomeLivesUpdate(_homeHealth);

        if (_homeHealth <= 0)
        {
            Debug.Log("GameOver");
            GameManager.Instance.PlayerHomeDestroyed();
            this.gameObject.SetActive(false);
        }
    }
}
