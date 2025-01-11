using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

public class TestConversationQueue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        List<string> lines = new List<string>()
        {
            "This is line 1 from the original conversation.",
            "This is line 2 from the original conversation.",
            "This is line 3 from the original conversation.",
        };

        yield return DialogueSystem.instance.Say(lines);

        DialogueSystem.instance.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        List<string> lines = new List<string>();
        Conversation conversation = null;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            lines = new List<string>()
            {
                "This is the start of an eqnueued conversation.",
                "We can keep going!"
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.Enqueue(conversation);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            lines = new List<string>()
            {
                "This is an Important conversation!",
                "Hi Ali, oh okay."
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.EnqueuePriority(conversation);
        }
    }
}
