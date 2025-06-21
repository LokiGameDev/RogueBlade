[System.Serializable]
public class PlayerData
{
    public int level;
    public int playerHealth, homeHealth, playerScore;
    public int ammoCount;

    public PlayerData(int playerH, int ammo, int homeH, int playerS)
    {
        level = GameManager.Instance._level;
        playerHealth = playerH;
        homeHealth = homeH;
        ammoCount = ammo;
        playerScore = playerS;
    }
}
