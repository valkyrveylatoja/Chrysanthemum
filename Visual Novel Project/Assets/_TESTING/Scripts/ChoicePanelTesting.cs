using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class ChoicePanelTesting : MonoBehaviour
    {
        ChoicePanel panel;
        // Start is called before the first frame update
        void Start()
        {
            panel = ChoicePanel.instance;
            StartCoroutine(Running());
        }

        IEnumerator Running()
        {
            string[] choices = new string[]
            {
            "I am feeling supercalifragilisticexpialidocious.",
            "Oh uh...",
            "I- I do not know... >_<",
            "What?"
            };

            panel.Show("How are you feeling today?", choices);

            while (panel.isWaitingOnUserChoice)
                yield return null;

            var decision = panel.lastDecision;

            Debug.Log($"Made choice {decision.answerIndex}- '{decision.choices[decision.answerIndex]}'");
        }
    }
}