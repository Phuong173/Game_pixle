using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SingletonUI<MainMenu>
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button settingButton;

    protected override void Awake()
    {
        base.Awake();
        inventoryButton.onClick.AddListener(OpenInventory);
        settingButton.onClick.AddListener(()=>CanvasManager.I.Push(GlobalUIInfo.SettingPopup));
    }

    private void OnEnable()
    {
        inventoryButton.gameObject.SetActive(true);
        settingButton.gameObject.SetActive(true);
        if (GameManager.Exists())
        {
            inventoryButton.gameObject.SetActive(false);
        }
    }

    public void OpenInventory()
    {
        CanvasManager.I.Push(GlobalUIInfo.SelectAmmoPanel);
    }
}
