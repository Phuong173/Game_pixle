using System;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.DataSTO;
using Pixel_Adventure_1.Scripts.UI;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


public class FireController : SingletonMonoBehavior<FireController>
{
    [SerializeField] public SerializableDictionaryBase<int, FruitItemData> listItemData;
    [SerializeField] public List<FruitItem> listFruit;
    [SerializeField] public FruitItem fruitBase;

    public void Resetup()
    {
        fruitBase = new FruitItem();
        listFruit = new List<FruitItem>();
        listItemData = GameData.I.StoFruitsData.FruitItemData;
        for (int i = 0; i < GameData.I.ListFruitItemsData.Count; i++)
        {
            if (GameData.I.ListFruitItemsData[i].isUsed)
            {
                FruitItem item = new FruitItem();
                item.m_ID = GameData.I.ListFruitItemsData[i].ID;
                item.m_ePooling = listItemData[item.m_ID].PoolingName;
                listFruit.Add(item);
            }
        }

        fruitBase.m_ePooling = GameData.I.StoFruitsData.FruitItemDataBase.PoolingName;
        AmmoManagerMenu.I.UpdateChange();
    }

    private void Start()
    {
        Resetup();
        AmmoManagerMenu.I.UpdateChange();
        AmmoManagerMenu.I.UpdateData();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire0"))
        {
            if (fruitBase.canShoot)
            {
                fruitBase.Fire();
                fruitBase.m_Cooldown = GameData.I.StoFruitsData.FruitItemDataBase.Cooldown;
                fruitBase.canShoot = false;
                AmmoManagerMenu.I.UpdateAmmo();
                OnFire();
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            if (listFruit[0].canShoot && listFruit[0].amount > 0)
            {
                listFruit[0].Fire();
                listFruit[0].m_Cooldown = listItemData[listFruit[0].m_ID].Cooldown;
                listFruit[0].canShoot = false;
                AmmoManagerMenu.I.UpdateAmmo();
                OnFire();
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (listFruit[1].canShoot && listFruit[1].amount > 0)
            {
                listFruit[1].Fire();
                listFruit[1].m_Cooldown = listItemData[listFruit[1].m_ID].Cooldown;
                listFruit[1].canShoot = false;
                AmmoManagerMenu.I.UpdateAmmo();
                OnFire();
            }
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            if (listFruit[2].canShoot && listFruit[2].amount > 0)
            {
                listFruit[2].Fire();
                listFruit[2].m_Cooldown = listItemData[listFruit[2].m_ID].Cooldown;
                listFruit[2].canShoot = false;
                AmmoManagerMenu.I.UpdateAmmo();
                OnFire();
            }
        }
        else if (Input.GetButtonDown("Change0"))
        {
            FruitItem temp = listFruit[0];
            listFruit[0] = listFruit[3];
            listFruit[3] = temp;
            AmmoManagerMenu.I.UpdateChange();
        }
        else if (Input.GetButtonDown("Change1"))
        {
            FruitItem temp = listFruit[1];
            listFruit[1] = listFruit[3];
            listFruit[3] = temp;
            AmmoManagerMenu.I.UpdateChange();
        }
        else if (Input.GetButtonDown("Change2"))
        {
            FruitItem temp = listFruit[2];
            listFruit[2] = listFruit[3];
            listFruit[3] = temp;
            AmmoManagerMenu.I.UpdateChange();
        }

        for (int i = 0; i < listFruit.Count; i++)
        {
            if (!listFruit[i].canShoot)
            {
                listFruit[i].m_Cooldown -= Time.deltaTime;
                if (listFruit[i].m_Cooldown <= 0)
                {
                    listFruit[i].m_Cooldown = 0;
                    listFruit[i].canShoot = true;
                }
            }
        }

        if (!fruitBase.canShoot)
        {
            fruitBase.m_Cooldown -= Time.deltaTime;
            if (fruitBase.m_Cooldown <= 0)
            {
                fruitBase.m_Cooldown = 0;
                fruitBase.canShoot = true;
            }
        }

        AmmoManagerMenu.I.UpdateData();
    }

    public void OnFire()
    {
        for (int i = 0; i < listFruit.Count; i++)
        {
            if (listFruit[i].m_Cooldown <= 0.5f)
            {
                listFruit[i].m_Cooldown = 0.5f;
                listFruit[i].canShoot = false;
            }
        }
    }
}

[Serializable]
public class FruitItem
{
    public int m_ID = -1;
    public int amount = 5;
    public bool canShoot = true;
    public ePooling m_ePooling;
    public float m_Cooldown = 0;

    public void Fire()
    {
        PoolingManager.I.GetObject(m_ePooling);
        AudioManager.I.Shot("Shot");
        if (GameManager.Exists())
        {
            amount--;
        }
    }
}