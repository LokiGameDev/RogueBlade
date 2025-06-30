using TMPro;
using UnityEngine;

public class TurretHolder : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject groundPanel;
    public TextMeshPro instructionText;
    public TextMeshPro instructionText2;
    private bool _isInsideHolder = false;
    private PlayerController _player;
    public int index;

    void Start()
    {
        instructionText.gameObject.SetActive(false);
        instructionText2.gameObject.SetActive(false);
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        groundPanel.SetActive(false);
    }
    void Update()
    {
        if (GameManager.Instance.turretStatus[index])
        {
            TurretSpawnFunction();
        }
        else
        {
            if (_isInsideHolder)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (_player.playerScore >= 50)
                    {
                        GameManager.Instance.TurretGotSpawned(index);
                        _player.ReduceScore(50);
                        TurretSpawnFunction();
                    }
                    else
                    {
                        _player.GetComponent<PlayerController>().InsufficientMoneyInfo();
                    }
                }
            }
        }
    }

    public void TurretSpawnFunction()
    {
        Instantiate(turretPrefab, transform.position, turretPrefab.transform.rotation);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInsideHolder = true;
            instructionText.gameObject.SetActive(true);
            instructionText2.gameObject.SetActive(true);
            groundPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInsideHolder = false;
            instructionText.gameObject.SetActive(false);
            instructionText2.gameObject.SetActive(false);
            groundPanel.SetActive(false);
        }
    }
}
