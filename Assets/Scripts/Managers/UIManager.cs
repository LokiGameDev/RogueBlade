using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelNumberText, enemyCountText, scoreText, enemyInstruct;
    public GameObject pauseMenu, gameOverMenu, gameWonMenu, settingsMenu;
    private bool isWaveStarted;
    public Slider lifeBar, homeHealthBar;
    public bool isPanelOpen{ get; private set; }
    private float elapsedTime;
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
        isPanelOpen = false;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        enemyInstruct.gameObject.SetActive(false);
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0) isWaveStarted = false;

        enemyCountText.text = "" + enemyCount;

        if (GameManager.Instance._playerIsHome && !isWaveStarted)
        {
            enemyInstruct.gameObject.SetActive(true);
        }
    }

    public void PanelOpenStatus(bool status)
    {
        isPanelOpen=status;
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

    public void PlayerScoreUpdate(int currScore, int score)
    {
        elapsedTime = 0;
        StartCoroutine(AnimateNumber(currScore, score));
    }

    private IEnumerator AnimateNumber(int startValue,int endValue)
    {
        while (elapsedTime < 0.4f)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / 0.4f);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));
            scoreText.text = currentValue.ToString();
            yield return null;
        }
        scoreText.text = endValue.ToString();
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

    public void SaveGame()
    {
        if (!isWaveStarted)
        {
            GameManager.Instance.SaveGame();
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameWon()
    {
        gameWonMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        isPanelOpen = true;
        Time.timeScale = 0;
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void SettingsBack()
    {
        settingsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPanelOpen = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isPanelOpen = false;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        isPanelOpen = false;
        SceneManager.LoadScene(0);
    }
}
