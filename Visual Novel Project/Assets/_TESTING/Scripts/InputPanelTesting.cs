using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;

    // Start is called before the first fram update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Character Baron = CharacterManager.instance.CreateCharacter("Baron", revealAfterCreation: true);

        yield return Baron.Say("Hi! What's your name?");

        inputPanel.Show("What is your name?");

        while (inputPanel.isWaitingOnUserInput)
            yield return null;

        string characterName = inputPanel.lastInput;

        yield return Baron.Say($"It's very nice to meet you, {characterName}.");
    }
}
