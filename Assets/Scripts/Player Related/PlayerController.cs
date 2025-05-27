using System.Collections;
using System.Collections.Generic;
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
    private float dashStartTime;
    private int BOUNDARY = 200;

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canPlayerDash)
        {
            _canPlayerDash = false;
            dashStartTime = Time.time;
            _playerRigid.velocity = playerAim.transform.forward * 40;
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
        if (transform.position.x > BOUNDARY) horizontalInput = horizontalInput > 0 ? 0 : horizontalInput;
        if (transform.position.x < -BOUNDARY) horizontalInput = horizontalInput < 0 ? 0 : horizontalInput;
        if (transform.position.z > BOUNDARY) verticalInput = verticalInput > 0 ? 0 : verticalInput;
        if (transform.position.z < -BOUNDARY) verticalInput = verticalInput < 0 ? 0 : verticalInput;
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * _playerSpeed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * _playerSpeed);
    }

    public void AddScore(int score)
    {
        playerScore+=score;
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }

    public void ReduceScore(int score)
    {
        playerScore -= score;
        UIManager.Instance.PlayerScoreUpdate(playerScore);
    }

    public void GotHitByBullet()
    {
        playerLives--;
        UIManager.Instance.PlayerLivesUpdate(playerLives);

        if (playerLives < 1)
        {
            Debug.Log("Player died");
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
        _canPlayerDash=true;
    }
}
