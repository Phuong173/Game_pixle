using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public static class EventDispatcher
{
    private static UnityAction[] Action = new UnityAction[Enum.GetNames(typeof(eEventType)).Length];

    public static void PostEvent(this object obj, eEventType eEventType)
    {
        Action[(int) eEventType]?.Invoke();
    }

    public static void RegistEvent(this object obj, eEventType eEventType, UnityAction action)
    {
        Action[(int) eEventType] += action;
    }

    public static void RemoveRegister(this object obj, eEventType eEventType, UnityAction action)
    {
        Action[(int) eEventType] -= action;
    }
}

public enum eEventType
{
    CharacterDie,
    CharacterTakeDamage,
    SceneLoaded,
    MonsterDie,
    WaveClear,
}