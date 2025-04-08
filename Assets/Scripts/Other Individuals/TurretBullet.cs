using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
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
        if(other.CompareTag("Enemy"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddScore(1);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("EnemyCamp"))
        {
            Destroy(this.gameObject);
        }
    }
}
