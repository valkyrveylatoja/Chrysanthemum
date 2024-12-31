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
            Character_Sprite EvilRainer = CreateCharacter("EvilRainer as Rainer") as Character_Sprite;
            Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;
            Character_Sprite Ali = CreateCharacter("Ali") as Character_Sprite;

            EvilRainer.SetColor(Color.red);

            Baron.SetPosition(new Vector2(0.3f, 0));
            Ali.SetPosition(new Vector2(0.45f, 0));
            Rainer.SetPosition(new Vector2(0.6f, 0));
            EvilRainer.SetPosition(new Vector2(0.75f, 0));

            EvilRainer.SetPriority(1000);
            Ali.SetPriority(15);
            Baron.SetPriority(8);
            Rainer.SetPriority(30);

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Ali", "Baron" });

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters();

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Baron", "EvilRainer", "Rainer", "Ali" });

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
