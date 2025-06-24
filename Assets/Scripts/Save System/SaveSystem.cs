using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string[] allPath = new string[] {
        Application.persistentDataPath + "/save1.loki",
        Application.persistentDataPath + "/save2.loki",
        Application.persistentDataPath + "/save3.loki"
    };

    public static void SavePlayer(int playerH, int ammo, int homeH, int score, int index, string name,string dateTime)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = allPath[index];

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerH, ammo, homeH, score, name, dateTime);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(int index)
    {
        string path = allPath[index];

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
            return null;
        }
    }
}
