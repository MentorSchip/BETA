using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

public enum LoadResult { Success, NoSavesFound }

public class PersistentDataManager : Singleton<PersistentDataManager>
{
    public List<UserData> UserData;
    string saveDirectory;

    [SerializeField] float uiDisplayTime = 1f;
    public Action<string, float> SaveDataEvent;

    public UserData currentUser = new UserData();
    public string userLocation;     // * Check if this is really needed

    #if UNITY_EDITOR
    [MenuItem("Debug/Wipe All Progress")]
    static void WipeAllProgress()
    {
        Debug.Log("ALL SAVE DATA DELETED!");
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/Saves");
    }
    #endif

    public void Initialize()
    {
        UserData = new List<UserData>();
    }

    // [TBD] Regenerate file in fail conditions (wait until requirements better known)
    public LoadResult Load()
    {
        //return LoadResult.NoSavesFound;
        saveDirectory = Application.persistentDataPath + "/Saves";
        Debug.Log("Loading from " + saveDirectory);

        if (!Directory.Exists(saveDirectory))
        {
            Debug.Log("Save directory not found, creating new...");
            Directory.CreateDirectory(saveDirectory);
        }

        var info = new DirectoryInfo(saveDirectory);
        FileInfo[] fileInfo = info.GetFiles();
        var result = LoadResult.NoSavesFound;

        foreach (FileInfo file in fileInfo)
        {
            if (file.Name.Contains(".meta"))
                continue;

            string filePath = saveDirectory + "/" + file.Name;
            string dataAsJson;

            if (File.Exists(filePath))
            {
                dataAsJson = File.ReadAllText(filePath);

                try { UserData.Add(JsonConvert.DeserializeObject<UserData>(dataAsJson)); result = LoadResult.Success; }
                catch  { continue; }
            }
        }
        return result;
    }

    public void Save(UserData currentUser)
    {
        Debug.Log("Saving...");
        this.currentUser = currentUser;

        if (SaveDataEvent != null)
            SaveDataEvent("Saving...", uiDisplayTime);

        string filePath = saveDirectory + "/" + currentUser.Name + ".json";
        string dataAsJson = JsonConvert.SerializeObject(currentUser, Formatting.Indented);

        try { File.WriteAllText(filePath, dataAsJson); }
        catch { Debug.LogError("Failed to save file"); }    // [TBD] Automatic file repair on fail
    }
}