using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    // Serializing to inspect private game objects
    [System.Serializable]

    // No MonoBehavior to instantiate it as a variable
    public class DialogueContainer
    {
        public GameObject root;
        public NameContainer nameContainer;
        public TextMeshProUGUI dialogueText;

        public void SetDialogueColor(Color color) => dialogueText.color = color;
        public void SetDialogueFont(TMP_FontAsset font) => dialogueText.font = font;

        public void SetDialogueFontSize(float size) => dialogueText.fontSize = size;
    }
}