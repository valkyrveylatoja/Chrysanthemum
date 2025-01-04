using CHARACTERS;
using DIALOGUE;
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
            yield return new WaitForSeconds(1);

            Character_Sprite Baron = CreateCharacter("Baron") as Character_Sprite;
            Character_Sprite Abod = CreateCharacter("Abod") as Character_Sprite;
            Baron.Show();

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/villagenight");

            AudioManager.instance.PlayTrack("Audio/Ambience/RainyMood", 0);
            AudioManager.instance.PlayTrack("Audio/Music/Calm", 1, pitch: 0.7f);

            yield return Baron.Say("We can have multiple channels for playing ambience as well as music!");

            AudioManager.instance.StopTrack(1);
        }
    }
}
        