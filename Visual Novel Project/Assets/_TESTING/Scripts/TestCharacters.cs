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

            Baron.SetPosition(Vector2.zero);
            Ali.SetPosition(new Vector2(1, 0));

            yield return new WaitForSeconds(1);

            yield return Baron.Flip(0.3f);

            yield return Ali.FaceRight(immediate: true);

            yield return Baron.FaceLeft(immediate: true);

            Ali.Unhighlight();
            yield return Baron.Say("Ali I... {wa 1} I want to say something.");

            Baron.Unhighlight();
            Ali.Highlight();
            yield return Ali.Say("I want to say something too, Baron. {wa 0.5} Can I go first?");

            Baron.Highlight();
            Ali.Unhighlight();
            yield return Baron.Say("I... {wa 0.5} Of course!");

            Ali.Highlight();
            Baron.Unhighlight();
            Ali.TransitionSprite(Ali.GetSprite("Happy"));
            yield return Ali.Say("Yay!");

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
