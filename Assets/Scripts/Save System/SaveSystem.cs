using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(int playerH, int ammo, int homeH, int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.loki";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerH, ammo, homeH, score);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saved Successfully");
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.loki";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file is not found");
            return null;
        }
    }
}
