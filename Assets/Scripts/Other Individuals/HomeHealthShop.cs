using System;
using UnityEngine;

public class HomeHealthShop : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public GameObject healthShopInstruct;

    void Start()
    {
        healthShopInstruct.SetActive(false);   
    }

    private void OnEnable()
    {
        inputReader.OnPlayerHomeHealth -= HandleHomeHealthRefill;
    }

    private void OnDisable()
    {
        inputReader.OnPlayerHomeHealth -= HandleHomeHealthRefill;
    }

    private void HandleHomeHealthRefill()
    {
        if (healthShopInstruct.activeSelf)
        {
            HomeHealthRefill();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        healthShopInstruct.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        healthShopInstruct.SetActive(false);
    }

    public void HomeHealthRefill()
    {
        GameManager.Instance.HomeHealth();
    }
}
