using System;
using UnityEngine;
using UnityEditor;

public class AutoSceneSetup : Singleton<AutoSceneSetup>
{
    public SetupSettings[] settings;

    public void SetScene(SetupSettingId id)
    {
        foreach (var setting in settings)
            if (setting.id == id)
                SetSceneObjectsActive(setting);
    }

    private void SetSceneObjectsActive(SetupSettings settings)
    {
        foreach(var obj in settings.activate)
            obj.SetActive(true);
        foreach (var obj in settings.deactivate)
            obj.SetActive(false);
    }
}

public enum SetupSettingId { Default, Map }

[Serializable]
public struct SetupSettings
{
    public SetupSettingId id;
    public GameObject[] activate;
    public GameObject[] deactivate;
}

public static class EditorSceneSetup
{
    #if UNITY_EDITOR

    [MenuItem("Debug/Scene Setup/Default")]
    static void SetDefaultActive()
    {
        //Debug.Log("Setting default scene objects active...");
        AutoSceneSetup.instance.SetScene(SetupSettingId.Default);
    }

    [MenuItem("Debug/Scene Setup/Map")]
    static void SetTestActive()
    {
        //Debug.Log("Setting map scene objects active...");
        AutoSceneSetup.instance.SetScene(SetupSettingId.Map);
    }

    #endif
}
