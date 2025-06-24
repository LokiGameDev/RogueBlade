using UnityEngine;

public class HomeHealthShop : MonoBehaviour
{
    public GameObject healthShopInstruct;

    void Start()
    {
        healthShopInstruct.SetActive(false);   
    }

    void OnTriggerEnter(Collider other)
    {
        healthShopInstruct.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        healthShopInstruct.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healthShopInstruct.activeSelf)
        {
            HomeHealthRefill();
        }
    }
    public void HomeHealthRefill()
    {
        GameManager.Instance.HomeHealth();
    }
}
