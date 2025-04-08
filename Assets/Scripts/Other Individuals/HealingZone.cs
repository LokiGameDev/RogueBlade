using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealingZone : MonoBehaviour
{
    private float _healAreaEnterTime;
    private bool _isPlayerInHeal,_hasTimeStarted;
    private int _healTime;
    public Slider healthSlider;
    private GameObject _player;
    void Start()
    {
        _healTime = 5;
        _isPlayerInHeal=false;
        _hasTimeStarted=false;
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        if(_isPlayerInHeal && !(_player.GetComponent<PlayerController>().playerLives>=5))
        {
            if(!_hasTimeStarted)
            {
                _healAreaEnterTime=Time.time;
                _hasTimeStarted=true;
            }
            else if(_hasTimeStarted)
            {
                if(Time.time-_healAreaEnterTime> _healTime)
                {
                    _player.GetComponent<PlayerController>().PlayerHealLives(1);
                    _hasTimeStarted=false;
                }
                else{
                    healthSlider.value = (Time.time-_healAreaEnterTime)/ _healTime;
                }
            }
        }
        else
        {
            healthSlider.value = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInHeal=true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInHeal=false;
            _hasTimeStarted=false;
            healthSlider.value = 0;
        }
    }
}
