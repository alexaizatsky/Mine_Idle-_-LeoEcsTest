using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class dataLoader 
{
    private string dataToJson;
    private string dataFromJson;
    
    private saveData readMyData, writeMyData;
    
    private string fileName = "myData.json";
    private string filePath;

    public dataLoader()
    {
        if (Application.isEditor)
            filePath = Path.Combine(Application.dataPath, fileName);
        else
            filePath = Path.Combine(Application.persistentDataPath + "/", fileName);
   
        if (File.Exists(filePath))
        {
            ReadFromJson();
        }
        else
        {
            writeMyData = new saveData(20,0,0);
            WriteToJson(writeMyData);
            ReadFromJson();
        }
    }


    public void ReadFromJson()
    {
        dataFromJson = File.ReadAllText(filePath);
        readMyData = JsonUtility.FromJson<saveData>(dataFromJson);
    }
    
    public void WriteToJson(saveData _data)
    {
        writeMyData = _data;
        dataToJson = JsonUtility.ToJson(writeMyData);
        File.WriteAllText(filePath, dataToJson);
    }

    public saveData GetMyData()
    {
        return readMyData;
    }
}
