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
            //Character_Sprite EvilRainer = CreateCharacter("EvilRainer as Rainer") as Character_Sprite;
            Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;
            Character_Sprite Ali = CreateCharacter("Ali") as Character_Sprite;

            Baron.SetPosition(new Vector2(0, 0));
            Ali.SetPosition(new Vector2(1, 0));

            yield return new WaitForSeconds(1);

            // Ali.TransitionSprite(Ali.GetSprite("Mogging"));
            Ali.Animate("Hop");
            yield return Ali.Say("Hey Baron.. {wa 1} Do you feel that chill?");

            Baron.FaceRight();
            Baron.TransitionSprite(Baron.GetSprite("A2"));
            Baron.TransitionSprite(Baron.GetSprite("A_Shocked"), layer: 1);
            Baron.MoveToPosition(new Vector2(0.1f, 0));
            Baron.Animate("Shiver", true);
            yield return Baron.Say("I- {wa 0.5} I don't know - but I hate it! {a} It's freezing!");

            Ali.TransitionSprite(Ali.GetSprite("Happy"));
            yield return Ali.Say("Oh, {wa 0.5} it's over!");

            Baron.TransitionSprite(Baron.GetSprite("B2"));
            Baron.TransitionSprite(Baron.GetSprite("B_Blush2"), layer: 1);
            Baron.Animate("Shiver", false);
            yield return Baron.Say("Thank the Lord... {wa 0.5} I'm not wearing enough clothes for that crap...");

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
