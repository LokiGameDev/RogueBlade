using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _bulletSpeed;
    private GameObject _bulletModel;
    void Start()
    {
        Destroy(this.gameObject, 5);
        _bulletModel = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _bulletSpeed);
        _bulletModel.transform.Rotate(0f, 0f, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("EnemyCamp"))
        {
            Destroy(this.gameObject);
        }
    }
}
