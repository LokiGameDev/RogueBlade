using TMPro;
using UnityEngine;

public class HealthForEnemy : MonoBehaviour
{
    [SerializeField]
    private int _lives;
    public TextMeshPro healthCount;
    public int enemyValue;

    void Update()
    {
        healthCount.text = "" + _lives;
    }

    public void GotHit()
    {
        _lives--;
        if(_lives<=0)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().AddScore(enemyValue);
            GameManager.Instance.EnemyDeathEffectPlay(transform.position);
            GameManager.Instance.PlayAudioClip(3);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            GotHit();
        }
    }
}
