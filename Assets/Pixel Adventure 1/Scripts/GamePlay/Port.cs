using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] public int monsterAmount;
    [SerializeField] public List<MonsterBase> listMonsters;
    [SerializeField] public bool isRegistered;

    private void Awake()
    {
        monsterAmount = listMonsters.Count;
        for (int i = 0; i < listMonsters.Count; i++)
        {
            listMonsters[i].gameObject.SetActive(false);
        }
    }

    public void RegisterEvent()
    {
        this.RegistEvent(eEventType.MonsterDie, OnMonsterDie);
        isRegistered = true;
        for (int i = 0; i < listMonsters.Count; i++)
        {
            listMonsters[i].gameObject.SetActive(true);
        }
    }

    public void RemoveEvent()
    {
        this.RemoveRegister(eEventType.MonsterDie, OnMonsterDie);
    }

    private void OnMonsterDie()
    {
        monsterAmount--;
        if (monsterAmount <= 0)
        {
            transform.DOMoveY(transform.position.y - 5, 3f);
            this.PostEvent(eEventType.WaveClear);
        }
    }

    private void OnDestroy()
    {
        if (isRegistered)
        {
            this.RemoveRegister(eEventType.MonsterDie, OnMonsterDie);
        }
    }
}