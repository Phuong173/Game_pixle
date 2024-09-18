using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pixel_Adventure_1.Scripts
{
    public class ChoosingPopupItem : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get => m_RectTransform;
            set => m_RectTransform = value;
        }

        public Image Image
        {
            get => m_Image;
            set => m_Image = value;
        }

        public int ID
        {
            get => m_ID;
            set => m_ID = value;
        }

        [SerializeField] private Image m_Image;
        private int m_ID;
        [SerializeField] private RectTransform m_RectTransform;
    }
}