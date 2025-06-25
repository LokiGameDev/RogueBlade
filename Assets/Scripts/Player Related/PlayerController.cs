using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;
    public int playerScore
    {
        get; private set;
    }
    public int playerLives
    {
        get; private set;
    }

    public bool isPlayerAlive
    {
        get; private set;
    }
    private bool _canPlayerDash;
    private Rigidbody _playerRigid;
    public GameObject playerAim;
    public Slider dashSlider;
    public GameObject infoAbovePlayer;
    private float dashStartTime;
    private int BOUNDARY = 200;
    private int _maxAmmo;
    public Animator playerAnimator;

    void Start()
    {
        isPlayerAlive = true;
        _canPlayerDash = true;
        _playerSpeed = 5;
        _playerRigid = GetComponent<Rigidbody>();
        infoAbovePlayer.gameObject.SetActive(false);

        UIManager.Instance.PlayerScoreUpdate(playerScore);
        UIManager.Instance.PlayerLivesUpdate(playerLives);
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canPlayerDash)
        {
            _canPlayerDash = false;
            GameManager.Instance.PlayAudioClip(0);
            dashStartTime = Time.time;
            _playerRigid.velocity = playerAim.transform.forward * _playerSpeed * 8;
            playerAnimator.SetTrigger("Dash");
            StartCoroutine(PlayerDashDelay());
        }
        if (!_canPlayerDash)
        {
            dashSlider.value = (Time.time - dashStartTime) / 1.5f;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerAnimator.SetBool("Running", false);
        }
        else
        {
            playerAnimator.SetBool("Running", true);
        }
        if (transform.position.x > BOUNDARY) horizontalInput = horizontalInput > 0 ? 0 : horizontalInput;
        if (transform.position.x < -BOUNDARY) horizontalInput = horizontalInput < 0 ? 0 : horizontalInput;
        if (transform.position.z > BOUNDARY) verticalInput = verticalInput > 0 ? 0 : verticalInput;
        if (transform.position.z < -BOUNDARY) verticalInput = verticalInput < 0 ? 0 : verticalInput;

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _playerSpeed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * _playerSpeed);
    }

    public void AddScore(int score)
    {
        playerScore += score;
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }

    public void ReduceScore(int score)
    {
        playerScore -= score;
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }

    public void GotHitByBullet()
    {
        Debug.Log("Geting hit" + playerLives);
        playerLives--;
        UIManager.Instance.PlayerLivesUpdate(playerLives);

        if (playerLives < 1)
        {
            GameManager.Instance.GameOver();
            isPlayerAlive = false;
        }
    }

    public void PlayerHealLives(int lives)
    {
        playerLives += lives;
        UIManager.Instance.PlayerLivesUpdate(playerLives);
    }

    IEnumerator PlayerDashDelay()
    {
        yield return new WaitForSeconds(0.15f);
        _playerRigid.velocity = transform.forward * 0;
        yield return new WaitForSeconds(1.5f);
        _canPlayerDash = true;
    }

    public void GunAmmoFillShop()
    {
        if (playerScore >= 3 && this.GetComponentInChildren<PlayerAttack>()._gunAmmo < _maxAmmo)
        {
            playerScore -= 3;
            UIManager.Instance.PlayerScoreUpdate(playerScore);
            this.GetComponentInChildren<PlayerAttack>().GunAmmoRefill(1);
        }
        else if (this.GetComponentInChildren<PlayerAttack>()._gunAmmo >= _maxAmmo)
        {
            infoAbovePlayer.SetActive(true);
            infoAbovePlayer.GetComponentInChildren<TextMeshProUGUI>().text = "Ammo is full!";
        }
        else
        {
            InsufficientMoneyInfo();
        }
    }

    public void HomeHealthRefill()
    {
        if (playerScore >= 5)
        {
            GameManager.Instance.RefillHomeHealth();
            playerScore -= 5;
            UIManager.Instance.PlayerScoreUpdate(playerScore);
        }
        else
        {
            InsufficientMoneyInfo();
        }
    }

    public void InsufficientAmmoInfo()
    {
        infoAbovePlayer.SetActive(true);
        infoAbovePlayer.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough Ammo";
    }

    public void MaxAmmoCountChange(int count)
    {
        _maxAmmo = count;
    }

    public void InsufficientMoneyInfo()
    {
        infoAbovePlayer.SetActive(true);
        infoAbovePlayer.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough Money";
    }

    public void InitializePlayerData(int value, int score)
    {
        playerLives = value;
        playerScore = score;
        UIManager.Instance.PlayerLivesUpdate(playerLives);
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }
}
