using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        playerLives = 2;
        isPlayerAlive=true;
        _canPlayerDash=true;
        _playerRigid = GetComponent<Rigidbody>();

        UIManager.Instance.PlayerScoreUpdate(playerScore);
        UIManager.Instance.PlayerLivesUpdate(playerLives);
    }

    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.LeftShift) && _canPlayerDash)
        {
            _canPlayerDash=false;
            _playerRigid.velocity = playerAim.transform.forward * 40;
            StartCoroutine(PlayerDashDelay());
            Debug.Log("Dash");
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * _playerSpeed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _playerSpeed);
    }

    public void AddScore(int score)
    {
        playerScore+=score;
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }

    public void GotHitByBullet()
    {
        playerLives--;
        UIManager.Instance.PlayerLivesUpdate(playerLives);

        if(playerLives<1)
        {
            Debug.Log("Player died");
            isPlayerAlive=false;
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
        _canPlayerDash=true;
    }
}
