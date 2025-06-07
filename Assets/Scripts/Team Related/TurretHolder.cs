using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretHolder : MonoBehaviour
{
    public GameObject turretPrefab;
    public TextMeshPro instructionText;
    public TextMeshPro instructionText2;
    private bool _isInsideHolder = false;
    private PlayerController _player;

    void Start()
    {
        instructionText.gameObject.SetActive(false);
        instructionText2.gameObject.SetActive(false);
        _player = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }
    void Update()
    {
        if (_isInsideHolder)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_player.playerScore >= 50)
                {
                    Instantiate(turretPrefab, transform.position, turretPrefab.transform.rotation);
                    _player.ReduceScore(50);
                    Destroy(this.gameObject);
                }
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInsideHolder = true;
            instructionText.gameObject.SetActive(true);
            instructionText2.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInsideHolder = false;
            instructionText.gameObject.SetActive(false);
            instructionText2.gameObject.SetActive(false);
        }
    }
}
