using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelNumberText,enemyCountText,scoreText,playerLivesText,homeLivesText;
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

    public void HomeLivesUpdate(int lives)
    {
        homeLivesText.text = "Home : " + lives;
    }

    public void PlayerLivesUpdate(int lives)
    {
        playerLivesText.text = "Lives : " + lives;
    }

    public void PlayerScoreUpdate(int score)
    {
        scoreText.text = "Score : " + score;
    }
}
