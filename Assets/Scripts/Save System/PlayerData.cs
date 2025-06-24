[System.Serializable]
public class PlayerData
{
    public int level;
    public int playerHealth, homeHealth, playerScore;
    public int ammoCount;
    public string saveName, dateTimeInfo;

    public PlayerData(int playerH, int ammo, int homeH, int playerS, string name, string dateTime)
    {
        level = GameManager.Instance._level;
        playerHealth = playerH;
        homeHealth = homeH;
        ammoCount = ammo;
        playerScore = playerS;
        saveName = name;
        dateTimeInfo = dateTime;
    }
}
