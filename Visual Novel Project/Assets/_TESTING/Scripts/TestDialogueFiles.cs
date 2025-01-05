using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        List<string> lines = FileManager.ReadTextAsset(fileToRead);

        //foreach (string line in lines)
        //{
        //    if (string.IsNullOrWhiteSpace(line))
        //        continue;

        //    DIALOGUE_LINE dl = DialogueParser.Parse(line);

        //    for (int i = 0; i < dl.commandData.commands.Count; i++)
        //    {
        //        DL_COMMAND_DATA.Command command = dl.commandData.commands[i];
        //        Debug.Log($"Command[{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
        //    }
        //}

        DialogueSystem.instance.Say(lines);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
            DialogueSystem.instance.dialogueContainer.Hide();

        else if (Input.GetKeyUp(KeyCode.UpArrow))
            DialogueSystem.instance.dialogueContainer.Show();
    }
}
