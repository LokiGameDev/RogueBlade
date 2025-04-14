using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHome : MonoBehaviour
{
    [SerializeField]
    private bool _playerIsHome;
    private int _homeHealth;
    void Start()
    {
        _playerIsHome=false;
        _homeHealth=5;
        UIManager.Instance.HomeLivesUpdate(_homeHealth);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _playerIsHome=true;
            GameManager.Instance.PlayerHomeStatus(_playerIsHome);
            UIManager.Instance.EnemySpawnInstructionStatus(_playerIsHome);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _playerIsHome=false;
            GameManager.Instance.PlayerHomeStatus(_playerIsHome);
            UIManager.Instance.EnemySpawnInstructionStatus(_playerIsHome);
        }
    }

    public void GotHitByBullet()
    {
        _homeHealth--;
        UIManager.Instance.HomeLivesUpdate(_homeHealth);

        if(_homeHealth<=0)
        {
            Debug.Log("GameOver");
            GameManager.Instance.PlayerHomeDestroyed();
            this.gameObject.SetActive(false);
        }
    }
}
