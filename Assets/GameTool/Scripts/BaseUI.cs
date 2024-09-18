using System;
using UnityEngine;

namespace GameTool
{
    public class BaseUI : MonoBehaviour
    {
        public Config config;
        [HideInInspector] public string identifier;

        public virtual void Init(object[] args)
        {
        }

        public virtual void Pop()
        {
            gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class Config
    {
        public eUIType uiType;
    }

    public enum eUIType
    {
        Menu,
        Popup,
        AlwaysOnTop,
    }
}