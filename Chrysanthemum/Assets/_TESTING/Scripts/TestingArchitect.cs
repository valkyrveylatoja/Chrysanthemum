using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TESTING
{
    public class TestingArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5]
        {
            "Hello my name is pwjgfpwjpfwkf3409809g8.",
            "Hi gerard.",
            "Hi rainer.",
            "Oh ok.",
            "Hi ali.",
        };
        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade; // Can be switched to typewriter/instant/fade
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();

            string longline = "this is a long line of text. this is a long line of text. this is a long line of text. this is a long line of text. this is a long line of text.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(longline);
                    //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longline);
                //architect.Append(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}
