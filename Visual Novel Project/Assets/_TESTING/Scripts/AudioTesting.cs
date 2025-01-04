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
            Character_Sprite Ali = CreateCharacter("Ali") as Character_Sprite;
            Baron.Show();

            yield return DialogueSystem.instance.Say("Narrator", "Can we see your ship?");

            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/5");
            AudioManager.instance.PlayTrack("Audio/Music/Calm", startingVolume: 0.7f);
            AudioManager.instance.PlayVoice("Audio/Voices/ALI_ohok");

            Baron.SetSprite(Baron.GetSprite("B1"), 0);
            Baron.SetSprite(Baron.GetSprite("B_Pumped"), 1);
            Baron.MoveToPosition(new Vector2(0.7f, 0), speed: 0.5f);
            yield return Baron.Say("Yes, of course!");

            Baron.SetSprite(Baron.GetSprite("A2"), 0);
            Baron.SetSprite(Baron.GetSprite("A_Shocked"), 1);
            AudioManager.instance.PlayTrack("Audio/Music/Isn't She Lovely", startingVolume: 0.7f);
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");
            Ali.Show();
            Baron.MoveToPosition(new Vector2(1f, 0), speed: 2f);
            yield return Ali.Say("*Starts crying like a baby*");

            yield return null;
        }
    }
}
        