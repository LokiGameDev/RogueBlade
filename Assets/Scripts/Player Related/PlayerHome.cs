using UnityEngine;

public class PlayerHome : MonoBehaviour
{
    [SerializeField]
    private bool _playerIsHome;
    public int _homeHealth
    {
        get; private set;
    }
    private GameObject player;
    public GameObject ammoShopInstruct;
    void Start()
    {
        _playerIsHome = false;
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
            GameManager.Instance.PlayerHomeDestroyed();
            this.gameObject.SetActive(false);
        }
    }

    public void HomeHealthIncrease(int val)
    {
        _homeHealth += val;
        UIManager.Instance.HomeLivesUpdate(_homeHealth);
    }

    public void InitializeHomeHealth(int value)
    {
        _homeHealth = value;
        UIManager.Instance.HomeLivesUpdate(_homeHealth);
    }
}
