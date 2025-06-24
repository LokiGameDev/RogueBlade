using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDetailsLoader : MonoBehaviour
{
    public TextMeshProUGUI[] saveNameText;
    public TextMeshProUGUI[] levelText;
    public TextMeshProUGUI[] dateAndTimeText;
    private bool[] hasSaveFile = new bool[3];
    public GameObject saveFileNamePanel;
    public TMP_InputField saveFileName;
    private int currentSaveIndex;

    void Start()
    {
        saveFileNamePanel.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            LoadTheSaveData(i);
        }
    }

    public void LoadTheSaveData(int index)
    {
        PlayerData data = SaveSystem.LoadPlayer(index);
        if (data == null)
        {
            hasSaveFile[index] = false;
            saveNameText[index].text = "Loki" + index;
            levelText[index].text = "Level : ";
            dateAndTimeText[index].text = "";
        }
        else
        {
            hasSaveFile[index] = true;
            saveNameText[index].text = data.saveName;
            levelText[index].text = "Level : " + data.level;
            dateAndTimeText[index].text = "" + data.dateTimeInfo;
        }
    }

    public void OpenGame(int index)
    {
        if (hasSaveFile[index])
        {
            CurrentSavePlay.ModifyCurrentSaveDetails("Null", index);
            SceneManager.LoadScene(1);
        }
        else
        {
            currentSaveIndex = index;
            saveFileNamePanel.SetActive(true);
        }
    }

    public void NewGame()
    {
        string name = saveFileName.text;
        if (name.Length > 0)
        {
            CurrentSavePlay.ModifyCurrentSaveDetails(name, currentSaveIndex);
            saveFileNamePanel.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    public void CancelSaveFile()
    {
        saveFileNamePanel.SetActive(false);
    }
}
