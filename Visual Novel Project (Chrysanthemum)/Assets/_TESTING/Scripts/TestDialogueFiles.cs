using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using VISUALNOVEL;

public class TestDialogueFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileToRead = null;

    // Start is called before the first frame update
    void Start()
    {
        StartConversation();
    }

    void StartConversation()
    {
        string fullPath = AssetDatabase.GetAssetPath(fileToRead);

        int resourcesIndex = fullPath.IndexOf("Resources/");
        string relativePath = fullPath.Substring(resourcesIndex + 10);

        string filePath = Path.ChangeExtension(relativePath, null);

        LoadFile(filePath);
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //    DialogueSystem.instance.dialogueContainer.Hide();

        //else if (Input.GetKeyUp(KeyCode.UpArrow))
        //    DialogueSystem.instance.dialogueContainer.Show();
    }

    public void LoadFile(string filePath)
    {
        List<string> lines = new List<string>();
        TextAsset file = Resources.Load<TextAsset>(filePath);

        try
        {
            lines = FileManager.ReadTextAsset(file);
        }
        catch
        {
            Debug.LogError($"Dialogue file at path 'Resources/{filePath}' does not exist!");
            return;
        }

        DialogueSystem.instance.Say(lines, filePath);
    }
}
