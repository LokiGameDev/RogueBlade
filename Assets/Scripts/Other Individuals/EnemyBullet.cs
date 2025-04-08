using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int _bulletSpeed;
    void Start()
    {
        _bulletSpeed = 13;
        Destroy(this.gameObject,2);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _bulletSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().GotHitByBullet();
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("PlayerHome"))
        {
            other.GetComponent<PlayerHome>().GotHitByBullet();
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("PlayerHeart"))
        {
            Destroy(this.gameObject);
        }
    }
}
