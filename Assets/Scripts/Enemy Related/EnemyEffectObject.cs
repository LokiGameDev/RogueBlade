using UnityEngine;

public class EnemyEffectObject : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 1.2f);
    }
}
