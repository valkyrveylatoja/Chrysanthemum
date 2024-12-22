using System.Collections;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;

// No MonoBehavior to instantiate it as a variable
public class TextArchitect
{
    #region MEMBERS
    // To use either each world space or ui (2D/3D)
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    // If/Or tmpro_ui is not equal to null, use tmpro_ui or tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro.text;
    // Publicly retrievable but only privately assignable
    // To try to build or append texts
    public string targetText { get; private set; } = "";
    // Ability to create new text and append to existing text
    public string preText { get; private set; } = "";
    // Specifically for the fade in text method
    private int preTextLength = 0;
    // Gives the full string of what this architect should be building/appending
    public string fullTargetText => preText + targetText;

    // Multiple Type Writing Methods
    public enum BuildMethod { instant, typewriter, fade }
    // Default Method
    public BuildMethod buildMethod = BuildMethod.typewriter;

    // Color Setting
    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

    // To multiply the base speed and speed multiplier. Otherwise, since base speed cannot be changed, speedMultiplier will be equal to a value that we choose to give in.
    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    // Universal (constant) text speed
    private const float baseSpeed = 1;
    // Will be changed from the Config Menu
    private float speedMultiplier = 1;

    // To control the hurryUp phase when too much text needs to be generated
    // If speed is less or equal to 2 then return characterMultiplier (default); if speed is less or equal to 2.5 return characterMultiplier multiplied by two otherwise characterMultiplier times three
    public int charactersPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }
    private int characterMultiplier = 1;

    // To speed up text depending on how much clicks the user does
    public bool hurryUp = false;

    #endregion
    // Creating instances for both tmpro_ui/tmpro_world
    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }
    public TextArchitect(TextMeshPro tmpro_world)
    {
        this.tmpro_world = tmpro_world;
    }

    // To delay any action until the architect is completed
    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;

        // To verify if nothing is running
        Stop();

        // To store the process
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    public Coroutine Append(string text)
    {
        // tmpro.text will now be our preText. targetText will still be the same
        preText = tmpro.text;
        targetText = text;

        // To verify if nothing is running
        Stop();

        // To store the process
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }
    // Will be handling the text generation
    private Coroutine buildProcess = null;
    // Will remain true if buildProcess is not equal to null
    public bool isBuilding => buildProcess != null;

    public void Stop()
    {
        // If not building then do not stop
        if (!isBuilding)
            return;
        // To stop the process
        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    IEnumerator Building()
    {
        Prepare();

        switch(buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }

        OnComplete();
    }

    private void OnComplete()
    {
        buildProcess = null;
        hurryUp = false;
    }

    public void ForceComplete()
    {
        switch(buildMethod)
        {
            case BuildMethod.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                tmpro.ForceMeshUpdate();
                break;
        }

        Stop();
        OnComplete();
    }
    private void Prepare()
    {
        switch(buildMethod)
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_TypeWriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    // To control the coloring within the message architect
    private void Prepare_Instant()
    {
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        // Changes made will be applied at this point
        tmpro.ForceMeshUpdate();
        // Make sure text is visible on screen
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    private void Prepare_Fade()
    {
        tmpro.text = preText;
        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            preTextLength = tmpro.textInfo.characterCount;
        }
        else
            preTextLength = 0;

        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textInfo = tmpro.textInfo;

        // Creative vertices that will make the opacity of the text from 0 to 100
        Color colorVisable = new Color(textColor.r, textColor.g, textColor.b, 1);
        Color colorHidden = new Color(textColor.r, textColor.g, textColor.b, 0);
        // Color32 to use bytes with 255 rgb values
        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for(int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if(!charInfo.isVisible) 
                continue;
            if (i < preTextLength)
            {
                for(int v = 0; v < 4; v++)
                    vertexColors[charInfo.vertexIndex + v] = colorVisable;
            }
            else
            {
                for (int v = 0; v < 4; v++)
                    vertexColors[charInfo.vertexIndex + v] = colorHidden;
            }
        }

        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private void Prepare_TypeWriter()
    {
        // Checking if everything starting is in order
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        // Checks if there's pretext, forces itself to update
        // Checking if the text is visible
        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    #region TYPEWRITER
    private IEnumerator Build_Typewriter()
    {
        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            // Makes a type writing effect, hurry up multiplies the cycle by 5; otherwise just continues the type normal type writing effect
            tmpro.maxVisibleCharacters += hurryUp ? charactersPerCycle * 5 : charactersPerCycle;

            yield return new WaitForSeconds(0.015f / speed);
        }
    }
    #endregion

    #region FADE
    private IEnumerator Build_Fade()
    {
        int minRange = preTextLength;
        int maxRange = minRange + 1;

        byte alphaThreshold = 15;

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
        float[] alphas = new float[textInfo.characterCount]; // Adds an alpha value for every character

        while(true)
        {
            // Adds the hurry up mechanic to the fade method
            float fadeSpeed = ((hurryUp ? charactersPerCycle * 5 : charactersPerCycle) * speed) * 4f;

            for (int i = minRange; i < maxRange; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                for (int v = 0; v < 4; v++)
                    vertexColors[charInfo.vertexIndex + v].a = (byte)alphas[i];

                if (alphas[i] >= 255)
                    minRange++;
            }

            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            bool lastCharacterIsInvisible = !textInfo.characterInfo[maxRange - 1].isVisible;
            if (alphas[maxRange -1] > alphaThreshold || lastCharacterIsInvisible)
            {
                if (maxRange < textInfo.characterCount)
                    maxRange++;
                else if (alphas[maxRange - 1] >= 255 || lastCharacterIsInvisible)
                    break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}
