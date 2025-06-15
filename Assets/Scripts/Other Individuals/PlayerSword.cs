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
        if(other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
