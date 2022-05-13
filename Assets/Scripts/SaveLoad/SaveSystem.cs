//used for working with files
using System.IO;
//allows accessing the binary formatter
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TheSignal.SaveLoad
{
    public static class SaveSystem 
    {
        public static void SavePlayer(int CurrentLevel)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //the path were we will save our file
            //Application.persistentDataPath is a unity function that generates a safe place for where we can save our game data
            string path = Application.persistentDataPath+"/player.txt";
            //cerated the file on the system
            FileStream stream = new FileStream(path,FileMode.Create);

            PlayerData data = new PlayerData(CurrentLevel);
            formatter.Serialize(stream, data);
            stream.Close();
        }


        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.txt";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data= formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            else
                return null;
        }
    }
}
