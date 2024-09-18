using System;
using DG.Tweening;
using GameTool;
using Pixel_Adventure_1.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : SingletonUI<PlayerMenu>
{
    [SerializeField] private Image currentHealthImg;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textHealth;

    protected override void Awake()
    {
        base.Awake();
        this.RegistEvent(eEventType.CharacterTakeDamage, UpdateData);
        textLevel.text = "Level" + GameData.I.CurrentLevel;
    }

    private void Start()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        textHealth.text = Character.I.currentHealth + "/" + Character.I.maxHealth;
        currentHealthImg.DOFillAmount(Character.I.currentHealth / Character.I.maxHealth, 0.5f);
        currentHealthImg.DOColor(Color.Lerp(Color.red, Color.green, Character.I.currentHealth / Character.I.maxHealth) , 0.5f);
    }

    private void OnDestroy()
    {
        this.RemoveRegister(eEventType.CharacterTakeDamage, UpdateData);
    }
}