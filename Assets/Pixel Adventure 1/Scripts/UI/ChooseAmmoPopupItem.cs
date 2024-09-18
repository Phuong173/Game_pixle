using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using Pixel_Adventure_1.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAmmoPopupItem : MonoBehaviour
{
    private int id;
    [SerializeField] private Image img;
    [SerializeField] private GameObject imgChoosing;
    [SerializeField] private Button btnChoose;
    [SerializeField] private bool isInChoose;

    private void Awake()
    {
        btnChoose.onClick.AddListener(Choose);
    }

    public void UpdateData(int id)
    {
        this.id = id;
        img.sprite = GameData.I.StoFruitsData.FruitItemData[id].Icon;
        imgChoosing.SetActive(false);
    }

    public void Choose()
    {
        if (ChooseAmmoPopup.I.choosing == null)
        {
            ChooseAmmoPopup.I.choosing = this;
            imgChoosing.SetActive(true);
        }
        else if (isInChoose != ChooseAmmoPopup.I.choosing.isInChoose)
        {
            ChooseAmmoPopup.I.choosing.imgChoosing.SetActive(false);
            for (int i = 0; i < GameData.I.ListFruitItemsData.Count; i++)
            {
                if (GameData.I.ListFruitItemsData[i].ID == ChooseAmmoPopup.I.choosing.id)
                {
                    GameData.I.ListFruitItemsData[i].isUsed = !ChooseAmmoPopup.I.choosing.isInChoose;
                }
                else if (GameData.I.ListFruitItemsData[i].ID == id)
                {
                    GameData.I.ListFruitItemsData[i].isUsed = !isInChoose;
                }
            }

            int tempID = id;
            UpdateData(ChooseAmmoPopup.I.choosing.id);
            ChooseAmmoPopup.I.choosing.UpdateData(tempID);
            FireController.I.Resetup();
            GameData.I.Save();
            ChooseAmmoPopup.I.choosing = null;
        }
        else
        {
            ChooseAmmoPopup.I.choosing.imgChoosing.SetActive(false);
            ChooseAmmoPopup.I.choosing = this;
            imgChoosing.SetActive(true);
        }
    }
}