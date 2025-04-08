using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    private int _heartHealth;
    void Start()
    {
        _heartHealth=1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            _heartHealth--;
            Debug.Log("Dead");
            GameManager.Instance.PlayerHeartDestroyed();
        }
    }
}
