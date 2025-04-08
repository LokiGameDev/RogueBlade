using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampHealth : MonoBehaviour
{
    [SerializeField]
    private int _campHealth;
    public TextMeshPro _campLivesText;
    void Start()
    {
        
    }

    void Update()
    {
        _campLivesText.text = "" + _campHealth;
    }

    void GotHitByBullet()
    {
        _campHealth--;
        if(_campHealth<=0)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddScore(10);
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            GotHitByBullet();
        }
    }
}
