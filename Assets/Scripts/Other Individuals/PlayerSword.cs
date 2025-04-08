using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddScore(1);
        }
        else if(other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
