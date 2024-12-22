using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        // Serializing to give access to private in Unity inspector
        public DialogueContainer dialogueContainer = new DialogueContainer();


        #region "Singleton"
        public static DialogueSystem instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
