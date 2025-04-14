using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelNumberText,enemyCountText,scoreText,enemyInstruct;
    public Slider lifeBar,homeHealthBar;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get{
            if(_instance==null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance=this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        enemyCountText.text = "Enemy : "+enemyCount;
    }

    public void LevelNumberChange(int level)
    {
        levelNumberText.text = "Level : "+ level;
    }

    public void HomeLivesUpdate(float lives)
    {
        homeHealthBar.value = lives/5;
    }

    public void PlayerLivesUpdate(float lives)
    {
        lifeBar.value = lives/5;
    }

    public void PlayerScoreUpdate(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void EnemySpawnInstructionStatus(bool status)
    {
        enemyInstruct.gameObject.SetActive(status);
    }
}
