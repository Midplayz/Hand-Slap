using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveLoadManager
{
    public static List<PersistanceEntity> persistanceEntities = new List<PersistanceEntity>()
    {
        new PersistanceEntity() 
        {
            objectType = typeof(UserData),
            fileName = "Uornsr0d8hkzcb4.whizzyants",
        },

        new PersistanceEntity()
        {
            objectType = typeof(PurchaseTrackerData),
            fileName = "Pchywvziuy9q0tj.whizzyants",
        },

        new PersistanceEntity()
        {
             objectType = typeof(SettingsData),
            fileName = "Slwu0wpr2nhvmkc.whizzyants",
        },
    };
    public static void SaveData<T>(T data)
    {
        string fileName = GetFileName<T>();
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + fileName, false);
        string content = JsonUtility.ToJson(data);
        writer.Write(content);
        writer.Close();
    }
    public static T LoadData<T>()
    {
        T t = default(T);
        string fileName = GetFileName<T>();

        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            string contents = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
            t = JsonUtility.FromJson<T>(contents);
        }

        return t;
    }

    static string GetFileName<T>()
    {
        string fileName = "";
        for (int i = 0; i < persistanceEntities.Count; i++)
        {
            if (persistanceEntities[i].objectType.Equals(typeof(T)))
            {
                fileName = persistanceEntities[i].fileName;
                break;
            }
        }

        if (fileName == "")
        {
            throw new Exception("Type " + typeof(T).Name + " has no save file configured");
        }

        return fileName;
    }
    public class PersistanceEntity
    {
        public Type objectType;
        public string fileName;
    }
}
