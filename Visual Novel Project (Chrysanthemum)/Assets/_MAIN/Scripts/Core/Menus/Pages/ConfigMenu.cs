using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DIALOGUE;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class ConfigMenu : MenuPage
{
    public static ConfigMenu instance { get; private set; }

    [SerializeField] private GameObject[] panels;
    private GameObject activePanel;

    public UI_ITEMS ui;

    

    private VN_Configuration config => VN_Configuration.activeConfig;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        activePanel = panels[0];

        LoadConfig();
    }

    private void LoadConfig()
    {
        if (File.Exists(VN_Configuration.filePath))
            VN_Configuration.activeConfig = FileManager.Load<VN_Configuration>(VN_Configuration.filePath, encrypt: VN_Configuration.ENCRYPT);
        else
            VN_Configuration.activeConfig = new VN_Configuration();

        VN_Configuration.activeConfig.Load();
    }

    private void OnApplicationQuit()
    {
        VN_Configuration.activeConfig.Save();
        VN_Configuration.activeConfig = null;
    }

    public void OpenPanel(string panelName)
    {
        GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

        if (panel == null)
        {
            Debug.LogWarning($"Did not find panel called '{panelName}' in config meny.");
            return;
        }

        if (activePanel != null && activePanel != panel)
            activePanel.SetActive(false);

        panel.SetActive(true);
        activePanel = panel;
    }

    [System.Serializable]
    public class UI_ITEMS
    {
        public static Color musicOnColor = new Color(1, 0.65f, 0, 1);
        public static Color musicOffColor = new Color(0.5f, 0.5f, 0.5f, 1);

        [Header("General")]
        public Slider architectSpeed;
        public Slider autoReaderSpeed;

        [Header("Audio")]
        public Image musicFill;
        public Image sfxFill;
        public Slider musicVolume;
        public Slider sfxVolume;
        public Button musicMute;
        public Button sfxMute;
    }

    // UI CALLABLE FUNCTIONS

    public void SetTextArchitectSpeed()
    {
        config.dialogueTextSpeed = ui.architectSpeed.value;

        if (DialogueSystem.instance != null )
            DialogueSystem.instance.conversationManager.architect.speed = config.dialogueTextSpeed;
    }

    public void SetAutoReaderSpeed()
    {
        config.dialogueAutoReadSpeed = ui.autoReaderSpeed.value;

        if (DialogueSystem.instance == null)
            return;

        AutoReader autoReader = DialogueSystem.instance.autoReader;
        if (autoReader != null )
            autoReader.speed = config.dialogueAutoReadSpeed;
    }


    public void SetMusicValue()
    {
        config.musicVolume = ui.musicVolume.value;
        AudioManager.instance.SetMusicVolume(config.musicVolume, config.musicMute);

        ui.musicFill.color = config.musicMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
    }

    public void SetSFXValue()
    {
        config.sfxVolume = ui.sfxVolume.value;
        AudioManager.instance.SetSFXVolume(config.sfxVolume, config.sfxMute);

        ui.sfxFill.color = config.sfxMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;
    }

    public void SetMusicMute()
    {
        config.musicMute = !config.musicMute;
        ui.musicVolume.fillRect.GetComponent<Image>().color = config.musicMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;

        AudioManager.instance.SetMusicVolume(config.musicVolume, config.musicMute);
    }

    public void SetSFXMute()
    {
        config.sfxMute = !config.sfxMute;
        ui.sfxVolume.fillRect.GetComponent<Image>().color = config.sfxMute ? UI_ITEMS.musicOffColor : UI_ITEMS.musicOnColor;

        AudioManager.instance.SetSFXVolume(config.sfxVolume, config.sfxMute);
    }

}
