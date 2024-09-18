using System.Collections.Generic;
using GameTool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pixel_Adventure_1.Scripts.UI
{
    public class AmmoManagerMenu : SingletonUI<AmmoManagerMenu>
    {
        [SerializeField] private Image coolDownBaseAmmoImg;
        [SerializeField] private List<Image> coolDownImages = new List<Image>();
        [SerializeField] private List<Image> iconImages = new List<Image>();
        [SerializeField] private List<TMP_Text> listAmountText = new List<TMP_Text>();

        public void UpdateData()
        {
            for (int i = 0; i < FireController.I.listFruit.Count; i++)
            {
                coolDownImages[i].fillAmount = FireController.I.listFruit[i].m_Cooldown /
                                               FireController.I.listItemData[FireController.I.listFruit[i].m_ID].Cooldown;
            }

            coolDownBaseAmmoImg.fillAmount = FireController.I.fruitBase.m_Cooldown /
                                             GameData.I.StoFruitsData.FruitItemDataBase.Cooldown;
        }

        public void UpdateChange()
        {
            for (int i = 0; i < FireController.I.listFruit.Count; i++)
            {
                iconImages[i].sprite = FireController.I.listItemData[FireController.I.listFruit[i].m_ID].Icon;
                listAmountText[i].text = FireController.I.listFruit[i].amount.ToString();
            }
        }

        public void UpdateAmmo()
        {
            for (int i = 0; i < FireController.I.listFruit.Count; i++)
            {
                listAmountText[i].text = FireController.I.listFruit[i].amount.ToString();
            }
        }
    }
}