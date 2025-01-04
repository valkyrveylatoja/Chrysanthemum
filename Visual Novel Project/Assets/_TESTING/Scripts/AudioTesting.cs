using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        IEnumerator Running()
        {
            Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;
            Baron.Show();

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop: true);

            yield return Baron.Say("I'm going to turn off the radio.");

            AudioManager.instance.StopSoundEffect("RadioStatic");

            Baron.Say("It's off now!");
        }
    }
}
        