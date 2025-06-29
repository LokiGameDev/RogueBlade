using UnityEngine;

public class PlayerSword : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
