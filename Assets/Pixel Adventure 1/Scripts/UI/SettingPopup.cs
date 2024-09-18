using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : BaseUI
{
    [SerializeField] private Button musicBtn;
    [SerializeField] private GameObject tickMusic;
    [SerializeField] private Button soundBtn;
    [SerializeField] private GameObject tickSound;

    private void Awake()
    {
        musicBtn.onClick.AddListener(OnMusicButton);
        soundBtn.onClick.AddListener(OnSoundButton);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        tickMusic.SetActive(GameData.I.MusicFX);
        tickSound.SetActive(GameData.I.SoundFX);
    }

    public override void Pop()
    {
        base.Pop();
        Time.timeScale = 1;
    }

    public void OnMusicButton()
    {
        GameData.I.MusicFX = !GameData.I.MusicFX;
        tickMusic.SetActive(GameData.I.MusicFX);
    }

    public void OnSoundButton()
    {
        GameData.I.SoundFX = !GameData.I.SoundFX;
        tickSound.SetActive(GameData.I.SoundFX);
    }
}
