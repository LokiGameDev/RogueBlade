using UnityEngine;

public class PlayerHeart : MonoBehaviour
{

    public void GotHitByBullet()
    {
        GameManager.Instance.GameOver();
    }
}
