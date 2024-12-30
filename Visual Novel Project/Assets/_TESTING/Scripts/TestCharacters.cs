using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;
using JetBrains.Annotations;
using TMPro;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempFont;

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start()
        {

            //Character Rainer = CharacterManager.instance.CreateCharacter("Ali");
            //Character Ali2 = CharacterManager.instance.CreateCharacter("Ali");
            //Character Gerard = CharacterManager.instance.CreateCharacter("Gerard");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            //Character_Sprite Rainer = CreateCharacter("Rainer") as Character_Sprite;
            Character_Sprite Ali = CreateCharacter("Ali") as Character_Sprite;
            Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;

            Ali.isVisible = false;
            Baron.Say("Hi I'm Baron.");

            yield return new WaitForSeconds(2);

            Baron.Say("What's your name?");

            yield return new WaitForSeconds(2);

            Baron.MoveToPosition(Vector2.zero);
            yield return Ali.Show();
            Ali.MoveToPosition(new Vector2(1, 0));

            Ali.Say("I dont know... I've lost my memory...");

            yield return new WaitForSeconds(2);

            Ali.Say("But... I remembered...");

            yield return new WaitForSeconds(2);

            Ali.TransitionSprite(Ali.GetSprite("Mogging"));
            Ali.Say("I remembered how beautiful you are.");

            yield return new WaitForSeconds(2);

            Sprite body = Baron.GetSprite("B2");
            Sprite face = Baron.GetSprite("B_Blush");
            Baron.TransitionSprite(body);
            yield return Baron.TransitionSprite(face, 1);

            Baron.Say("H-hey... Quite a fliratious one aren't you?");

            yield return new WaitForSeconds(2);

            Baron.Say("A-anyway. Please stop joking around. Tell me your full name please, Sir.");
            Baron.TransitionSprite(Baron.GetSprite("B_Angry"), layer:1);
            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
