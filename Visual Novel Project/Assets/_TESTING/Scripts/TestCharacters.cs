using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            //Character Ali = CharacterManager.instance.CreateCharacter("Ali");
            //Character Ali2 = CharacterManager.instance.CreateCharacter("Ali");
            //Character Gerard = CharacterManager.instance.CreateCharacter("Gerard");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character Rainer = CharacterManager.instance.CreateCharacter("Rainer");
            Character Ali = CharacterManager.instance.CreateCharacter("Ali");
            Character Gerard = CharacterManager.instance.CreateCharacter("Gerard");

            List<string> lines = new List<string>()
            {
                "Hi there!",
                "My name is Rainer!",
                "What's your name?",
                "Oh,{wa 1} is that so?"
            };
            yield return Rainer.Say(lines);

            lines = new List<string>()
            {
                "I am Ali.",
                "More lines{c} here."
            };

            yield return Ali.Say(lines);

            yield return Gerard.Say("This is a line that I want to say.{a} It is a simple line.{a} Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Lo-Louis.");
            Debug.Log("Finished");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
