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
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
    }
}