using System;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int playerHealth, homeHealth, playerScore;
    public int ammoCount;
    public string saveName, dateTimeInfo;
    public bool[] turretStatus;

    public PlayerData(int playerH, int ammo, int homeH, int playerS, string name, string dateTime,bool[] turStatus)
    {
        level = GameManager.Instance._level;
        playerHealth = playerH;
        homeHealth = homeH;
        ammoCount = ammo;
        playerScore = playerS;
        saveName = name;
        dateTimeInfo = dateTime;
        turretStatus = turStatus;
    }

    public PlayerData()
    {
        level = 0;
        playerHealth = 5;
        homeHealth = 3;
        ammoCount = 0;
        playerScore = 0;
        saveName = "WebPlay";
        dateTimeInfo = DateTime.Now.ToString();
        turretStatus = new bool[] { false, false, false, false};
    }
}
