using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DiffLevel;

public static class PorgressSaveSystem{
    
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
            
            InitProgressData();
            ProgressData data = LoadProgressData();

            return data;
        }
    }

    public static void InitProgressData(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.ss";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData();

        formatter.Serialize(stream, data);
        
        stream.Close();

        Debug.Log("Data saved");
    }
}
