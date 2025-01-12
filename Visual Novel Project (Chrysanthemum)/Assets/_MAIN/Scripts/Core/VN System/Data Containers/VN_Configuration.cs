using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VN_Configuration
{
    public static VN_Configuration activeConfig;

    public static string filePath => $"{FilePaths.root}vnconfig.cfg";

    public const bool ENCRYPT = false ;

    // General Settings
    public float dialogueTextSpeed = 1f;
    public float dialogueAutoReadSpeed = 1f;

    // Audio Settings
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public bool musicMute = false;
    public bool sfxMute = false;

    // Other Settings


    public void Load()
    {
        var ui = ConfigMenu.instance.ui;
        // Apply the general Settings

        // Set the value of the architect and auto reader speed
        ui.architectSpeed.value = dialogueTextSpeed;
        ui.autoReaderSpeed.value = dialogueAutoReadSpeed;
    }

    public void Save()
    {
        FileManager.Save(filePath, JsonUtility.ToJson(this), encrypt: ENCRYPT);
    }
}
