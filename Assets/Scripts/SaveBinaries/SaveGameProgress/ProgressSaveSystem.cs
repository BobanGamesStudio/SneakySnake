using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DiffLevel;

public static class ProgressSaveSystem{
    
    public static void SaveProgressData(ProgressData campaignData){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.ss";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData(campaignData);

        formatter.Serialize(stream, data);

        stream.Close();

        Debug.Log("Progress data saved");
    }

    public static void ProgressDataInit(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.ss";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData();

        formatter.Serialize(stream, data);

        stream.Close();

        Debug.Log("Progress data initiated");
    }

    public static ProgressData LoadProgressData(){
        string path = Application.persistentDataPath + "/progress.ss";
        
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressData data = formatter.Deserialize(stream) as ProgressData;
            stream.Close();

            Debug.Log("Progress data loaded");

            return data;
        }
        else{
            Debug.LogWarning("Save file not found in " + path);

            ProgressDataInit();
            ProgressData data = ProgressSaveSystem.LoadProgressData();

            return data;
        }
    }
}
