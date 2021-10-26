
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        //Laitetaan arvot playerdata luokkaan, ja tallennetaan tiedot binary formatterilla persistent data path polkuun.
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        //Avataan Bin‰‰ritiedosto ja ladataan sielt‰ arvot playerdata luokkaan, josta ne ladataan pelaajalle..
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data=formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static void Deleteall()
    {
        //Poistetaan kaikki tiedot

        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            File.Delete(Application.persistentDataPath + "/player.fun");
        }
            
    }
}
