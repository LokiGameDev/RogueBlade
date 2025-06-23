using System;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int playerHealth, homeHealth, playerScore;
    public int ammoCount;
    public string saveName;

    public PlayerData(int playerH, int ammo, int homeH, int playerS, string name)
    {
        level = GameManager.Instance._level;
        playerHealth = playerH;
        homeHealth = homeH;
        ammoCount = ammo;
        playerScore = playerS;
        saveName = name;
    }
}
