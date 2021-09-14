using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class DictStrGameObj : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField]
    private DictScritObjStrGameObj dictionaryData;

    [SerializeField]
    private List<string> keys = new List<string>();
    [SerializeField]
    private List<GameObject> values = new List<GameObject>();

    [ReadOnly]
    public Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    public bool modifyValues;

    private void Awake()
    {
        for (int i = 0; i < Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
        {
            dict.Add(dictionaryData.Keys[i], dictionaryData.Values[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        if(modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i =0; i< Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
            {
                keys.Add(dictionaryData.Keys[i]);
                values.Add(dictionaryData.Values[i]);
            }
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        Debug.Log("DESERIALIZATION");
        dict = new Dictionary<string, GameObject>();
        dictionaryData.Keys.Clear();
        dictionaryData.Values.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Keys.Add(keys[i]);
            dictionaryData.Values.Add(values[i]);
            dict.Add(keys[i], values[i]);
        }
        modifyValues = false;

        
    }
    
    public void PrintDictionary()
    {
        foreach (var pair in dict)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
}
