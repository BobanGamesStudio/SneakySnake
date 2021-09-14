using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DiffLevel;

public static class SaveSystem{
    
    public static void SaveProgressData(ProgressData campaignData){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.ss";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData(campaignData);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static ProgressData LoadProgressData(){
        string path = Application.persistentDataPath + "/progress.ss";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData data = formatter.Deserialize(stream) as ProgressData;
            stream.Close();

            return data;
        }
        else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
