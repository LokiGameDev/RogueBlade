using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject webGLPanel;
    public GameObject saveFilePanels;
    public GameObject startPanel, settingsPanel;
    void Start()
    {
        startPanel.SetActive(true);
        saveFilePanels.SetActive(false);
        settingsPanel.SetActive(false);
        if (!PlayerPrefs.HasKey("MusicV")) PlayerPrefs.SetFloat("MusicV", 0.5f);
        if (!PlayerPrefs.HasKey("SFXVolume")) PlayerPrefs.SetFloat("SFXVolume", 0.5f);
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        #if UNITY_WEBGL && !UNITY_EDITOR
            webGLPanel.SetActive(true);
        #else
            saveFilePanels.SetActive(true);
        #endif
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsPanel()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackButton(int index)
    {
        if (index == 0) saveFilePanels.SetActive(false);
        else settingsPanel.SetActive(false);

        startPanel.SetActive(true);
    }
    
}
