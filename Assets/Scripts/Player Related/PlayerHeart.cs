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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            _heartHealth--;
            Debug.Log("Dead");
            GameManager.Instance.PlayerHeartDestroyed();
        }
    }
}
