using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelNumberText, enemyCountText, scoreText, enemyInstruct;
    public GameObject pauseMenu;
    private bool isWaveStarted;
    public Slider lifeBar, homeHealthBar;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        pauseMenu.SetActive(false);
        enemyInstruct.gameObject.SetActive(false);
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0) isWaveStarted = false;

        enemyCountText.text = "Enemy : " + enemyCount;
    }

    public void LevelNumberChange(int level)
    {
        levelNumberText.text = "" + level;
    }

    public void HomeLivesUpdate(float lives)
    {
        homeHealthBar.value = lives / 5;
    }

    public void PlayerLivesUpdate(float lives)
    {
        lifeBar.value = lives / 5;
    }

    public void PlayerScoreUpdate(int score)
    {
        scoreText.text = "" + score;
    }

    public void EnemySpawnInstructionStatus(bool status)
    {
        if (!isWaveStarted) { enemyInstruct.gameObject.SetActive(status); }
    }

    public void WaveStatus(bool status)
    {
        isWaveStarted = status;
        if (isWaveStarted) { enemyInstruct.gameObject.SetActive(false); }
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
