using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private int CURRENTSAVEINDEX = 0;
    private string CURRENTSAVENAME;
    public PlayerHome home;
    public GameObject generalInstruction, enemyDeathEffectPrefab;
    public AudioSource effectsAudioSource;
    public AudioClip[] effectsAudioClips;

    public bool _playerIsHome
    {
        get; private set;
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
            if (_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        CURRENTSAVEINDEX = CurrentSavePlay.saveIndex;
        CURRENTSAVENAME = CurrentSavePlay.currentSaveName;
        _BOSSLEVEL = 15;
        _canSpawnWave = true;
        UIManager.Instance.WaveStatus(!_canSpawnWave);
        player = GameObject.FindWithTag("Player");
        LoadGame();
        _isPlayerHomeSafe = true;
    }
    void Update()
    {
        if (!player.GetComponent<PlayerController>().isPlayerAlive)
        {
            GameOver();
        }
        if (!_canSpawnWave) AllEnemeisCleared();
        if (Input.GetKeyDown(KeyCode.E) && _playerIsHome)
        {
            SpawnWave();
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
            SaveGame();
            if (_level == _BOSSLEVEL)
            {
                UIManager.Instance.WaveStatus(!_canSpawnWave);
                _canSpawnWave = true;
                UIManager.Instance.GameWon();
            }
            else
            {
                UIManager.Instance.WaveStatus(!_canSpawnWave);
                _canSpawnWave = true;
            }
        }
    }
    public void PlayerHomeStatus(bool status)
    {
        _playerIsHome = status;
    }

    public void PlayerHomeDestroyed()
    {
        _isPlayerHomeSafe = false;
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();
    }

    public void SaveGame()
    {
        PlayerController data = player.GetComponent<PlayerController>();
        PlayerAttack data2 = player.GetComponentInChildren<PlayerAttack>();
        string dateTime = "" + DateTime.Now;
        SaveSystem.SavePlayer(data.playerLives, data2._gunAmmo, home._homeHealth, data.playerScore, CURRENTSAVEINDEX, CURRENTSAVENAME, dateTime);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer(CURRENTSAVEINDEX);
        if (data != null)
        {
            _level = data.level;
            CURRENTSAVENAME = data.saveName;
            home.InitializeHomeHealth(data.homeHealth);
            player.GetComponent<PlayerController>().InitializePlayerData(data.playerHealth, data.playerScore);
            player.GetComponentInChildren<PlayerAttack>().InitializeAmmo(data.ammoCount);

        }
        else
        {
            _level = 0;
            home.InitializeHomeHealth(5);
            player.GetComponent<PlayerController>().InitializePlayerData(3, 0);
            player.GetComponentInChildren<PlayerAttack>().InitializeAmmo(0);
            SaveGame();
            generalInstruction.SetActive(true);
            generalInstruction.GetComponent<InstructionManager>().InstructionPlay(0);
            Time.timeScale = 0;
        }
        UIManager.Instance.LevelNumberChange(_level);
    }

    public void HomeHealth()
    {
        if (home._homeHealth < 5)
        {
            player.GetComponent<PlayerController>().HomeHealthRefill();
        }
    }

    public void RefillHomeHealth()
    {
        home.HomeHealthIncrease(1);
    }

    public void GeneralInstructions(bool status)
    {
        generalInstruction.SetActive(status);
    }

    public void PlayAudioClip(int index)
    {
        effectsAudioSource.clip = effectsAudioClips[index];
        effectsAudioSource.Play();
    }

    public void EnemyDeathEffectPlay(Vector3 pos)
    {
        Vector3 position = new Vector3(pos.x, 0.4f, pos.z);
        Instantiate(enemyDeathEffectPrefab, position, enemyDeathEffectPrefab.transform.rotation);
    }
}
