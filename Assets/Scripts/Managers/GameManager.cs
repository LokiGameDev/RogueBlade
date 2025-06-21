using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

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
    public int _level
    {
        get; private set;
    }
    private bool _canSpawnWave;
    private int _BOSSLEVEL;

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
        _level = 0;
        _BOSSLEVEL = 15;
        _canSpawnWave = true;
        UIManager.Instance.WaveStatus(!_canSpawnWave);
        player = GameObject.Find("Player");
        _isPlayerHomeSafe=true;
    }
    void Update()
    {
        if (!player.GetComponent<PlayerController>().isPlayerAlive)
        {
            GameOver();
        }
        AllEnemeisCleared();
        if (Input.GetKeyDown(KeyCode.E) && _playerIsHome)
        {
            if (_level < _BOSSLEVEL)
            {
                SpawnWave();
            }
            else
            {
                Debug.Log("Boss Level");
            }
        }
    }
    public static event Action SpawnTheEnemies;

    private void SpawnWave()
    {
        if (_canSpawnWave)
        {
            _level++;
            UIManager.Instance.LevelNumberChange(_level);
            SpawnTheEnemies?.Invoke();
            _canSpawnWave = false;
            UIManager.Instance.WaveStatus(!_canSpawnWave);
        }
    }

    public void AllEnemeisCleared()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0 && GameObject.FindGameObjectsWithTag("EnemyCamp").Count() == 0)
        {
            UIManager.Instance.WaveStatus(!_canSpawnWave);
            _canSpawnWave = true;
        }
    }
    public void PlayerHomeStatus(bool status)
    {
        _playerIsHome = status;
    }

    public void PlayerHomeDestroyed()
    {
        _isPlayerHomeSafe=false;
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();
    }
}
