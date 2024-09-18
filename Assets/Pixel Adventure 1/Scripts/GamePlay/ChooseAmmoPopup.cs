using System;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.UI;

namespace Pixel_Adventure_1.Scripts
{
    public class ChooseAmmoPopup : SingletonUI<ChooseAmmoPopup>
    {
        public ChooseAmmoPopupItem choosing;
        public List<ChooseAmmoPopupItem> itemsUsed = new List<ChooseAmmoPopupItem>();
        public List<ChooseAmmoPopupItem> itemUnUsed = new List<ChooseAmmoPopupItem>();

        private void OnEnable()
        {
            UpdateItems();
        }

        public void UpdateItems()
        {
            choosing = null;
            for (int i = 0; i < GameData.I.ListFruitUse.Count; i++)
            {
                itemsUsed[i].UpdateData(GameData.I.ListFruitUse[i].ID);
            }

            for (int i = 0; i < GameData.I.ListFruitUnUse.Count; i++)
            {
                itemUnUsed[i].UpdateData(GameData.I.ListFruitUnUse[i].ID);
            }
        }
    }
}