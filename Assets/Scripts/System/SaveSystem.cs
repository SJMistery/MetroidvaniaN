using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(CharacterController2D_Mod player)
    {
        GlobalController.Instance.streamEnded = false;
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.data";//Esto hace que siempre se guarde en el mismo
                                                                      //lugar, dandole el nombre player + .dot, .txt
                                                                      //el que queramos

        FileStream stream = new FileStream(path, FileMode.Create); //para crear la data

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        GlobalController.Instance.streamEnded = false;
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            formatter.Deserialize(stream);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Save File not found in" + path);
            return null;
        }
    }
}
