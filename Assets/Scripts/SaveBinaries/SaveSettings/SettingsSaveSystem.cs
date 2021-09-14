using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DiffLevel;

public static class SettingsSaveSystem{
    
    public static void SaveSettingsData(SettingsSceneManager settingsData){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.ss";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settingsData);

        formatter.Serialize(stream, data);
        
        stream.Close();

        Debug.Log("Data saved");
    }

    public static SettingsData LoadSettingsData(){
        string path = Application.persistentDataPath + "/settings.ss";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();

            Debug.Log("Data loaded");

            return data;
        }
        else{
            Debug.LogError("Save file(Settings) not found in " + path);
            return null;
        }
    }
}
