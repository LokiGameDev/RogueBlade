using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    public bool _playerIsHome
    {
        get;private set;
    }
    public bool _isPlayerHomeSafe
    {
        get; private set;
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance==null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance=this;
    }
    void Start()
    {
        player = GameObject.Find("Player");
        _isPlayerHomeSafe=true;
    }
    void Update()
    {
        if(!player.GetComponent<PlayerController>().isPlayerAlive)
        {
            GameOver();
        }
    }

    public void PlayerHomeStatus(bool status)
    {
        _playerIsHome=status;
    }

    public void PlayerHomeDestroyed()
    {
        _isPlayerHomeSafe=false;
    }

    public void PlayerHeartDestroyed()
    {
        GameOver();
    }

    private void GameOver()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
