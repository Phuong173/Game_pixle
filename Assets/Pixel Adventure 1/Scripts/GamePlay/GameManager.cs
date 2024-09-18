using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private List<Port> listPort;
    [SerializeField] public int indexPort = 0;

    protected override void Awake()
    {
        base.Awake();
        this.RegistEvent(eEventType.WaveClear, OnWaveClear);
        this.RegistEvent(eEventType.CharacterDie, OnCharacterDie);
        listPort[indexPort].RegisterEvent();
    }

    private void OnDestroy()
    {
        this.RemoveRegister(eEventType.WaveClear, OnWaveClear);
        this.RemoveRegister(eEventType.CharacterDie, OnCharacterDie);    }

    private void OnCharacterDie()
    {
        CanvasManager.I.Push(GlobalUIInfo.LosePopup);
    }

    private void OnWaveClear()
    {
        listPort[indexPort].RemoveEvent();
        indexPort++;
        if (indexPort < listPort.Count)
        {
            listPort[indexPort].RegisterEvent();
        }
    }
}