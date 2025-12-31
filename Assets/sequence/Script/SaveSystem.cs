using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    /// this is save system stored the file in binary formate
    public static void SavePlayer(UIManager player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bin";
        //Debug.Log("path" + path);
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            //Debug.Log("Game data");
            return data;
        }
        else
        {
            Debug.LogError("save file not found");
            return null;
        }
    }
    public static void ResetGame()
    {
        string path = Application.persistentDataPath + "/player.bin";

        if (File.Exists(path))
        {
            File.Delete(Application.persistentDataPath + "/player.bin");
            Debug.Log("Game data reset successful");
        }
        else
        {
            Debug.LogWarning("No save file found to reset");
        }
    }
}
