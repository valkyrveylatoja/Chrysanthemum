using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CHARACTERS;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        // Serializing to give access to private in Unity inspector
        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;

        public static DialogueSystem instance { get; private set; }

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        public DialogueContinuePrompt prompt;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);
        }

        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect); // To use architect referene from conversation manager with no other scripts referencing it

        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke(); // If it is null then it won't do anything, otherwise Invoke()
        }

        public void ApplySpeakerDataToDialogueContainer(string speakerName)
        {
            Character character = CharacterManager.instance.GetCharacter(speakerName);
            CharacterConfigData config = character != null ? character.config : CharacterManager.instance.GetCharacterConfig(speakerName);

            ApplySpeakerDataToDialogueContainer(config);
        }

        public void ApplySpeakerDataToDialogueContainer(CharacterConfigData config)
        {
            // Set Dialogue details
            dialogueContainer.SetDialogueColor(config.dialogueColor);
            dialogueContainer.SetDialogueFont(config.dialogueFont);
            float fontSize = this.config.defaultDialogueFontSize * this.config.dialogueFontScale * config.dialogueFontScale;
            dialogueContainer.SetDialogueFontSize(fontSize);

            // Set Name details
            dialogueContainer.nameContainer.SetNameColor(config.nameColor);
            dialogueContainer.nameContainer.SetNameFont(config.nameFont);
            fontSize = this.config.defaultNameFontSize * config.nameFontScale;
            dialogueContainer.nameContainer.SetNameFontSize(fontSize);
        }
        public void ShowSpeakerName(string speakerName = "")
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerName();
        }
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();

        public Coroutine Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            return Say(conversation);
        }

        public Coroutine Say(List<string> conversation)
        {
            return conversationManager.StartConversation(conversation);
        }
    }
}
