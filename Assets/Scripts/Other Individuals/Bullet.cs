using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _bulletSpeed;
    void Start()
    {
        Destroy(this.gameObject,5);
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _bulletSpeed);
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
