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
            Character_Sprite Rainer = CreateCharacter("Rainer") as Character_Sprite;
            Character_Sprite Ali = CreateCharacter("Ali") as Character_Sprite;
            //Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;

            yield return new WaitForSeconds(1);

            Sprite Ali1 = Ali.GetSprite("Happy");
            Ali.TransitionSprite(Ali1);
            Ali.MoveToPosition(Vector2.zero);
            yield return Rainer.Show();

            yield return new WaitForSeconds(1);

            Ali.TransitionSprite(Ali.GetSprite("Mogging"));




            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
