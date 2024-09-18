using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSpawner : MonoBehaviour
{
    private void Awake()
    {
        this.RegistEvent(eEventType.SceneLoaded, RegisterSceneLoaded);
    }

    public void RegisterSceneLoaded()
    {
        if (!GameManager.Exists())
        {
            CanvasManager.I.Push(GlobalUIInfo.AmmoManagerMenu);
            CanvasManager.I.Push(GlobalUIInfo.MainMenu);
        }
        else
        {
            CanvasManager.I.Push(GlobalUIInfo.AmmoManagerMenu);
            CanvasManager.I.Push(GlobalUIInfo.PlayerMenu);
            CanvasManager.I.Push(GlobalUIInfo.MainMenu);
        }
    }

    private void OnDestroy()
    {
        this.RemoveRegister(eEventType.SceneLoaded, RegisterSceneLoaded);
    }
}
