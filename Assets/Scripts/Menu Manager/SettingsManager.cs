using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject[] panels;
    private int currentPanel;
    public Toggle fullScreenToggle;
    public Slider musicSlider, sfxSlider;

    void Start()
    {
        EnablePanel(0);
        DisablePanel(1);
        currentPanel = 0;
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicV");
    }

    void OnEnable()
    {
        fullScreenToggle.onValueChanged.AddListener(FullScreenModeChanger);
    }

    void OnDisable()
    {
        fullScreenToggle.onValueChanged.RemoveListener(FullScreenModeChanger);
    }

    public void PanelEnabler(int index)
    {
        DisablePanel(currentPanel);
        EnablePanel(index);
        currentPanel = index;
    }

    private void EnablePanel(int index)
    {
        panels[index].SetActive(true);
    }

    private void DisablePanel(int index)
    {
        panels[index].SetActive(false);
    }

    public void FullScreenModeChanger(bool state)
    {
        Screen.fullScreen = state;
    }

    public void MusicVolumeChange()
    {
        PlayerPrefs.SetFloat("MusicV", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        GameManager.Instance.AudioLevelChange();
    }
}
