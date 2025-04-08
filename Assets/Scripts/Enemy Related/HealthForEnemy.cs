using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthForEnemy : MonoBehaviour
{
    [SerializeField]
    private int _lives;
    public TextMeshPro healthCount;
    void Start()
    {
        
    }

    void Update()
    {
        healthCount.text = "" + _lives;
    }

    public void GotHit()
    {
        _lives--;
        if(_lives<=0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GotHit();
        }
    }
}
