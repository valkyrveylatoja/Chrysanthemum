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
            Character rainer1 = CreateCharacter("Rainer1 as Rainer");
            Character rainer2 = CreateCharacter("Ali");
            Character rainer3 = CreateCharacter("Gerard");

            rainer1.SetPosition(Vector2.zero);
            rainer2.SetPosition(new Vector2(0.5f, 0.5f));
            rainer3.SetPosition(Vector2.one);

            rainer2.Show();
            rainer3.Show();

            yield return rainer1.Show();

            yield return rainer1.MoveToPosition(Vector2.one, smooth: true);
            yield return rainer1.MoveToPosition(Vector2.zero, smooth: true);

            rainer1.SetDialogueFont(tempFont);
            rainer1.SetNameFont(tempFont);
            rainer2.SetDialogueColor(Color.cyan);
            rainer3.SetNameColor(Color.green);
            rainer3.SetDialogueColor(Color.red);

            yield return rainer1.Say("Yo. I'm Rainer.");
            yield return rainer2.Say("...and I'm Ali.");
            yield return rainer3.Say("..and I'm Gerard.");

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
