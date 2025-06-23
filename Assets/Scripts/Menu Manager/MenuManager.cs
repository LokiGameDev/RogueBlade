using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject saveFilePanels;
    public GameObject startPanel;
    void Start()
    {
        startPanel.SetActive(true);
        saveFilePanels.SetActive(false);
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        saveFilePanels.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
